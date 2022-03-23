namespace TheXamlGuy.TaskbarGroup.Core
{
    public interface IBindViewModel<TViewModel> where TViewModel : class
    {
        public TViewModel ViewModel { get; set; }
    }
}
