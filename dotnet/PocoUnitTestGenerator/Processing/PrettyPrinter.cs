using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Processing
{
    public class PrettyPrinter
    {
        public void OutputToConsole(IEnumerable<PropertyDeclarationSyntax> properties, string header = null)
        {
            var title = string.IsNullOrWhiteSpace(header) ? "Properties:" : $"Properties ({header}):";
            Console.WriteLine(title);

            foreach (var property in properties)
            {
                var type = property.Type;
                var accessorList = property.AccessorList;
                var identifier = property.Identifier;

                Console.WriteLine($"type = {type}, identifier = {identifier}, accessorList = {accessorList}");
            }
        }

        public void OutputToConsole(IEnumerable<UsingDirectiveSyntax> usings)
        {
            Console.WriteLine("Usings:");

            foreach (var @using in usings)
            {
                var name = @using.Name;

                Console.WriteLine($"name = {name}");
            }
        }

        public void OutputToConsole(IEnumerable<NamespaceDeclarationSyntax> namespaces)
        {
            Console.WriteLine("Namespaces:");

            foreach (var @namespace in namespaces)
            {
                var name = @namespace.Name;

                Console.WriteLine($"name = {name}");
            }
        }

        public void OutputToConsole(IEnumerable<ClassDeclarationSyntax> classes)
        {
            Console.WriteLine("Classes:");

            foreach (var @class in classes)
            {
                var name = @class.Identifier;

                Console.WriteLine($"name = {name}");
            }
        }
    }
}
