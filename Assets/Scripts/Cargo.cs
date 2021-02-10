using UnityEngine;

public class Cargo : MonoBehaviour
{
    public float MaxAmount;
    public float Amount;

    private GAgent mAgent;

    private void Awake()
    {
        mAgent = GetComponent<GAgent>();
    }

    public void Add(float amount)
    {
        Amount += amount;
        if (Amount >= MaxAmount)
            mAgent.beliefs.SetState("TooMuchCargo", 1);
    }

    public void Clear()
    {
        Amount = 0;
        mAgent.beliefs.RemoveState("TooMuchCargo");
    }
}
