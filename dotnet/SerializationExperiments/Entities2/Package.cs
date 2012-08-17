using System;

namespace Entities2
{
    [Serializable]
    public class Package : Entities1.Package
    {
        public DateTime DateValue { get; set; }
        public new Colors Color { get; set; }

        public Package(int v)
            : base(v)
        { }

        /*public static explicit operator Package(Entities1.Package package)
        {
            var converted = new Package(package.Value);
            return converted;
        }*/

        public override string ToString()
        {
            return string.Format("{0}, Color = {1} {2}, DateValue = {3} {4}", base.ToString(), Color.GetType(), Color, DateValue.GetType(), DateValue);
        }
    }
}
