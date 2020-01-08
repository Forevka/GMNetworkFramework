namespace GMLoggerBackend.Enums
{
    public enum RequestFlag : ushort
    {
        ForAll = ushort.MaxValue,
        Undefined = 0,
        Disconnect = 1,
        NewConnection = 2000,
        Ping = 2004,
        PingResponse = 2005,
        Log = 2006,
    }
}
