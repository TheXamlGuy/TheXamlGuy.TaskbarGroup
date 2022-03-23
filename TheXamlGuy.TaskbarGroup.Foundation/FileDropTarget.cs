using System.Diagnostics;
using System.Windows;
using System.Windows.Shell;
using TheXamlGuy.TaskbarGroup.Core;
using Microsoft.WindowsAPICodePack.Shell;

namespace TheXamlGuy.TaskbarGroup.Foundation
{
    public class FileDropTarget : IDropTarget<UIElement>
    {
        private UIElement? target;
        private readonly IMessenger messenger;

        public FileDropTarget(IMessenger messenger)
        {
            this.messenger = messenger;
        }

        public void Register(UIElement target)
        {
            if (this.target is not null)
            {
                target.DragOver -= OnDragOver;
                target.DragEnter -= OnDragEnter;
                target.Drop -= OnDrop;
            }

            this.target = target;

            target.DragOver += OnDragOver;
            target.DragEnter += OnDragEnter;
            target.Drop += OnDrop;
        }

        private void OnDrop(object sender, DragEventArgs args)
        {
            String[] fileName = (String[])args.Data.GetFormats();

            var ddd = ShellObjectCollection.FromDataObject((System.Runtime.InteropServices.ComTypes.IDataObject)args.Data);

            //args.Handled = true;
            //var fileName = GetFileName(args.Data);
            //messenger.Publish<FileDropped>();

        }

        private string GetFileName(IDataObject data)
        {
            var filenames = (string[])data.GetData(DataFormats.FileDrop);
            return filenames[0];
        }

        private bool IsFileDrop(IDataObject data)
        {
            return data.GetDataPresent(DataFormats.FileDrop);
        }

        private void OnDragEnter(object sender, DragEventArgs args)
        {
            if (IsFileDrop(args.Data))
            {
                args.Effects = DragDropEffects.Link;
            }
            else
            {
                args.Effects = DragDropEffects.None;
            }
        }

        private void OnDragOver(object sender, DragEventArgs args)
        {
            if (IsFileDrop(args.Data))
            {
                args.Effects = DragDropEffects.Link;
            }
            else
            {
                args.Effects = DragDropEffects.None;
            }
        }
    }
}
