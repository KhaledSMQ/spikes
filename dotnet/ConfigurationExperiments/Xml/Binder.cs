using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace ConfigurationExperiments.Xml
{
    public static class Binder
    {
        public static T Bind<T>(this DynamicElement element)
        {
            return element.Bind(default(T));
        }

        public static T Bind<T>(this DynamicElement element, T defaultValue)
        {
            var value = element.Bind(typeof(T));
            if (value == null)
            {
                return defaultValue;
            }

            return (T)value;
        }

        public static object Bind(this DynamicElement element, Type type)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            return BindInstance(
                type: type,
                instance: null,
                element: element);
        }

        private static void BindNonScalar(this DynamicElement element, object instance)
        {
            if (instance != null)
            {
                foreach (var property in GetAllProperties(instance.GetType().GetTypeInfo()))
                {
                    BindProperty(property, instance, element);
                }
            }
        }

        private static void BindProperty(PropertyInfo property, object instance, DynamicElement element)
        {
            // We don't support set only, non public, or indexer properties
            if (property.GetMethod == null ||
                !property.GetMethod.IsPublic ||
                property.GetMethod.GetParameters().Length > 0)
            {
                return;
            }

            var propertyValue = property.GetValue(instance);
            var hasPublicSetter = property.SetMethod != null && property.SetMethod.IsPublic;

            if (propertyValue == null && !hasPublicSetter)
            {
                // Property doesn't have a value and we cannot set it so there is no
                // point in going further down the graph
                return;
            }

            //propertyValue = BindInstance(property.PropertyType, propertyValue, element.GetSection(property.Name));
            propertyValue = BindInstance(property.PropertyType, propertyValue, element[property.Name]); //TODO
            if (propertyValue != null && hasPublicSetter)
            {
                property.SetValue(instance, propertyValue);
            }
        }

        private static object BindInstance(Type type, object instance, DynamicElement element)
        {
            var section = element;
            var configValue = section?["Value"]; //TODO
            if (configValue != null)
            {
                // Leaf nodes are always reinitialized
                return ReadValue(type, configValue, section);
            }

            if (element != null && element.GetChildren().Any())
            {
                if (instance == null)
                {
                    instance = CreateInstance(type);
                }

                // See if its a Dictionary
                var collectionInterface = FindOpenGenericInterface(typeof(IDictionary<,>), type);
                if (collectionInterface != null)
                {
                    BindDictionary(instance, collectionInterface, element);
                }
                else if (type.IsArray)
                {
                    instance = BindArray((Array)instance, element);
                }
                else
                {
                    // See if its an ICollection
                    collectionInterface = FindOpenGenericInterface(typeof(ICollection<>), type);
                    if (collectionInterface != null)
                    {
                        BindCollection(instance, collectionInterface, element);
                    }
                    // Something else
                    else
                    {
                        BindNonScalar(element, instance);
                    }
                }
            }

            return instance;
        }

        private static object CreateInstance(Type type)
        {
            var typeInfo = type.GetTypeInfo();

            if (typeInfo.IsInterface || typeInfo.IsAbstract)
            {
                throw new InvalidOperationException(Resources.FormatError_CannotActivateAbstractOrInterface(type));
            }

            if (typeInfo.IsArray)
            {
                if (typeInfo.GetArrayRank() > 1)
                {
                    throw new InvalidOperationException(Resources.FormatError_UnsupportedMultidimensionalArray(type));
                }

                return Array.CreateInstance(typeInfo.GetElementType(), 0);
            }

            var hasDefaultConstructor = typeInfo.DeclaredConstructors.Any(ctor => ctor.IsPublic && ctor.GetParameters().Length == 0);
            if (!hasDefaultConstructor)
            {
                throw new InvalidOperationException(Resources.FormatError_MissingParameterlessConstructor(type));
            }

            try
            {
                return Activator.CreateInstance(type);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(Resources.FormatError_FailedToActivate(type), ex);
            }
        }

        private static void BindDictionary(object dictionary, Type dictionaryType, DynamicElement config)
        {
            var typeInfo = dictionaryType.GetTypeInfo();

            // IDictionary<K,V> is guaranteed to have exactly two parameters
            var keyType = typeInfo.GenericTypeArguments[0];
            var valueType = typeInfo.GenericTypeArguments[1];

            if (keyType != typeof(string))
            {
                // We only support string keys
                return;
            }

            var addMethod = typeInfo.GetDeclaredMethod("Add");
            foreach (var child in config.GetChildren2())
            {
                var item = BindInstance(
                    type: valueType,
                    instance: null,
                    element: child);
                if (item != null)
                {
                    //var key = child.Key;
                    var key = string.Empty; //TODO
                    addMethod.Invoke(dictionary, new[] { key, item });
                }
            }
        }

        private static void BindCollection(object collection, Type collectionType, DynamicElement config)
        {
            var typeInfo = collectionType.GetTypeInfo();

            // ICollection<T> is guaranteed to have exacly one parameter
            var itemType = typeInfo.GenericTypeArguments[0];
            var addMethod = typeInfo.GetDeclaredMethod("Add");

            foreach (var section in config.GetChildren2())
            {
                try
                {
                    var item = BindInstance(
                        type: itemType,
                        instance: null,
                        element: section);
                    if (item != null)
                    {
                        addMethod.Invoke(collection, new[] { item });
                    }
                }
                catch
                {
                }
            }
        }

        private static Array BindArray(Array source, DynamicElement config)
        {
            var children = config.GetChildren2().ToArray();
            var arrayLength = source.Length;
            var elementType = source.GetType().GetElementType();
            var newArray = Array.CreateInstance(elementType, arrayLength + children.Length);

            // binding to array has to preserve already initialized arrays with values
            if (arrayLength > 0)
            {
                Array.Copy(source, newArray, arrayLength);
            }

            for (int i = 0; i < children.Length; i++)
            {
                try
                {
                    var item = BindInstance(
                        type: elementType,
                        instance: null,
                        element: children[i]);
                    if (item != null)
                    {
                        newArray.SetValue(item, arrayLength + i);
                    }
                }
                catch
                {
                }
            }

            return newArray;
        }

        private static object ReadValue(Type type, string value, DynamicElement element)
        {
            if (type.GetTypeInfo().IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                return ReadValue(Nullable.GetUnderlyingType(type), value, element);
            }

            try
            {
                //return TypeDescriptor.GetConverter(type).ConvertFromInvariantString(element.Value);
                return TypeDescriptor.GetConverter(type).ConvertFromInvariantString(element["Value"]); //TODO
            }
            catch (Exception ex)
            {
                //throw new InvalidOperationException(Resources.FormatError_FailedBinding(element.Value, type), ex);
                throw new InvalidOperationException(Resources.FormatError_FailedBinding(element["Value"], type), ex); //TODO
            }
        }

        private static Type FindOpenGenericInterface(Type expected, Type actual)
        {
            var interfaces = actual.GetTypeInfo().ImplementedInterfaces;
            foreach (var interfaceType in interfaces)
            {
                if (interfaceType.GetTypeInfo().IsGenericType &&
                    interfaceType.GetGenericTypeDefinition() == expected)
                {
                    return interfaceType;
                }
            }
            return null;
        }

        private static IEnumerable<PropertyInfo> GetAllProperties(TypeInfo type)
        {
            var allProperties = new List<PropertyInfo>();

            do
            {
                allProperties.AddRange(type.DeclaredProperties);
                type = type.BaseType.GetTypeInfo();
            }
            while (type != typeof(object).GetTypeInfo());

            return allProperties;
        }

        private static IEnumerable<DynamicElement> GetChildren2(this DynamicElement element)
        {
            return new DynamicElement[] {};
        }

        private static IDictionary<string, dynamic> GetChildren(this DynamicElement element)
        {
            var list = element.Properties.Where(p => typeof(DynamicElement) == p.Value.GetType() || typeof(List<dynamic>) == p.Value.GetType()).ToDictionary(e => e.Key, e => e.Value);
            return list;
        }
    }
}
