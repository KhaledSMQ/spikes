using Pipeline;

namespace PipesAndFilters
{
    public abstract class FetcherBase<TInput, TOutput> : PipelineElementWithBehaviors<TInput, TOutput>
    {
        public abstract TOutput Fetch(TInput input);

        public override sealed TOutput Process(TInput input)
        {
            var preResults = (TInput)ApplyBehaviors(input);
            var results = Fetch(preResults);
            return results;
        }
    }
}
