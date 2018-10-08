using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;

namespace Processing
{
    public class CodeProvider
    {
        private Workspace Workspace { get; }

        public CodeProvider(Workspace workspace)
        {
            Workspace = workspace;
        }

        public async Task<IMethodSymbol> Analyze(Document document, int tokenPosition, CancellationToken token)
        {
            var model = await document.GetSemanticModelAsync(token);
            var tree = await model.SyntaxTree.GetRootAsync(token);
            return null;
        }
    }
}
