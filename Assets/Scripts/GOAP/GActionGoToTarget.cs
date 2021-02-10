using System.Collections;
using UnityEngine;

public class GActionGoToTarget : GAction
{
    public GameObject target;
    public string targetTag;
    public float stopDistance = 1;

    public override bool PostPerform()
    {
        return true;
    }

    public override bool PrePerform()
    {
        base.PrePerform();

        if (target == null && targetTag != "")
            target = GameObject.FindWithTag(targetTag);

        if (target != null)
        {
            agent.SetDestination(target.transform.position);
            return true;
        }

        return false;
    }

    public override void Perform()
    {
        if (!target)
        {
            running = false;
            return;
        }

        // si el navmesh no está calculando bien el remaining distance, se puede
        //calcular la distancia a mano.
        float distanceToTarget = Vector3.Distance(target.transform.position, this.transform.position);
        //if (currentAction.agent.hasPath && distanceToTarget < 2f) //currentAction.agent.remainingDistance < 2f)
        if (distanceToTarget <= stopDistance)
        {
            running = false;
        }
        else
        {
            agent.SetDestination(target.transform.position);
        }
    }


    protected void PickRandomWithTag(string tag)
    {
        GameObject[] wanderPoints = GameObject.FindGameObjectsWithTag(tag);
        target = wanderPoints[Random.Range(0, wanderPoints.Length)];
    }

}