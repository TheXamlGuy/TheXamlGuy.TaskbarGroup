namespace TheXamlGuy.TaskbarGroup.Core
{
    public class TaskbarButton : ITaskbarButton
    {
        private readonly IMessenger messenger;
        private readonly IDisposer disposer;
        private bool isWithinBounds;
        private bool isDrag;

        public TaskbarButton(string name,
            Rect rect,
            IMessenger messenger,
            IDisposer disposer)
        {
            this.messenger = messenger;
            this.disposer = disposer;
            Name = name;
            Rect = rect;

            disposer.Add(this, messenger.Subscribe<PointerReleased>(OnPointerReleased));
            disposer.Add(this, messenger.Subscribe<PointerMoved>(OnPointerMoved));
            disposer.Add(this, messenger.Subscribe<PointerDrag>(OnPointerDrag));
        }

        public Rect Rect { get; internal set; }

        public string Name { get; internal set; }

        public void Dispose()
        {
            disposer.Dispose(this);
            GC.SuppressFinalize(this);
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
            if (args.Location.IsWithinBounds(Rect))
            {
                if (isWithinBounds)
                {
                    return;
                }

                isWithinBounds = true;
                messenger.Send(new TaskbarButtonEnter(this));
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
