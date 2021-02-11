public class GABankCargo : GActionGoToTarget
{
    private Cargo mCargo;

    public override void Awake()
    {
        base.Awake();
        mCargo = GetComponent<Cargo>();
    }

    public override bool PostPerform()
    {
        mCargo.Clear();
        return base.PostPerform();
    }
}
