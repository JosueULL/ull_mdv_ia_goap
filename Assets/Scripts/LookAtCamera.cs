using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private Transform CameraTransform;

    void Start()
    {
        CameraTransform = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(CameraTransform.position, CameraTransform.up);
    }
}
