using System.IO;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Processing
{
    public class Extractor
    {
        public SyntaxTree Tree { get; private set; }
        public CompilationUnitSyntax Root { get; private set; }
        public SemanticModel SemanticModel { get; private set; }

        public void ExtractFromFile(string path)
        {
            var code = File.ReadAllText(path);
            Tree = CSharpSyntaxTree.ParseText(code);
            var compilation = CSharpCompilation.Create("test").AddSyntaxTrees(Tree);
            SemanticModel = compilation.GetSemanticModel(Tree);
            Root = (CompilationUnitSyntax) Tree.GetRoot();
        }
    }
}
