using System.Collections.Generic;

namespace Pipeline
{
    public class PipelineFactory
    {
        private static readonly PipelineFactory Instance = new PipelineFactory();
        private IList<IPipelineElement> Elements { get; set; }

        public static Pipeline CreatePipeline()
        {
            var pipeline = new Pipeline(Instance.Elements);
            return pipeline;
        }

        private PipelineFactory()
        {
            Elements = new List<IPipelineElement>();

            // Read the pipeline elements from config
        }
    }
}
