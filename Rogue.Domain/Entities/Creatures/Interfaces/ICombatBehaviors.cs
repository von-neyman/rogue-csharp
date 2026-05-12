namespace Rogue.Domain.Entities.Creatures.Interfaces;

/// <summary>Существо способно атаковать.</summary>
internal interface ICanAttack
{
    bool Attack(Creature target);
}

/// <summary>После атаки требуется перезарядка (отдых) — следующий ход пропускается.</summary>
internal interface IRecharge { }

/// <summary>Первая атака по этому существу гарантированно промахивается.</summary>
internal interface IFirstAttackEvasion
{
    bool HasEvaded { get; set; }
}

/// <summary>Атака уменьшает максимальное здоровье цели.</summary>
internal interface IReducesMaxHealth { }

/// <summary>Атака может усыпить цель.</summary>
internal interface ISleepInducer { }