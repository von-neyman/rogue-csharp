namespace Rogue.Domain.Entities.Creatures.Interfaces;

/// <summary>Существо способно атаковать.</summary>
public interface ICanAttack
{
    bool Attack(Creature target);
}

/// <summary>После атаки требуется перезарядка (отдых) — следующий ход пропускается.</summary>
public interface IRecharge { }

/// <summary>Первая атака по этому существу гарантированно промахивается.</summary>
public interface IFirstAttackEvasion
{
    bool HasEvaded { get; set; }
}

/// <summary>Атака уменьшает максимальное здоровье цели.</summary>
public interface IReducesMaxHealth { }

/// <summary>Атака может усыпить цель.</summary>
public interface ISleepInducer { }