public static class LogManager
{
    public enum LogType
    {
        DoHit = 0,
        Miss = 1
    }

    private static List<string> log = new List<string>();

    public static void AddLog(string str)
    { 
        
    }

    public static void AddLogHit(string name, int dmg)
    { 
        
    }

    public static void AddLog(string str, LogType type)
    {
        /*switch(type)
        {
            case LogType.DoHit: 
        }*/
    }
}