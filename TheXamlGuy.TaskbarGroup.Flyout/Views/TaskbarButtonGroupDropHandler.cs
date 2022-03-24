using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using TheXamlGuy.TaskbarGroup.Core;
using TheXamlGuy.TaskbarGroup.Flyout.Foundation;
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage;
using Windows.Storage.FileProperties;

namespace TheXamlGuy.TaskbarGroup.Flyout
{
    public class TaskbarButtonGroupDropHandler : IDropHandler<TaskbarButtonGroupView>
    {
        private readonly IMediator mediator;

        public TaskbarButtonGroupDropHandler(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async void Handle(Drop<TaskbarButtonGroupView> message)
        {
            if (message.DropEventArgs.DataView.Contains(StandardDataFormats.StorageItems))
            {
                var items = await message.DropEventArgs.DataView.GetStorageItemsAsync();
                foreach (IStorageItem storageItem in items)
                {
                    if ((await storageItem.RetrievePropertiesAsync("System.AppUserModel.ID"))
                             .TryGetValue("System.AppUserModel.ID", out var appUserModelId)
                             || File.Exists(storageItem.Path))
                    {
                        if (storageItem is IStorageItemProperties storageItemProperties)
                        {
                            using var thumbnail = await storageItemProperties.GetThumbnailAsync(ThumbnailMode.SingleItem, 94);
                            using var stream = thumbnail.AsStreamForWrite();
                            mediator.Handle(new IconStorage(appUserModelId is not null ? $"{appUserModelId}" : storageItem.Path, stream));
                        }
                    }
                }
            }
        }
    }
}
