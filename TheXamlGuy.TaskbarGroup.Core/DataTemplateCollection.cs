using System.Collections.ObjectModel;

namespace TheXamlGuy.TaskbarGroup.Core
{
    public class DataTemplateCollection : ReadOnlyDictionary<Type, Type>, IDataTemplateCollection
    {
        public DataTemplateCollection(IDictionary<Type, Type> dictionary) : base(dictionary)
        {

        }
    }
}
