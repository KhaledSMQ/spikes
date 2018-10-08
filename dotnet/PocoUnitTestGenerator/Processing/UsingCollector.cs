using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Processing
{
    public class UsingCollector : CSharpSyntaxWalker
    {
        public IEnumerable<UsingDirectiveSyntax> Usings => UsingsInternal;

        private List<UsingDirectiveSyntax> UsingsInternal { get; }

        public UsingCollector()
        {
            UsingsInternal = new List<UsingDirectiveSyntax>();
        }

        public override void VisitUsingDirective(UsingDirectiveSyntax node)
        {
            UsingsInternal.Add(node);
        }
    }
}
