namespace Pipeline
{
    public interface IPipelineElement
    {
        object Process(object input);
    }
}
