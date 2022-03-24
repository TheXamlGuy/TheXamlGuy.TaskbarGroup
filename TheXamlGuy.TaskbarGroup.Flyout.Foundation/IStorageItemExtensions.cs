using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Storage;

namespace TheXamlGuy.TaskbarGroup.Flyout.Foundation
{
    public static class IStorageItemExtensions
    {
        public static async Task<IDictionary<string, object>> RetrievePropertiesAsync(this IStorageItem storageFile, params string[] paramaters)
        {
            return await (await storageFile.GetBasicPropertiesAsync()).RetrievePropertiesAsync(paramaters);
        }
    }
}
