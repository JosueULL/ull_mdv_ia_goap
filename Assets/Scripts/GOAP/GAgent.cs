using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GAgentSubGoal
{
    public Dictionary<GKey, int> SubGoals;
    public bool Remove;
}

public class GAgent : MonoBehaviour
{
    [System.Serializable]
    public class GoalEntry
    {
        public WorldState[] States;
        public int Priority;
        public bool Remove;
    }

    public GoalEntry[] Goals;
    public WorldStates Beliefs = new WorldStates();
    public GInventory Inventory = new GInventory();

    private Dictionary<GAgentSubGoal, int> mGoals = new Dictionary<GAgentSubGoal, int>();
    private GPlanner mPlanner;
    private Queue<GAction> mActionQueue;
    private GAction mCurrentAction;
    private GAgentSubGoal mCurrentGoal;
    private List<GAction> mActions = new List<GAction>();

#if UNITY_EDITOR
    // Used by GAgentDebugInfo --------
    public GAction CurrentAction => mCurrentAction;
    public List<GAction> CurrentActions => mActions;
    public Dictionary<GAgentSubGoal, int> CurrentGoals => mGoals;
#endif

    public void Start()
    {
        foreach(GoalEntry ge in Goals)
        {
            GAgentSubGoal subGoal = new GAgentSubGoal();
            subGoal.SubGoals = new Dictionary<GKey, int>();
            foreach (WorldState gs in ge.States)
            {
                subGoal.SubGoals.Add(gs.Key, gs.Value);
            }
            subGoal.Remove = ge.Remove;
            mGoals.Add(subGoal, ge.Priority);
        }

        GAction[] acts = this.GetComponents<GAction>();
        foreach (GAction a in acts)
            mActions.Add(a);
    }

    void LateUpdate()
    {
        if (mCurrentAction != null && mCurrentAction.Running)
        {
            mCurrentAction.Perform();
            if (!mCurrentAction.Running)
            {
                mCurrentAction.PostPerform();
                mCurrentAction = null;
            }
           else
            {
                return;
            }
        }

        if (mPlanner == null || mActionQueue == null)
        {
            mPlanner = new GPlanner();

            var sortedGoals = from entry in mGoals orderby entry.Value descending select entry;

            foreach (KeyValuePair<GAgentSubGoal, int> sg in sortedGoals)
            {
                mActionQueue = mPlanner.Plan(mActions, sg.Key.SubGoals, Beliefs);
                if (mActionQueue != null)
                {
                    mCurrentGoal = sg.Key;
                    break;
                }
            }
        }

        if (mActionQueue != null && mActionQueue.Count == 0)
        {
            if (mCurrentGoal.Remove)
            {
                mGoals.Remove(mCurrentGoal);
            }
            mPlanner = null;
        }

        if (mActionQueue != null && mActionQueue.Count > 0)
        {
            GAction newAction = mActionQueue.Dequeue();
            //if (newAction != currentAction)
           // {
                mCurrentAction = newAction;

                if (!mCurrentAction.PrePerform())
                    mActionQueue = null;
           // }

        }

    }
}
