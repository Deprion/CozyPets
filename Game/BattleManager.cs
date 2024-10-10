public static class BattleManager
{
    public static void Battle(Entity pet, Entity enemy)
    {
        while(pet.HP > 0 && enemy.HP > 0)
        {
            if (pet.Initiative > enemy.Initiative)
            {
                Fight(pet, enemy);   
            }
            else
            {
                Fight(enemy, pet);
            }
        }
    }

    private static void Fight(Entity first, Entity second)
    {
        first.DoHit(second);

        if (second.HP > 0)
        {
            second.DoHit(first);
        }
    }
}