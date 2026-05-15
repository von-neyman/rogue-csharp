using Rogue.Domain.Entities.Items.Weapons.Interfaces;

namespace Rogue.Domain.Entities.Items.Weapons;

/// <summary>
/// Ятаган — сбалансированное оружие. Механики: стремительная атака, контратака.
/// </summary>
internal class Yatagan : Weapon, IBalancedWeapon, ISwiftStrike, ICounterattack
{
    internal Yatagan()
    {
        Name = "Ятаган";
        NameAccusative = "Ятаган";
        NameDative = "Ятагану";
        Description = "Изогнутый вперёд клинок. Рубящий удар идёт по дуге — если лезвие " +
                      "проходит мимо, обратное движение подхватывает атаку. Промах врага — " +
                      "и ятаган сам находит путь к его плоти.";
        ShortDescription = "Сила ×3, Ловкость ×2, Стремительная атака, Контратака.";
        Symbol = ')';
    }
}