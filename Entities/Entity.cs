using Newtonsoft.Json;

public class Entity : Behaviour
{
    [JsonProperty] public long Id { get; protected set; }
    [JsonProperty] public int HP { get; protected set; }
    [JsonProperty] public int MaxHP { get; protected set; }

    [JsonProperty] public int Attack { get; protected set; }
    [JsonProperty] public int Accuracy { get; protected set; }
    [JsonProperty] public int Defense { get; protected set; }
    [JsonProperty] public int Initiative { get; protected set; }
    [JsonProperty] public int Level { get; protected set; } = 1;

    public virtual int GetAttack()
    { 
        return Attack;
    }

    public virtual double GetHitChance(int acc)
    {
        return (double)acc / Defense;
    }

    public virtual void RecieveDamage(int dmg)
    {
        HP -= dmg;
    }

    public virtual void DoHit(Entity enemy)
    {
        double hitChance = enemy.GetHitChance(Accuracy);

        double hitChRnd = Global.random.NextDouble();

        if (hitChance > hitChRnd)
        {
            enemy.RecieveDamage(GetAttack());
        }
    }

    public override void Update(float delta) 
    {
        
    }

    public virtual string GetInfo()
    {
        return $"{Name} {Level} лвл\n" + GetInfoWithoutName();
    }

    protected string GetInfoWithoutName()
    {
        return $"ХП: {HP}/{MaxHP} ❤️\nАтака: {Attack} ⚔️\nТочность: {Accuracy} 🎯" +
            $"\nЗащита: {Defense} 🛡\nИнициатива: {Initiative} 🥾";
    }

    private void TransferStats(Entity ent)
    {
        Name = ent.Name;
        MaxHP = ent.MaxHP;
        HP = MaxHP;
        Attack = ent.Attack;
        Accuracy = ent.Accuracy;
        Defense = ent.Defense;
        Initiative = ent.Initiative;
    }

    private void TransferStats(string name, int maxHP, int attack, int accuracy, int defense, int initiative)
    {
        Name = name;
        MaxHP = maxHP;
        HP = MaxHP;
        Attack = attack;
        Accuracy = accuracy;
        Defense = defense;
        Initiative = initiative;
    }

    public Entity(Entity ent, bool withoutSub) : base(withoutSub)
    {
        Id = ent.Id;
        TransferStats(ent);
    }

    public Entity(Entity ent) : base()
    {
        Id = ent.Id;
        TransferStats(ent);
    }

    public Entity(long id, string name, int maxHP, int attack, int accuracy, int defense, int initiative, bool withoudSub) : base(name, withoudSub)
    {
        Id = id;
        TransferStats(name, maxHP, attack, accuracy, defense, initiative);
    }

    public Entity(long id, string name, int maxHP, int attack, int accuracy, int defense, int initiative) : base(name)
    {
        Id = id;
        TransferStats(name, maxHP, attack, accuracy, defense, initiative);
    }

    public Entity(bool withoutSub) : base(withoutSub)
    {

    }

    public Entity() : base()
    {

    }
}