using Newtonsoft.Json;

public class Pet : Entity
{
    [JsonProperty] public string Photo { get; protected set; }
    [JsonProperty] public string Description { get; }

    //[JsonProperty] public Item? Weapon { get; protected set; }
    //[JsonProperty] public Item? Armor { get; protected set; }

    [JsonProperty] public int Exp { get; protected set; }
    [JsonProperty] public int MaxExp { get; protected set; } = 50;

    [JsonProperty] protected float regenTimerLeft, regenTimer = 60;

    /*public void EquipWeapon(Item item)
    { 
        Weapon = item;
    }

    public void EquipArmor(Item item)
    {
        Armor = item;
    }*/

    public override void Update(float delta)
    {
        if (HP >= MaxHP) return;

        regenTimerLeft -= delta;

        if (regenTimerLeft <= 0)
        {
            regenTimerLeft = regenTimer;
            HP = Global.MultiFloat(MaxHP, 0.1f);

            if (HP > MaxHP) HP = MaxHP;
        }
    }

    public void AddExp(int exp)
    {
        Exp += exp;

        while(Exp >= MaxExp)
        {
            Exp -= MaxExp;
            MaxExp = (int)(MaxExp * 1.5f);
            LevelUp();
        }
    }

    private void LevelUp()
    {
        Level++;

        MaxHP = Global.MultiFloat(MaxHP, 1.1f);
        Attack = Global.MultiFloat(Attack, 1.05f) + 1;
        Accuracy = Global.MultiFloat(Accuracy, 1.05f) + 1;
        Defense = Global.MultiFloat(Defense, 1.07f) + 1;
        Initiative = Global.MultiFloat(Initiative, 1.15f) + 1;
    }

    public override string GetInfo()
    {
        return $"{Name} лвл: {Level} ({Exp}/{MaxExp} опыт)\n" + GetInfoWithoutName();
    }

    public Pet(Pet pet) : base(pet) 
    {
        Photo = pet.Photo;
        Description = pet.Description;
    }

    public Pet(long id, string name, int maxHP, int attack, int accuracy, int defense, int initiative, string desc, string photo) : base(id, name, maxHP, attack, accuracy, defense, initiative)
    {
        Photo = photo;
        Description = desc;
    }

    public Pet() : base() { }
}