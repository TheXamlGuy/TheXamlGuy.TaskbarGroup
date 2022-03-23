namespace TheXamlGuy.TaskbarGroup.Core
{
    public interface IDropTarget<TTarget>
    {
        void Register(TTarget target);
    }
}
