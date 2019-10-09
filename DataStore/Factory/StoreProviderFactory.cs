using System;
using System.Collections.Generic;
using System.Text;

namespace DataStore
{
    public class StoreProviderFactory
    {
        public static IStore GetStoreProvider(StoreProviders storeProviders)
        {
            switch(storeProviders)
            {
                case StoreProviders.File:
                    return new FileStore();
                case StoreProviders.Memory:
                    return new MemoryStore();
                default:
                    return new MemoryStore();
            }
        }
    }

    public enum StoreProviders
    {
        Memory,
        File
    }
}
