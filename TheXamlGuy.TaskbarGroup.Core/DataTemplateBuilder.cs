namespace TheXamlGuy.TaskbarGroup.Core
{
    public class DataTemplateBuilder : IDataTemplateBuilder
    {
        private readonly Dictionary<Type, Type> items = new();

        public IDataTemplateCollection DataTemplates => new DataTemplateCollection(items);

        public IDataTemplateBuilder Map<TViewModel, TView>()
        {
            items.Add(typeof(TViewModel), typeof(TView));
            return this;
        }
    }
}
