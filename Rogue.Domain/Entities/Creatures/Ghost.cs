using Rogue.Domain.Entities.Items.Treasures;

namespace Rogue.Domain.Entities.Creatures;

/// <summary>
/// Привидение — невидимый телепортирующийся противник.
/// Характеристики: сила 3, ловкость 6, здоровье 15, враждебность 3.
/// Вне боя невидимо, периодически телепортируется по комнате.
/// </summary>
public class Ghost : Monster
{
    public override int Cost => 2;

    /// <summary>Невидимо ли привидение.</summary>
    public bool IsInvisible { get; set; } = true;

    public Ghost()
    {
        Strength = 3;
        Agility = 6;
        MaxHealth = 15;
        Health = 15;
        Hostility = 3;
        TreasureLoot = new GoldPlate { IsOnGround = false };
        Symbol = 'G';
    }

    public override void IdleMove()
    {
        // TODO: телепортация в случайную точку комнаты
    }

    /// <summary>Переключает видимость привидения.
    /// Обрабатывается в FogOfWarSystem.</summary>
    public void ToggleInvisibility()
    {
        // TODO: переключение видимости
    }
}