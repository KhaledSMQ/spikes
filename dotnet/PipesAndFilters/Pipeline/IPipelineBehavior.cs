namespace Pipeline
{
    public interface IPipelineBehavior : IPipelineElement
    {
        void AddParameters(object parameters);
        object ApplyBehavior(object input);
    }
}
