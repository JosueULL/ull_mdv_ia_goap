public sealed class GWorld
{
    private static readonly GWorld mInstance = new GWorld();
    private static WorldStates mWorld;

    static GWorld()
    {
        mWorld = new WorldStates();
    }

    private GWorld()
    {
    }

    public static GWorld Instance
    {
        get { return mInstance; }
    }

    public WorldStates GetWorld()
    {
        return mWorld;
    }
}
