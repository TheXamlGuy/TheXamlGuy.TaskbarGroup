using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TheXamlGuy.TaskbarGroup.Flyout.Foundation;
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage;

namespace TheXamlGuy.TaskbarGroup.Flyout
{
    public class TaskbarButtonGroupDropHandler : IDropHandler<TaskbarButtonGroupView>
    {
        public async Task Handle(Drop<TaskbarButtonGroupView> message, CancellationToken canellationToken = default)
        {
            if (message.DropEventArgs.DataView.Contains(StandardDataFormats.StorageItems))
            {
                var items = await message.DropEventArgs.DataView.GetStorageItemsAsync();
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
}
