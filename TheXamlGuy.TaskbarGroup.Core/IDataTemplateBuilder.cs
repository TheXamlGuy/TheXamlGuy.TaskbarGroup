namespace TheXamlGuy.TaskbarGroup.Core
{
    public interface IDataTemplateBuilder
    {
        IDataTemplateCollection DataTemplates { get; }

        IDataTemplateBuilder Map<TViewModel, TView>();
    }
}
