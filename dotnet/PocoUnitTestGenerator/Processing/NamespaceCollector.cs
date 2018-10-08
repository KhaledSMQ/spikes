using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Processing
{
    public class NamespaceCollector : CSharpSyntaxWalker
    {
        public IEnumerable<NamespaceDeclarationSyntax> Namespaces => NamespacesInternal;

        private List<NamespaceDeclarationSyntax> NamespacesInternal { get; }

        public NamespaceCollector()
        {
            NamespacesInternal = new List<NamespaceDeclarationSyntax>();
        }

        public override void VisitNamespaceDeclaration(NamespaceDeclarationSyntax node)
        {
            NamespacesInternal.Add(node);
        }
    }
}
