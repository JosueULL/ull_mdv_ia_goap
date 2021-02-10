using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CargoUI : MonoBehaviour
{
    public Cargo Cargo;
    public Image Fill;

    public void Update()
    {
        Fill.fillAmount = Cargo.Amount / Cargo.MaxAmount;
    }
}
