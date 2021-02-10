using UnityEngine;

public class GPirate : GAgent
{
    public GameObject CargoPrefab;

    private void Awake()
    {
        goals.Add(new SubGoal("FindVictim", 1, false), 1);
        goals.Add(new SubGoal("KillVictim", 1, false), 2);
        goals.Add(new SubGoal("PickupCargo", 1, false), 3);
        goals.Add(new SubGoal("BankCargo", 1, false), 4);

        GetComponent<Health>().OnHealthDepleted.AddListener(OnDeath);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Victim"))
        {
            GameObject victim = inventory.GetItem("Victim");
            if (!victim || Vector3.Distance(transform.position, victim.transform.position) > Vector3.Distance(transform.position, other.transform.position))
            {
                inventory.AddItem("Victim", other.gameObject);
                beliefs.SetState("FoundVictim", 1);
            }
        }

        if (other.CompareTag("Cargo"))
        {
            inventory.AddItem("TargetCargo", other.gameObject);
            beliefs.SetState("CanSeeCargo", 1);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Victim"))
        {
            GameObject victim = inventory.GetItem("Victim");
            if (victim == other.gameObject)
            {
                inventory.RemoveItem("Victim");
                beliefs.RemoveState("FoundVictim");
            }
        }
    }

    private void OnDeath()
    {
        float cargoAmount = GetComponent<Cargo>().Amount;
        if (cargoAmount > 0) 
        {
            GameObject cargo = Instantiate(CargoPrefab, transform.position, transform.rotation);
            cargo.GetComponentInChildren<Loot>().Value = cargoAmount;
        }
    }
}
