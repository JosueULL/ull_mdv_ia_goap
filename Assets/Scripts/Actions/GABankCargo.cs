using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GABankCargo : GActionGoToTarget
{

    public override bool PostPerform()
    {
        
        agent.GetComponent<Cargo>().Clear();
        return base.PostPerform();
    }
}
