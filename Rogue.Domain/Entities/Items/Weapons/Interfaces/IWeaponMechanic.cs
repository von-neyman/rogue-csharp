namespace Rogue.Domain.Entities.Items.Weapons.Interfaces;

/// <summary>Стремительная атака: если первый удар не попал — немедленно второй шанс.</summary>
public interface ISwiftStrike { }

/// <summary>Крит: 50% шанс удвоения урона при попадании.</summary>
public interface ICrit { }

/// <summary>Контратака: если враг промахнулся — бесплатный ответный удар.</summary>
public interface ICounterattack { }

/// <summary>Парирование: 50% шанс отразить попавшую по игроку атаку.</summary>
public interface IParry { }