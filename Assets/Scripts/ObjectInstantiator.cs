using UnityEngine;

public class ObjectInstantiator : MonoBehaviour
{
    public GameObject Prefab;
    public Transform Parent;
    public bool DetachFromParent = true;

    public void Instantiate()
    {
        GameObject instance = Instantiate(Prefab, Parent);
        if (DetachFromParent)
            instance.transform.SetParent(null);
    }
}
