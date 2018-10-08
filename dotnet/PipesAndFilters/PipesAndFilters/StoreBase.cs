namespace Pipeline
{
    public abstract class StoreBase<TInput, TMetadata, TOutput> : PipelineElement<TInput, TOutput>
    {
        public abstract TMetadata ResolveMetadata(TInput input);
        public abstract TOutput Store(TInput input, TMetadata metadata);

        public override TOutput Process(TInput input)
        {
            var metadata = ResolveMetadata(input);
            var results = Store(input, metadata);
            return results;
        }
    }
}
