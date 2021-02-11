using UnityEngine;

public class GPirate : GAgent
{
    public GKey FoundVictimKey;
    public GKey CanSeeCargoKey;

    public GInventoryKey VictimKey;
    public GInventoryKey CargoKey;

    public GameObject CargoPrefab;

    private Cargo mCargo;

    private void Awake()
    {
        mCargo = GetComponent<Cargo>();
        
        GetComponent<Health>().OnHealthDepleted.AddListener(OnDeath);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(Tags.Victim))
        {
            GameObject victim = Inventory.GetItem(VictimKey);
            if (!victim || Vector3.Distance(transform.position, victim.transform.position) > Vector3.Distance(transform.position, other.transform.position))
            {
                Inventory.AddItem(VictimKey, other.gameObject);
                Beliefs.SetState(FoundVictimKey, 1);
            }
        }

        if (other.CompareTag(Tags.Cargo))
        {
            Inventory.AddItem(CargoKey, other.gameObject);
            Beliefs.SetState(CanSeeCargoKey, 1);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Tags.Victim))
        {
            GameObject victim = Inventory.GetItem(VictimKey);
            if (victim == other.gameObject)
            {
                Inventory.RemoveItem(VictimKey);
                Beliefs.RemoveState(FoundVictimKey);
            }
        }
    }

    private void OnDeath()
    {
        float cargoAmount = mCargo.Amount;
        if (cargoAmount > 0) 
        {
            GameObject cargo = Instantiate(CargoPrefab, transform.position, transform.rotation);
            cargo.GetComponentInChildren<Loot>().Value = cargoAmount;
        }
    }
}
