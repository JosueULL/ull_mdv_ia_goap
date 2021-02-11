using System.Collections.Generic;
using UnityEngine;

public class GInventory
{
    public Dictionary<GInventoryKey, GameObject> items = new Dictionary<GInventoryKey, GameObject>();

    public void AddItem(GInventoryKey key, GameObject i)
    {
        items[key] = i;
    }

    public GameObject GetItem(GInventoryKey key)
    {
        if (items.ContainsKey(key))
            return items[key];
        else
            return null;
    }
    public void RemoveItem(GInventoryKey key)
    {
        items.Remove(key);
    }
}
