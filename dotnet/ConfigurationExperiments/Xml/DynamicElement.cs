using System.Collections.Generic;
using System.Dynamic;

namespace ConfigurationExperiments.Xml
{
    public class DynamicElement : DynamicObject
    {
        public IDictionary<string, dynamic> Properties { get; }

        public dynamic this[string property]
        {
            get
            {
                dynamic v;
                if (Properties.TryGetValue(property, out v))
                    return v;
                return null;
            }
            set { Properties[property] = value; }
        }

        public DynamicElement()
        {
            Properties = new Dictionary<string, dynamic>();
        }

        public void AddProperty(string name, dynamic value)
        {
            Properties[name] = value;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = Properties[binder.Name];
            return true;
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            Properties[binder.Name] = value;
            return true;
        }
    }
}
