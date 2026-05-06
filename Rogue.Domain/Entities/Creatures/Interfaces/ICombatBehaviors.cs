namespace Rogue.Domain.Entities.Creatures.Interfaces;

/// <summary>После атаки требуется перезарядка (отдых) — следующий ход пропускается.</summary>
public interface IRecharge
{
    bool IsRecharging { get; set; }
}

/// <summary>Первая атака по этому существу гарантированно промахивается.</summary>
public interface IFirstAttackEvasion
{
    bool HasEvaded { get; set; }
}

/// <summary>Атака уменьшает максимальное здоровье цели.</summary>
public interface IReducesMaxHealth { }

/// <summary>Атака может усыпить цель.</summary>
public interface ISleepInducer { }