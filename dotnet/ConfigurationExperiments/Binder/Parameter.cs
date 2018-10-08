using System;

namespace ConfigurationExperiments.Binder
{
    public class Parameter
    {
        private string _name;
        private Type _type;
        private string _value;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public Type Type
        {
            get { return _type; }
            set { _type = value; }
        }
        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }
    }
}