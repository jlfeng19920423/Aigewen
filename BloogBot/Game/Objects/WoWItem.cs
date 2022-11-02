using BloogBot.Game.Enums;
using System;


namespace BloogBot.Game.Objects
{
    public class WoWItem : WoWObject
    {
        internal WoWItem(
            IntPtr pointer,
            CGGuid guid,
            ObjectType objectType)
            : base(pointer, guid, objectType)
        {
            // TODO
            //var addr = Functions.GetItemCacheEntry(ItemId);
            //if (addr != IntPtr.Zero)
            //{
            //    var itemCacheEntry = MemoryManager.ReadItemCacheEntry(addr);
            //    Info = new ItemCacheInfo(itemCacheEntry);
            //}
        }
    }
}
