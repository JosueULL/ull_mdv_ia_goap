using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public int MaxAmount;
    public Text Text;
    public UnityEvent OnHealthChanged;
    public UnityEvent OnHealthReduced;
    public UnityEvent OnHealthDepleted;

    private int mAmount;

    public float CurrentAmount { get { return mAmount; } }

    // --------------------------------------------------------------------

    private void OnEnable()
    {
        RefillHealth(MaxAmount);
    }

    // --------------------------------------------------------------------

    public void ReduceHealth(int amount)
    {
        mAmount = Mathf.Clamp(mAmount - amount,0,MaxAmount);

        OnHealthChanged.Invoke();
        OnHealthReduced.Invoke();
        if (mAmount <= 0)
            OnHealthDepleted.Invoke();
    }

    // --------------------------------------------------------------------

    public void RefillHealth(int amount)
    {
        mAmount = Mathf.Clamp(mAmount + amount, 0, MaxAmount);
        OnHealthChanged.Invoke();
    }
}
