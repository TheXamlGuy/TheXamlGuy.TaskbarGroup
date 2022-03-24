using System.Threading;
using System.Threading.Tasks;
using TheXamlGuy.TaskbarGroup.Flyout.Foundation;
using Windows.ApplicationModel.DataTransfer;

namespace TheXamlGuy.TaskbarGroup.Flyout
{
    public class TaskbarButtonGroupDragHandler : IDragHandler<TaskbarButtonGroupView>
    {
        public void Handle(Drag<TaskbarButtonGroupView> message)
        {
            message.DragEventArgs.AcceptedOperation = DataPackageOperation.Link;

            if (message.DragEventArgs.DragUIOverride is not null)
            {
                message.DragEventArgs.DragUIOverride.IsContentVisible = true;
                message.DragEventArgs.DragUIOverride.IsGlyphVisible = false;
                message.DragEventArgs.DragUIOverride.IsCaptionVisible = false;
            }
        }
    }
}
