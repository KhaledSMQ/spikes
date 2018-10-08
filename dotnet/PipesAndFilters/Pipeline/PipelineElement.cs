namespace Pipeline
{
    public abstract class PipelineElement<TInput, TOutput> : IPipelineElement
    {
        public abstract TOutput Process(TInput input);

        object IPipelineElement.Process(object input)
        {
            return Process((TInput) input);
        }
    }

    public abstract class PipelineElement<TInputAndOutput> : PipelineElement<TInputAndOutput, TInputAndOutput>
    { }
}
