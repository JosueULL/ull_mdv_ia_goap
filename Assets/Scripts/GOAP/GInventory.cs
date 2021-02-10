using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GInventory
{
    public Dictionary<string, GameObject> items = new Dictionary<string, GameObject>();

    public void AddItem(string key, GameObject i)
    {
        items[key] = i;
    }

    public GameObject GetItem(string key)
    {
        if (items.ContainsKey(key))
            return items[key];
        else
            return null;
    }
    public void RemoveItem(string key)
    {
        items.Remove(key);
    }
}
