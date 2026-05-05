using Rogue.Domain.Entities.Items.Treasures;

namespace Rogue.Domain.Entities.Creatures;

/// <summary>
/// Вампир — ловкий и живучий хищник.
/// Характеристики: сила 4, ловкость 6, здоровье 30, враждебность 6.
/// Первый удар по нему всегда промахивается.
/// При успешной атаке отнимает 1 максимального здоровья у игрока навсегда.
/// </summary>
public class Vampire : Monster
{
    public override int Cost => 4;

    /// <summary>Уклонился ли уже от первого удара в этом бою.</summary>
    public bool FirstHitEvaded { get; set; }

    public Vampire()
    {
        Strength = 4;
        Agility = 6;
        MaxHealth = 30;
        Health = 30;
        Hostility = 6;
        TreasureLoot = new GoldGoblet { IsOnGround = false };
        Symbol = 'V';
    }

    public override void IdleMove()
    {
        // TODO: случайное блуждание
    }

    /// <summary>Отнимает 1 максимального здоровья у игрока. Текущее здоровье снижается,
    /// если превышает новый максимум.</summary>
    public void DrainMaxHealth(Hero player)
    {
        player.MaxHealth -= 1;
        if (player.Health > player.MaxHealth) player.Health = player.MaxHealth;
    }

    /// <summary>Проверяет, должен ли первый удар игрока промахнуться.
    /// Срабатывает один раз за бой.</summary>
    public bool ShouldEvadeFirstHit()
    {
        if (!FirstHitEvaded)
        {
            FirstHitEvaded = true;
            return true;
        }
        return false;
    }
}