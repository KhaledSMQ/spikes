using System.Collections.Generic;

namespace Pipeline
{
    public abstract class PipelineElementWithBehaviors<TInput, TOutput> : PipelineElement<TInput, TOutput>
    {
        private IList<IPipelineBehavior> Behaviors { get; set; }

        public void AddBehavior(IPipelineBehavior behavior)
        {
            Behaviors.Add(behavior);
        }

        public override TOutput Process(TInput input)
        {
            var results = ApplyBehaviors(input);
            return (TOutput)results;
        }

        protected PipelineElementWithBehaviors()
        {
            Behaviors = new List<IPipelineBehavior>();
        }

        protected virtual object ApplyBehaviors(object input)
        {
            var results = input;
            foreach (var behavior in Behaviors)
            {
                results = behavior.ApplyBehavior(results);
            }
            return results;
        }
    }
}
