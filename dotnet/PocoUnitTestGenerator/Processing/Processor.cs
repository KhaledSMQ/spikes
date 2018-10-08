using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Formatting;

namespace Processing
{
    public class Processor
    {
        private UsingCollector UsingCollector { get; set; }
        private NamespaceCollector NamespaceCollector { get; set; }
        private ClassCollector ClassCollector { get; set; }
        private AutoPropertyCollector AutoPropertyCollector { get; set; }

        private CompilationUnitSyntax Root { get; }
        private SyntaxTree Tree { get; }
        private SemanticModel Model { get; }

        public Processor(string inputPath, string output)
        {
            var extractor = new Extractor();
            extractor.ExtractFromFile(inputPath);

            Root = extractor.Root;
            Tree = extractor.Tree;
            Model = extractor.SemanticModel;
        }

        public void Process()
        {
            ProcessUsings();
            ProcessNamespaces();
            ProcessClasses();
            ProcessProperties();

            Generate();
        }

        private void ProcessUsings()
        {
            UsingCollector = new UsingCollector();
            UsingCollector.Visit(Root);
            new PrettyPrinter().OutputToConsole(UsingCollector.Usings);
        }

        private void ProcessNamespaces()
        {
            NamespaceCollector = new NamespaceCollector();
            NamespaceCollector.Visit(Root);
            new PrettyPrinter().OutputToConsole(NamespaceCollector.Namespaces);
        }

        private void ProcessClasses()
        {
            ClassCollector = new ClassCollector();
            ClassCollector.Visit(Root);
            new PrettyPrinter().OutputToConsole(ClassCollector.Classes);
        }

        private void ProcessProperties()
        {
            AutoPropertyCollector = new AutoPropertyCollector(Model);
            AutoPropertyCollector.Visit(Root);
            new PrettyPrinter().OutputToConsole(AutoPropertyCollector.PredefinedTypeProperties, "predefined types");
            new PrettyPrinter().OutputToConsole(AutoPropertyCollector.ValueTypeProperties, "value types");
            new PrettyPrinter().OutputToConsole(AutoPropertyCollector.NullableTypeProperties, "nullable types");
        }

        private void Generate()
        {
            BuildFixtureClass(out var cd);
            AddTests(ref cd);

            BuildNamespace(out var ns);
            AddFixtureClass(ref ns, cd);

            var cu = SyntaxFactory.CompilationUnit();

            BuildUsings(out var uds);
            AddUsings(ref cu, uds);
            AddNamespace(ref cu, ns);

            var ws = new AdhocWorkspace();
            var options = ws.Options;
            var formatted = Formatter.Format(cu, ws, options);
            var sb = new StringBuilder();
            using (var sw = new StringWriter(sb))
            {
                formatted.WriteTo(sw);
            }

            var text = sb.ToString();
            Console.WriteLine(text);
        }

        private void BuildUsings(out UsingDirectiveSyntax[] uds)
        {
            var usingNames = UsingCollector.Usings.Select(u => u.Name.ToString()).ToList();
            usingNames.Add("Xunit");

            uds = usingNames.Select(n => SyntaxFactory.UsingDirective(SyntaxFactory.IdentifierName(n))).ToArray();
        }

        private static void AddUsings(ref CompilationUnitSyntax cu, IEnumerable<UsingDirectiveSyntax> uds)
        {
            foreach (var ud in uds)
            {
                cu = cu.AddUsings(ud);
            }
        }

        private void BuildNamespace(out NamespaceDeclarationSyntax ns)
        {
            var nsName = NamespaceCollector.Namespaces.First().Name.ToString();
            nsName += ".Tests";
            ns = SyntaxFactory.NamespaceDeclaration(SyntaxFactory.IdentifierName(nsName));
        }

        private static void AddNamespace(ref CompilationUnitSyntax cu, NamespaceDeclarationSyntax ns)
        {
            cu = cu.AddMembers(ns);
        }

        private void BuildFixtureClass(out ClassDeclarationSyntax cd)
        {
            var cdName = ClassCollector.Classes.First().Identifier.ToString();
            cdName += "Fixture";
            cd = SyntaxFactory.ClassDeclaration(cdName)
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword));
        }

        private static void AddFixtureClass(ref NamespaceDeclarationSyntax ns, ClassDeclarationSyntax cd)
        {
            ns = ns.AddMembers(cd);
        }

        private static void AddTests(ref ClassDeclarationSyntax cd)
        {
            
        }
    }
}
