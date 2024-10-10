public class Boss : Entity
{
    //public Skill BaseSkill { get; private set; }
    //public Skill ExtraSkill { get; private set; }
    
    protected float regenTimerLeft, regenTimer = 10;

    public override void DoHit(Entity enemy)
    {
        double hitChance = enemy.GetHitChance(Accuracy);

        double critChance = hitChance * 0.1;

        double hitChRnd = Global.random.NextDouble();

        if (hitChance > hitChRnd)
        {
            int attack = GetAttack();

            if (critChance > Global.random.NextDouble())
                attack *= 2;

            enemy.RecieveDamage(attack);
        }
    }

    public override void Update(float delta)
    {
        if (HP >= MaxHP) return;

        regenTimerLeft -= delta;

        if (regenTimerLeft <= 0)
        {
            regenTimerLeft = regenTimer;
            HP++;
        }
    }

    public Boss(Entity ent) : base(ent) 
    {
        MaxHP *= 20;
        HP = MaxHP;
        Level += 15;
    }
}