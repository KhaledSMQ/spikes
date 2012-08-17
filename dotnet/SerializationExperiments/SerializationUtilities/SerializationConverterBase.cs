namespace SerializationUtilities
{
    public abstract class SerializationConverterBase<TResult, TOriginal> : ISerializationConverter
    {
        public abstract TResult Convert(TOriginal original);

        object ISerializationConverter.Convert(object original)
        {
            var asTOriginal = (TOriginal)original;
            return Convert(asTOriginal);
        }
    }
}
