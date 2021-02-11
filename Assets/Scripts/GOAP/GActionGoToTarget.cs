using UnityEngine;
using UnityEngine.AI;

public class GActionGoToTarget : GAction
{
    public GameObject Target;
    public string TargetTag;
    public float StopDistance = 1;

    protected NavMeshAgent mNavMeshAgent;

    public override void Awake()
    {
        base.Awake();
        mNavMeshAgent = this.GetComponent<NavMeshAgent>();
    }

    public override bool PostPerform()
    {
        return true;
    }

    public override bool PrePerform()
    {
        base.PrePerform();

        if (Target == null && TargetTag != "")
            Target = GameObject.FindWithTag(TargetTag);

        if (Target != null)
        {
            mNavMeshAgent.SetDestination(Target.transform.position);
            return true;
        }

        return false;
    }

    public override void Perform()
    {
        if (!Target)
        {
            Running = false;
            return;
        }

        float distanceToTarget = Vector3.Distance(Target.transform.position, this.transform.position);
        if (distanceToTarget <= StopDistance)
        {
            Running = false;
        }
        else
        {
            mNavMeshAgent.SetDestination(Target.transform.position);
        }
    }

    protected void PickRandomWithTag(string tag)
    {
        GameObject[] wanderPoints = GameObject.FindGameObjectsWithTag(tag);
        Target = wanderPoints[Random.Range(0, wanderPoints.Length)];
    }

}