namespace GMNetworkFramework.Server.Enums
{
    public enum RequestFlag : ushort
    {
        ForAll = ushort.MaxValue,
        Undefined = 0,
        Unhandled = 1,
        Disconnect = 2,
    }
}
