public static class TimeManager
{
    public static Action<float> UpdateAction;

    private static Timer timer;

    public static void Update(object? obj)
    {
        UpdateAction?.Invoke(1);
    }
    
    static TimeManager()
    {
        timer = new Timer(new TimerCallback(Update), null, 0, 1000);
    }
}