using System;

namespace BaseEntities
{
    [Serializable]
    public class Package
    {
        public string Name { get; set; }

        public int Value { get; private set; }

        public Package(int value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return string.Format("{0}: Name = {1} {2}, Value = {3} {4}", GetType().FullName, Name.GetType(), Name, Value.GetType(), Value);
        }
    }
}
