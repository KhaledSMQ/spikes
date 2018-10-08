using System.Collections.Generic;

namespace Pipeline
{
    public class Pipeline : IPipeline
    {
        public IEnumerable<IPipelineElement> Elements { get; private set; }

        public Pipeline(IEnumerable<IPipelineElement> elements)
        {
            Elements = elements ?? new IPipelineElement[]{};
        }

        public object Process(object payload)
        {
            foreach(var element in Elements)
            {
                payload = element.Process(payload);
            }

            return payload;
        }

    
    }
}
