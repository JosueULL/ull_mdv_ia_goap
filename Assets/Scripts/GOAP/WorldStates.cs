using System.Collections.Generic;
using UnityEngine.Serialization;

[System.Serializable]
public class WorldState
{
    public GKey Key;
    public int Value;
}

public class WorldStates
{
    public Dictionary<GKey, int> States;

    public WorldStates()
    {
        States = new Dictionary<GKey, int>();
    }

    public bool HasState(GKey key)
    {
        return States.ContainsKey(key);
    }

    void AddState(GKey key, int value)
    {
        States.Add(key, value);
    }

    public void ModifyState(GKey key, int value)
    {
        if (States.ContainsKey(key))
        {
            States[key] += value;
            if (States[key] <= 0)
                RemoveState(key);
        }
        else
            States.Add(key, value);
    }

    public void RemoveState(GKey key)
    {
        if (States.ContainsKey(key))
            States.Remove(key);
    }

    public void SetState(GKey key, int value)
    {
        if (States.ContainsKey(key))
            States[key] = value;
        else
            States.Add(key, value);
    }

    public Dictionary<GKey, int> GetStates()
    {
        return States;
    }
}
