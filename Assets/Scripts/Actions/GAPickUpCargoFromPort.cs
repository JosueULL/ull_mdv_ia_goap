using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GAPickUpCargoFromPort : GActionGoToTarget
{
    public override bool PrePerform()
    {
        PickRandomWithTag("Port");
        return base.PrePerform();
    }

    public override bool PostPerform()
    {
        return base.PostPerform();
    }
}
