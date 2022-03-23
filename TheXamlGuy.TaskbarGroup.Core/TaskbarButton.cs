namespace TheXamlGuy.TaskbarGroup.Core
{
    public class TaskbarButton : ITaskbarButton
    {
        private readonly IMessenger messenger;
        private readonly IDisposer disposer;
        private bool isWithinBounds;
        private bool isDrag;

        public TaskbarButton(IMessenger messenger,
            IDisposer disposer,
            string name, 
            TaskbarButtonBounds bounds)
        {
            this.messenger = messenger;
            this.disposer = disposer;
            Name = name;
            Bounds = bounds;

            disposer.Add(this, messenger.Subscribe<PointerReleased>(OnPointerReleased));
            disposer.Add(this, messenger.Subscribe<PointerMoved>(OnPointerMoved));
            disposer.Add(this, messenger.Subscribe<PointerDrag>(OnPointerDrag));
        }

        public TaskbarButtonBounds Bounds { get; internal set; }

        public string Name { get; internal set; }

        public void Dispose()
        {
            disposer.Dispose(this);
            GC.SuppressFinalize(this);
        }

        private bool IsWithinBounds(PointerLocation args)
        {
            if (args.X >= Bounds.X
                && args.X <= Bounds.X + Bounds.Width
                && args.Y >= Bounds.Y
                && args.Y <= Bounds.Y + Bounds.Height)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void OnPointerDrag(PointerDrag args)
        {
            if (isWithinBounds)
            {
                if (isDrag)
                {
                    messenger.Send(new TaskbarButtonDragOver(this));
                }
                else
                {
                    messenger.Send(new TaskbarButtonDragEnter(this));
                }
                
                isDrag = true;
            }
            else
            {
                isDrag = false;
            }
        }

        private void OnPointerMoved(PointerMoved args)
        {
            if (IsWithinBounds(args.Location))
            {
                if (isWithinBounds)
                {
                    return;
                }

                isWithinBounds = true;
                messenger.Send(new TaskbarButtonEntered(this));
            }
            else
            {
                isDrag = false;
                isWithinBounds = false;
            }
        }

        private void OnPointerReleased(PointerReleased args)
        {
            if (!isDrag && isWithinBounds)
            {
                messenger.Send(new TaskbarButtonInvoked(this));
            }

            if (isDrag)
            {
                isDrag = false;
            }
        }
    }
}
