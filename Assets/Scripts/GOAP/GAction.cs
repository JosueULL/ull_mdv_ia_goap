using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GAction : MonoBehaviour
{
    public float Cost = 1.0f;
    
    public WorldState[] PreConditions;
    public WorldState[] AfterEffects;
    
    public Dictionary<GKey, int> Preconditions;
    public Dictionary<GKey, int> Effects;
    public bool Running = false;

    protected GAgent mAgent;

    public GInventory Inventory => mAgent.Inventory;
    public WorldStates Beliefs => mAgent.Beliefs;

    public GAction()
    {
        Preconditions = new Dictionary<GKey, int>();
        Effects = new Dictionary<GKey, int>();
    }

    public virtual void Awake()
    {
        if (PreConditions != null)
            foreach (WorldState w in PreConditions)
            {
                Preconditions.Add(w.Key, w.Value);
            }

        if (AfterEffects != null)
            foreach (WorldState w in AfterEffects)
            {
                Effects.Add(w.Key, w.Value);
            }

        mAgent = GetComponent<GAgent>();
    }

    public bool IsAchievable()
    {
        return true;
    }

    public bool IsAchievableGiven(Dictionary<GKey, int> conditions)
    {
        foreach (KeyValuePair<GKey, int> p in Preconditions)
        {
            if (!conditions.ContainsKey(p.Key))
                return false;
        }
        return true;
    }

    public virtual bool PrePerform() 
    { 
        Running = true;
        return true;
    }

    public abstract bool PostPerform();
    public abstract void Perform();
}
