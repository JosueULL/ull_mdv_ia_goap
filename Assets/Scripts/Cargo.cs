using UnityEngine;

public class Cargo : MonoBehaviour
{
    public GKey TooMuchCargoKey;
    public float MaxAmount;
    public float Amount;

    private GAgent mAgent;

    private void Awake()
    {
        mAgent = GetComponent<GAgent>();
    }

    public void Add(float amount)
    {
        Debug.Log("Added cargo : " + amount);
        Amount += amount;
        if (Amount >= MaxAmount)
            mAgent.Beliefs.SetState(TooMuchCargoKey, 1);
    }

    public void Clear()
    {
        Amount = 0;
        mAgent.Beliefs.RemoveState(TooMuchCargoKey);
    }
}
