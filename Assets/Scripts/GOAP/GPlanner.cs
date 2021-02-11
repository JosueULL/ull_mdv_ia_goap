using System.Collections.Generic;

public class GPlanner
{
    class Node
    {

        public Node Parent;
        public float Cost;
        public Dictionary<GKey, int> State;
        public GAction Action;

        // Constructor
        public Node(Node parent, float cost, Dictionary<GKey, int> allStates, GAction action)
        {

            this.Parent = parent;
            this.Cost = cost;
            this.State = new Dictionary<GKey, int>(allStates);
            this.Action = action;
        }
        public Node(Node parent, float cost, Dictionary<GKey, int> allStates, Dictionary<GKey, int> beliefstates, GAction action)
        {

            this.Parent = parent;
            this.Cost = cost;
            this.State = new Dictionary<GKey, int>(allStates);
            foreach (KeyValuePair<GKey, int> b in beliefstates)
                if (!this.State.ContainsKey(b.Key))
                    this.State.Add(b.Key, b.Value);
            this.Action = action;
        }
    }


    public Queue<GAction> Plan(List<GAction> actions, Dictionary<GKey, int> goal, WorldStates states)
    {

        List<GAction> usableActions = new List<GAction>();

        foreach (GAction a in actions)
        {
            if (a.IsAchievable())
            {
                usableActions.Add(a);
            }
        }

        List<Node> leaves = new List<Node>();
        Node start = new Node(null, 0.0f, GWorld.Instance.GetWorld().GetStates(), states.GetStates(), null);

        bool success = BuildGraph(start, leaves, usableActions, goal);

        if (!success)
        {
            return null;
        }

        Node cheapest = null;
        foreach (Node leaf in leaves)
        {
            if (cheapest == null)
            {
                cheapest = leaf;
            }
            else if (leaf.Cost < cheapest.Cost)
            {
                cheapest = leaf;
            }
        }
        List<GAction> result = new List<GAction>();
        Node n = cheapest;

        while (n != null)
        {
            if (n.Action != null)
            {
                result.Insert(0, n.Action);
            }
            n = n.Parent;
        }

        Queue<GAction> queue = new Queue<GAction>();

        foreach (GAction a in result)
        {
            queue.Enqueue(a);
        }

        /*
        Debug.Log("The Plan is: ");
        foreach (GAction a in queue)
        {
            Debug.Log("Q: " + a.actionName);
        }
        */
        return queue;
    }

    private bool BuildGraph(Node parent, List<Node> leaves, List<GAction> usableActions, Dictionary<GKey, int> goal)
    {

        bool foundPath = false;
        foreach (GAction action in usableActions)
        {
            if (action.IsAchievableGiven(parent.State))
            {
                Dictionary<GKey, int> currentState = new Dictionary<GKey, int>(parent.State);
                foreach (KeyValuePair<GKey, int> eff in action.Effects)
                {
                    if (!currentState.ContainsKey(eff.Key))  
                        currentState.Add(eff.Key, eff.Value);
                    
                }

                Node node = new Node(parent, parent.Cost + action.Cost, currentState, action);

                if (GoalAchieved(goal, currentState))
                {
                    leaves.Add(node);
                    foundPath = true;
                }
                else
                {
                    List<GAction> subset = ActionSubset(usableActions, action);
                    bool found = BuildGraph(node, leaves, subset, goal);
                    if (found)
                        foundPath = true;
                }
            }
        }
        return foundPath;
    }

    private List<GAction> ActionSubset(List<GAction> actions, GAction removeMe)
    {
        List<GAction> subset = new List<GAction>();
        foreach (GAction a in actions)
        {
            if (!a.Equals(removeMe))
                subset.Add(a);
        }
        return subset;
    }

    private bool GoalAchieved(Dictionary<GKey, int> goal, Dictionary<GKey, int> state)
    {

        foreach (KeyValuePair<GKey, int> g in goal)
        {
            if (!state.ContainsKey(g.Key))
                return false;
        }
        return true;
    }
}
