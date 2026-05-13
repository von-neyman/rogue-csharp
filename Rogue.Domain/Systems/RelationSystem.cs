using Rogue.Domain.Common;

namespace Rogue.Domain.Systems;

/// <summary>
/// Система определения отношений между фракциями.
/// </summary>
internal static class RelationSystem
{
    /// <summary>Получить отношение первой фракции ко второй.</summary>
    internal static Relation GetRelation(Faction first, Faction second)
    {
        if (first == Faction.HeroParty && second == Faction.HeroParty) return Relation.Ally;
        if (first == Faction.DungeonMonsters && second == Faction.DungeonMonsters) return Relation.Neutral;
        if ((first == Faction.HeroParty && second == Faction.DungeonMonsters) || (first == Faction.DungeonMonsters && second == Faction.HeroParty)) return Relation.Hostile;
        return Relation.Hostile;
    }
}