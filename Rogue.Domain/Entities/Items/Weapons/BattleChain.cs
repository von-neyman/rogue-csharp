using Rogue.Domain.Entities.Items.Weapons.Interfaces;

namespace Rogue.Domain.Entities.Items.Weapons;

/// <summary>
/// Боевой цеп — сбалансированное оружие. Механики: стремительная атака, крит.
/// </summary>
internal class BattleChain : Weapon, IBalancedWeapon, ISwiftStrike, ICrit
{
    internal BattleChain()
    {
        Name = "Боевой цеп";
        Description = "Шипастое било на цепи с рукоятью. Раскрутил — бросил. " +
                      "Если цепь прошла мимо, обратное движение возвращает её в бой. " +
                      "Шипы рвут плоть, и когда цепь ложится особенно удачно — рана выходит страшной.";
        Symbol = ')';
    }
}