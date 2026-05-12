namespace Rogue.Domain.Entities.Items.Weapons.Interfaces;

/// <summary>Стремительная атака: если первый удар не попал — немедленно второй шанс.</summary>
internal interface ISwiftStrike { }

/// <summary>Крит: 50% шанс удвоения урона при попадании.</summary>
internal interface ICrit { }

/// <summary>Контратака: если враг промахнулся — бесплатный ответный удар.</summary>
internal interface ICounterattack { }

/// <summary>Парирование: 50% шанс отразить попавшую по игроку атаку.</summary>
internal interface IParry { }