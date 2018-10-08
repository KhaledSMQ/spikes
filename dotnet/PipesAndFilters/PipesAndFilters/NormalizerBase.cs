namespace Pipeline
{
    public abstract class NormalizerBase<TInput, TMetadata, TOutput> : PipelineElement<TInput, TOutput>
    {
        public abstract TMetadata ResolveMetadata(TInput input);
        public abstract TOutput Normalize(TInput input, TMetadata metadata);

        public override TOutput Process(TInput input)
        {
            var metadata = ResolveMetadata(input);
            var results = Normalize(input, metadata);
            return results;
        }
    }
}
