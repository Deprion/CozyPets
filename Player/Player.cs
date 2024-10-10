using Newtonsoft.Json;
using System.Runtime.Serialization;

public class Player : Behaviour
{
    public long Id;
    public int Energy = 50;
    public int MaxEnergy = 50;

    public int Coins = 50;

    public List<Pet> Pets = new List<Pet>();

    [JsonProperty] 
    private int chosedPet;

    [JsonIgnore]
    public int ChosedPet
    {
        get 
        {
            if (chosedPet >= Pets.Count)
                chosedPet = 0;
            return chosedPet;
        }
        set
        {
            if (value > 0 && value < Pets.Count)
                chosedPet = value;
        }
    }

    [JsonProperty] 
    private float energyTimerLeft, energyTimer = 60;

    [JsonIgnore] 
    public MessageHandler MsgHandler { get; set; } = MessageManager.GetHandler("Menu");

    public Player(long id, string name) : base(name)
    {
        Id = id;

        MsgHandler = MessageManager.GetHandler("Introduction");
    }

    public Player() : base()
    {
        
    }

    public void AddPet(Pet pet)
    {
        Pets.Add(pet);
    }

    public override void Update(float delta)
    {
        if (Energy >= MaxEnergy) return;

        energyTimerLeft -= delta;

        if (energyTimerLeft <= 0)
        {
            energyTimerLeft = energyTimer;
            Energy++;
        }
    }

    [OnDeserialized]
    internal void OnDeserializedMethod(StreamingContext context)
    {
        if (Pets.Count == 0)
            MsgHandler = MessageManager.GetHandler("Introduction");
    }
}