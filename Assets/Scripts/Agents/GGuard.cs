using UnityEngine;

public class GGuard : GAgent
{
    public GKey FoundVictimKey;
    public GInventoryKey VictimKey;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(Tags.Pirate))
        {
            GameObject victim = Inventory.GetItem(VictimKey);
            if (!victim || Vector3.Distance(transform.position, victim.transform.position) > Vector3.Distance(transform.position, other.transform.position))
            {
                // Check if the pirate is attacking someone
                GAgent otherAgent = other.GetComponent<GAgent>();
                if (otherAgent.Inventory.GetItem(VictimKey) != null)
                {
                    Inventory.AddItem(VictimKey, other.gameObject);
                    Beliefs.SetState(FoundVictimKey, 1);
                }
            }
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Tags.Pirate))
        {
            GameObject victim = Inventory.GetItem(VictimKey);
            if (victim == other.gameObject)
            {
                Inventory.RemoveItem(VictimKey);
                Beliefs.RemoveState(FoundVictimKey);
            }
        }
    }
}
