using Newtonsoft.Json;

public abstract class Behaviour : IDisposable
{
    public string Name { get; set; } = "";

    public abstract void Update(float delta);

    [JsonProperty] public bool IsSub { get; protected set; } = true;

    public void Dispose()
    {
        if (IsSub) TimeManager.UpdateAction -= Update;
    }

    public override string ToString()
    {
        return Name;
    }

    public Behaviour(string name, bool withoutSub)
    {
        Name = name;
        IsSub = false;
    }

    public Behaviour(bool withoutSub)
    {
        IsSub = false;
    }

    public Behaviour(string name)
    {
        Name = name;
        TimeManager.UpdateAction += Update;
    }

    public Behaviour()
    {
        TimeManager.UpdateAction += Update;
    }
}