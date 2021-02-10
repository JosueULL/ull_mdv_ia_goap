using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject Prefab;
    public float IntervalMin;
    public float IntervalMax;

    void OnEnable()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (gameObject.activeSelf)
        {
            yield return new WaitForSeconds(Random.Range(IntervalMin, IntervalMax));
            Instantiate(Prefab, transform.position, transform.rotation);
        }
    }
}
