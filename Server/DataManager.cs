using Newtonsoft.Json;

public static class DataManager
{
    public static Dictionary<long, Player> Players = new Dictionary<long, Player>();

    private readonly static string path = "save.gz";

    private static float leftTime = 300;

    public static Player TryCreateUser(long id, string username)
    {
        if (Players.ContainsKey(id))
            return Players[id];

        Player pl = new Player(id, username);

        AddPlayer(pl);

        return pl;
    }

    public static Player GetPlayer(long id)
    { 
        return Players[id];
    }

    public static void AddPlayer(Player pl)
    {
        Players.Add(pl.Id, pl);
    }

    public static void Save(float val)
    {
        leftTime -= val;

        if (leftTime < 0)
        {
            leftTime = 300;

            File.WriteAllText(path, JsonConvert.SerializeObject(Players));
        }
    }

    public static void Load()
    {
        TimeManager.UpdateAction += Save;
        
        if (File.Exists(path) && File.ReadAllText(path) is string data)
        {
            if (JsonConvert.DeserializeObject<Dictionary<long, Player>>(data) is var pl
                && pl != null)
            {
                Players = pl;
            }
        }
    }
}