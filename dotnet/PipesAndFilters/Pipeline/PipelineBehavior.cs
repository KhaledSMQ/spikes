namespace Pipeline
{
    public abstract class PipelineBehavior<TInput, TParameters, TOutput> : PipelineElement<TInput, TOutput>, IPipelineBehavior
    {
        public abstract void AddParameters(TParameters parameters);
        public abstract TOutput ApplyBehavior(TInput input);

        object IPipelineElement.Process(object input)
        {
            var result = ((IPipelineBehavior) this).ApplyBehavior(input);
            return result;
        }

        void IPipelineBehavior.AddParameters(object parameters)
        {
            AddParameters((TParameters) parameters);
        }

        object IPipelineBehavior.ApplyBehavior(object input)
        {
            var result = ApplyBehavior((TInput) input);
            return result;
        }
    }
}
