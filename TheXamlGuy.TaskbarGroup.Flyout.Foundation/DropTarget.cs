using System;
using System.Collections.Generic;
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage;
using Windows.UI.Xaml;

namespace TheXamlGuy.TaskbarGroup.Flyout.Foundation
{
    public class DropTarget
    {
        public void Initialize(UIElement target)
        {
            target.DragOver += OnDragOver;
            target.Drop += OnDrop;
        }

        private async void OnDrop(object sender, DragEventArgs args)
        {
            if (args.DataView.Contains(StandardDataFormats.StorageItems))
            {
                var items = await args.DataView.GetStorageItemsAsync();
                if (items.Count > 0)
                {
                    foreach (var storageItem in items)
                    {
                        if (storageItem is StorageFile storageFile)
                        {
                            if (storageFile.Path is { Length: > 0 })
                            {

                            }
                            else
                            {
                                var properties = await storageFile.Properties.RetrievePropertiesAsync(new List<string>
                                {
                                    "System.AppUserModel.ID"
                                });

                                var appUserModelId = properties["System.AppUserModel.ID"];
                                if (appUserModelId is not null)
                                {

                                }
                            }
                        }

                        if (storageItem is StorageFolder storageFolder)
                        {

                        }
                    }
                }
            }
        }

        private void OnDragOver(object sender, DragEventArgs args)
        {
            args.AcceptedOperation = DataPackageOperation.Link;
        }
    }
}
