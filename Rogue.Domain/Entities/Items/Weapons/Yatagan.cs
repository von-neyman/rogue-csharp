using Rogue.Domain.Entities.Items.Weapons.Interfaces;

namespace Rogue.Domain.Entities.Items.Weapons;

/// <summary>
/// Ятаган — сбалансированное оружие. Механики: стремительная атака, контратака.
/// </summary>
public class Yatagan : Weapon, IBalancedWeapon, ISwiftStrike, ICounterattack
{
    public Yatagan()
    {
        Name = "Ятаган";
        Description = "Изогнутый вперёд клинок. Рубящий удар идёт по дуге — если лезвие " +
                      "проходит мимо, обратное движение подхватывает атаку. Промах врага — " +
                      "и ятаган сам находит путь к его плоти.";
        Symbol = ')';
    }
}