using System;
using System.Collections.Generic;

namespace SerializationUtilities
{
    public class SerializationConverterRegistry
    {
        private IDictionary<Type, IDictionary<Type, ISerializationConverter>> Converters { get; set; }

        public SerializationConverterRegistry()
        {
            Converters = new Dictionary<Type, IDictionary<Type, ISerializationConverter>>();
        }

        public void Register<TReturn, TOriginal>(ISerializationConverter converter)
        {
            var convertersByReturnType = GetConvertersByReturnType(typeof(TReturn));
            if (convertersByReturnType == null)
            {
                convertersByReturnType = new Dictionary<Type, ISerializationConverter>();
                Converters[typeof(TReturn)] = convertersByReturnType;
            }

            convertersByReturnType[typeof(TOriginal)] = converter;
        }

        public ISerializationConverter Get<TReturn, TOriginal>()
        {
            return GetConvertersByReturnTypeAndOriginalType(typeof(TReturn), typeof(TOriginal));
        }

        public ISerializationConverter Get<TReturn>(Type originalType)
        {
            return GetConvertersByReturnTypeAndOriginalType(typeof(TReturn), originalType);
        }

        public ISerializationConverter Get(Type returnType, Type originalType)
        {
            return GetConvertersByReturnTypeAndOriginalType(returnType, originalType);
        }

        private IDictionary<Type, ISerializationConverter> GetConvertersByReturnType(Type returnType)
        {
            IDictionary<Type, ISerializationConverter> convertersByReturnType;
            Converters.TryGetValue(returnType, out convertersByReturnType);
            return convertersByReturnType;
        }

        private ISerializationConverter GetConvertersByReturnTypeAndOriginalType(Type returnType, Type originalType)
        {
            var convertersByReturnType = GetConvertersByReturnType(returnType);
            if (convertersByReturnType == null)
                return null;
            ISerializationConverter converter;
            convertersByReturnType.TryGetValue(originalType, out converter);
            return converter;
        }
    }
}
