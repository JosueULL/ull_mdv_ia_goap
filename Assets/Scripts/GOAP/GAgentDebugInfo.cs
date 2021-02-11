using UnityEngine;

#if UNITY_EDITOR

[ExecuteInEditMode]
public class GAgentDebugInfo : MonoBehaviour
{
    private GAgent mAgent;

    void Start()
    {
        mAgent = this.GetComponent<GAgent>();
    }
}

#endif
