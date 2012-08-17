using System;

namespace Entities1
{
    [Serializable]
    public class Package : BaseEntities.Package
    {
        public double DoubleValue { get; set; }
        public Colors Color { get; set; }
        //public new string Value { get; set; }

        public Package(int v)
            : base(v)
        { }

        public override string ToString()
        {
            return string.Format("{0}, Color = {1} {2}, DoubleValue = {3} {4}", base.ToString(), Color.GetType(), Color, DoubleValue.GetType(), DoubleValue);
            //return string.Format("{0}, Value = {1} {2}, DoubleValue = {3}", base.ToString(), Value.GetType(), Value, DoubleValue);
        }
    }
}
