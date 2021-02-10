using UnityEngine;

public class GGuard : GAgent
{
    void Awake()
    {
        goals.Add(new SubGoal("FindUnprotected", 1, false), 1);
        goals.Add(new SubGoal("KillVictim", 1, false), 2);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Pirate"))
        {
            GameObject victim = inventory.GetItem("Victim");
            if (!victim || Vector3.Distance(transform.position, victim.transform.position) > Vector3.Distance(transform.position, other.transform.position))
            {
                // Check if the pirate is attacking someone
                GAgent otherAgent = other.GetComponent<GAgent>();
                if (otherAgent.inventory.GetItem("Victim") != null)
                {
                    inventory.AddItem("Victim", other.gameObject);
                    beliefs.SetState("FoundVictim",1);
                }
            }
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Pirate"))
        {
            GameObject victim = inventory.GetItem("Victim");
            if (victim == other.gameObject)
            {
                inventory.RemoveItem("Victim");
                beliefs.RemoveState("FoundVictim");
            }
        }
    }
}
