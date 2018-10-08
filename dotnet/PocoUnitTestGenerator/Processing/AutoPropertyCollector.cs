using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Processing
{
    public class AutoPropertyCollector : CSharpSyntaxWalker
    {
        public IEnumerable<PropertyDeclarationSyntax> PredefinedTypeProperties => PredefinedTypePropertiesInternal;
        public IEnumerable<PropertyDeclarationSyntax> ValueTypeProperties => ValueTypePropertiesInternal;
        public IEnumerable<PropertyDeclarationSyntax> NullableTypeProperties => NullableTypePropertiesInternal;

        private List<PropertyDeclarationSyntax> PredefinedTypePropertiesInternal { get; }
        private List<PropertyDeclarationSyntax> ValueTypePropertiesInternal { get; }
        private List<PropertyDeclarationSyntax> NullableTypePropertiesInternal { get; }

        private SemanticModel Model { get; }

        public AutoPropertyCollector(SemanticModel model)
        {
            PredefinedTypePropertiesInternal = new List<PropertyDeclarationSyntax>();
            ValueTypePropertiesInternal = new List<PropertyDeclarationSyntax>();
            NullableTypePropertiesInternal = new List<PropertyDeclarationSyntax>();
            Model = model;
        }

        public override void VisitPropertyDeclaration(PropertyDeclarationSyntax node)
        {
            if (node.Modifiers.All(m => m.Text != "public" && m.Text != "internal"))
                return;

            if (node.AccessorList.Accessors.Count == 2 &&
                node.AccessorList.Accessors.All(a => a.Modifiers.Count == 0 || a.Modifiers.Any(m => m.Text == "public" || m.Text == "internal")) &&
                node.AccessorList.Accessors.All(a => a.Body == null))
            {
                var type = node.Type;
                switch (type.GetType().ToString())
                {
                    case "Microsoft.CodeAnalysis.CSharp.Syntax.PredefinedTypeSyntax":
                    {
                        var pts = (PredefinedTypeSyntax) type;
                        var keyword = pts.Keyword;
                        var kt = keyword.Text;
                        var kv = keyword.Value;
                        var kvt = keyword.ValueText;
                        PredefinedTypePropertiesInternal.Add(node);
                        break;
                    }
                    case "Microsoft.CodeAnalysis.CSharp.Syntax.NullableTypeSyntax":
                    {
                        var nts = (NullableTypeSyntax) type;
                        var s = nts.ToString();
                        NullableTypePropertiesInternal.Add(node);
                        break;
                    }
                    case "Microsoft.CodeAnalysis.CSharp.Syntax.IdentifierNameSyntax":
                    {
                        var ins = (IdentifierNameSyntax) type;
                        var s = ins.ToString();
                        break;
                    }
                    case "Microsoft.CodeAnalysis.CSharp.Syntax.GenericNameSyntax":
                    {
                        var gns = (GenericNameSyntax) type;
                        var s = gns.ToString();
                        break;
                    }
                }
                /*var typeKind = type.Kind();
                var declaredSymbol = Model.GetDeclaredSymbol(node);
                var ds = declaredSymbol.ToString();

                var typeSymbol = (ITypeSymbol) Model.GetSymbolInfo(type).Symbol;
                var ts = typeSymbol?.ToString();*/
            }
        }
    }
}
