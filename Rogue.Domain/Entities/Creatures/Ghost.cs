using Rogue.Domain.Common;
using Rogue.Domain.Entities.Creatures.Interfaces;
using Rogue.Domain.Entities.Items.Treasures;

namespace Rogue.Domain.Entities.Creatures;

/// <summary>
/// Привидение — невидимый телепортирующийся противник.
/// </summary>
internal class Ghost : Monster, ITeleport, IInvisible
{
    /// <summary>Невидимо ли привидение.</summary>
    public bool IsInvisible { get; set; } = true;

    internal Ghost()
    {
        Name = "Привидение";
        NameAccusative = "Привидение";
        NameDative = "Привидению";
        Description = "Невидимый телепортирующийся противник.";
        ShortDescription = "Монстр.";
        Faction = Faction.DungeonMonsters;
        BaseStrength = 3;
        BaseAgility = 6;
        BaseMaxHealth = 15;
        Strength = BaseStrength;
        Agility = BaseAgility;
        MaxHealth = BaseMaxHealth;
        Health = MaxHealth;
        Hostility = 3;
        Cost = 2;
        TreasureLoot = new GoldPlate();
        Symbol = 'G';
    }
}