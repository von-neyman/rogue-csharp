using Rogue.Domain.Entities.Creatures;
using Rogue.Domain.Entities.Items;
using Rogue.Domain.Entities.Items.Food;
using Rogue.Domain.Entities.Items.Potions;
using Rogue.Domain.Entities.Items.Scrolls;
using Rogue.Domain.Entities.Items.Weapons;

namespace Rogue.Domain.World;

/// <summary>
/// Генератор сущностей уровня: расставляет игрока, выход, монстров и предметы по комнатам.
/// </summary>
internal static class EntityGenerator
{
    private static readonly Random RandomGenerator = new();

    /// <summary>Главный метод генерации. Заполняет уровень монстрами и предметами,
    /// задаёт точки старта и выхода.</summary>
    internal static void Generate(Level level)
    {
        PlacePlayer(level);
        PlaceExit(level);
        PlaceMonsters(level);
        PlaceItems(level);
    }

    /// <summary>Размещает игрока в случайной свободной клетке случайной комнаты.
    /// Эта комната становится стартовой (без монстров и предметов).</summary>
    private static void PlacePlayer(Level level)
    {
        int roomIndex = RandomGenerator.Next(level.Rooms.Count);
        var spawnRoom = level.Rooms[roomIndex];
        spawnRoom.IsStartRoom = true;
        var position = GetRandomPosition(spawnRoom);
        level.StartPoint = position;
    }

    /// <summary>Размещает выход в случайной свободной клетке комнаты,
    /// отличной от стартовой. Эта комната становится конечной.</summary>
    private static void PlaceExit(Level level)
    {
        int roomIndex;
        do { roomIndex = RandomGenerator.Next(level.Rooms.Count); } while (level.Rooms[roomIndex].IsStartRoom);
        var exitRoom = level.Rooms[roomIndex];
        exitRoom.IsExitRoom = true;
        var position = GetRandomPosition(exitRoom);
        level.ExitPoint = position;
    }

    // Паттерн Factory Method: создание монстров через список делегатов-фабрик позволяет выбирать конкретный тип во время выполнения, не теряя типизацию.
    /// <summary>Размещает монстров по комнатам. Количество определяется пулом очков
    /// и стоимостью каждого монстра. Стартовая комната остаётся пустой.</summary>
    private static void PlaceMonsters(Level level)
    {
        int remainingPool = level.MonsterPool;
        var monsterFactories = new List<Func<Monster>>
    {
        () => new Zombie(),
        () => new Ghost(),
        () => new SnakeMage(),
        () => new Vampire(),
        () => new Ogre()
    };
        while (remainingPool > 0)
        {
            var createMonster = monsterFactories[RandomGenerator.Next(monsterFactories.Count)];
            var monster = createMonster();
            if (monster.Cost > remainingPool) continue;
            Room targetRoom;
            do { targetRoom = level.Rooms[RandomGenerator.Next(level.Rooms.Count)]; } while (targetRoom.IsStartRoom);
            var position = GetRandomPosition(targetRoom);
            var tile = level.Map.GetTile(position.X, position.Y)!;
            monster.CurrentTile = tile;
            tile.CreaturesOnTile.Add(monster);
            targetRoom.CreaturesInRoom.Add(monster);
            level.DungeonMonsters.Add(monster);
            remainingPool -= monster.Cost;
        }
    }

    /// <summary>Размещает предметы в комнатах (кроме стартовой).
    /// Каждая комната получает от 0 до 3 предметов.
    /// Шансы: еда 40%, зелье 20%, свиток 20%, оружие 20%.</summary>
    private static void PlaceItems(Level level)
    {
        foreach (var room in level.Rooms)
        {
            if (room.IsStartRoom) continue;
            int maxItems = level.LevelNumber <= 7 ? 4 : (level.LevelNumber <= 14 ? 3 : 2);
            int itemCount = RandomGenerator.Next(maxItems);
            for (int i = 0; i < itemCount; i++)
            {
                var item = CreateRandomItem();
                var position = GetRandomPosition(room);
                var tile = level.Map.GetTile(position.X, position.Y)!;
                item.CurrentTile = tile;
                tile.ItemsOnTile.Add(item);
                room.ItemsInRoom.Add(item);
                level.Items.Add(item);
            }
        }
    }

    /// <summary>Создаёт случайный предмет согласно шансам:
    /// 40% еда, 20% зелье, 20% свиток, 20% оружие.</summary>
    private static Item CreateRandomItem()
    {
        int roll = RandomGenerator.Next(100);
        if (roll < 40) return CreateRandomFood();
        if (roll < 60) return CreateRandomPotion();
        if (roll < 80) return CreateRandomScroll();
        return CreateRandomWeapon();
    }

    /// <summary>Создаёт случайную еду (равновероятно один из 5 типов).</summary>
    private static Food CreateRandomFood()
    {
        int roll = RandomGenerator.Next(5);
        if (roll == 0) return new Hardtack();
        if (roll == 1) return new Apple();
        if (roll == 2) return new Cheese();
        if (roll == 3) return new SmokedMeat();
        return new MeatStew();
    }

    /// <summary>Создаёт случайное зелье (равновероятно один из 3 типов).</summary>
    private static Potion CreateRandomPotion()
    {
        int roll = RandomGenerator.Next(3);
        if (roll == 0) return new PotionOfStrength();
        if (roll == 1) return new PotionOfAgility();
        return new PotionOfHealth();
    }

    /// <summary>Создаёт случайный свиток (равновероятно один из 3 типов).</summary>
    private static Scroll CreateRandomScroll()
    {
        int roll = RandomGenerator.Next(3);
        if (roll == 0) return new ScrollOfStrength();
        if (roll == 1) return new ScrollOfAgility();
        return new ScrollOfHealth();
    }

    /// <summary>Создаёт случайное оружие (равновероятно одно из 18).</summary>
    private static Weapon CreateRandomWeapon()
    {
        int roll = RandomGenerator.Next(18);
        if (roll == 0) return new DaggerPair();
        if (roll == 1) return new BattleChain();
        if (roll == 2) return new Greatsword();
        if (roll == 3) return new Rapier();
        if (roll == 4) return new Yatagan();
        if (roll == 5) return new Glaive();
        if (roll == 6) return new LightEspadon();
        if (roll == 7) return new Estoc();
        if (roll == 8) return new Pole();
        if (roll == 9) return new Claw();
        if (roll == 10) return new Cleaver();
        if (roll == 11) return new WarHammer();
        if (roll == 12) return new Saber();
        if (roll == 13) return new Broadsword();
        if (roll == 14) return new BattleAxe();
        if (roll == 15) return new SwordAndDagger();
        if (roll == 16) return new Longsword();
        return new Spear();
    }

    /// <summary>Находит случайную свободную позицию внутри комнаты
    /// и добавляет её в список занятых.</summary>
    private static (int X, int Y) GetRandomPosition(Room room)
    {
        int x, y;
        do
        {
            x = RandomGenerator.Next(room.TopLeft.X + 1, room.BottomRight.X);
            y = RandomGenerator.Next(room.TopLeft.Y + 1, room.BottomRight.Y);
        } while (room.OccupiedPositions.Contains((x, y)));
        room.OccupiedPositions.Add((x, y));
        return (x, y);
    }
}