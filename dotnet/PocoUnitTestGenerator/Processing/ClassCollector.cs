using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Processing
{
    public class ClassCollector : CSharpSyntaxWalker
    {
        public IEnumerable<ClassDeclarationSyntax> Classes => ClassesInternal;

        private List<ClassDeclarationSyntax> ClassesInternal { get; }

        public ClassCollector()
        {
            ClassesInternal = new List<ClassDeclarationSyntax>();
        }

        public override void VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            if (node.Modifiers.All(m => m.Text != "public" && m.Text != "internal"))
                return;

            ClassesInternal.Add(node);
        }
    }
}
