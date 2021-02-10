using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Health Health;
    public Image Fill;

    void Update()
    {
        Fill.fillAmount = Health.CurrentAmount / Health.MaxAmount;
    }
}
