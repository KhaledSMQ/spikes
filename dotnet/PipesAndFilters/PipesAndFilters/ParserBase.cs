namespace Pipeline
{
    public abstract class ParserBase<TInput, TMetadata, TOutput> : PipelineElement<TInput, TOutput>
    {
        public abstract TMetadata ResolveMetadata(TInput input);
        public abstract TOutput Parse(TInput input, TMetadata metadata);

        public override TOutput Process(TInput input)
        {
            var metadata = ResolveMetadata(input);
            var results = Parse(input, metadata);
            return results;
        }
    }
}
