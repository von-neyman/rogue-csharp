using Rogue.Domain.Entities.Items.Treasures;

namespace Rogue.Domain.Entities.Creatures;

/// <summary>
/// Змей-Маг — стремительный противник с гипнотическим укусом.
/// Характеристики: сила 4, ловкость 8, здоровье 20, враждебность 6.
/// Ходит только по диагонали. При успешной атаке может усыпить игрока на 1 ход (50%).
/// </summary>
public class SnakeMage : Monster
{
    public SnakeMage()
    {
        Strength = 4;
        Agility = 8;
        MaxHealth = 20;
        Health = 20;
        Hostility = 6;
        TreasureLoot = new GoldGoblet { IsOnGround = false };
        Symbol = 'S';
    }

    public override void IdleMove()
    {
        // TODO: движение только по диагонали
    }

    /// <summary>Попытка усыпить игрока. 50% шанс успеха.</summary>
    public bool TryPutToSleep()
    {
        return Random.Shared.Next(2) == 0;
    }
}