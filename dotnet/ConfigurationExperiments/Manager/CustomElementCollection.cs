using System.Configuration;

namespace ConfigurationExperiments.Manager
{
    public class CustomElementCollection : ConfigurationElementCollection
    {
        public CustomElement this[int index]
        {
            get { return (CustomElement)BaseGet(index); }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }

        public void Add(CustomElement customElement)
        {
            BaseAdd(customElement);
        }

        public void Clear()
        {
            BaseClear();
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new CustomElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((CustomElement)element).SomeProp;
        }

        public void Remove(CustomElement customElement)
        {
            BaseRemove(customElement.SomeProp);
        }

        public void RemoveAt(int index)
        {
            BaseRemoveAt(index);
        }

        public void Remove(string name)
        {
            BaseRemove(name);
        }
    }
}