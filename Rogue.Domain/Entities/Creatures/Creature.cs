namespace Rogue.Domain.Entities.Creatures;

/// <summary>
/// Базовый класс для всех живых существ — игрока и монстров.
/// Содержит общие характеристики, позицию на карте и методы получения урона/лечения.
/// </summary>
public abstract class Creature
{
    /// <summary>Позиция X на карте.</summary>
    public int X { get; set; }

    /// <summary>Позиция Y на карте.</summary>
    public int Y { get; set; }

    /// <summary>Текущее здоровье. При падении до 0 существо погибает.</summary>
    public int Health { get; set; }

    /// <summary>Максимальное здоровье. Используется как потолок для лечения.</summary>
    public int MaxHealth { get; set; }

    /// <summary>Сила. Влияет на наносимый урон.</summary>
    public int Strength { get; set; }

    /// <summary>Ловкость. Влияет на шанс попадания и уклонения.</summary>
    public int Agility { get; set; }

    /// <summary>Живо ли существо (Health > 0).</summary>
    public bool IsAlive => Health > 0;

    /// <summary>Символ для отображения на карте.</summary>
    public char Symbol { get; set; }

    /// <summary>Получить урон. Health не опускается ниже 0.</summary>
    public void TakeDamage(int amount)
    {
        Health -= amount;
        if (Health < 0) Health = 0;
    }

    /// <summary>Восстановить здоровье. Не превышает MaxHealth.</summary>
    public void Heal(int amount)
    {
        Health += amount;
        if (Health > MaxHealth) Health = MaxHealth;
    }
}