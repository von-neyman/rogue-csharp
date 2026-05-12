namespace Rogue.Domain.ViewModels;

/// <summary>
/// Данные инвентаря для отрисовки.
/// </summary>
public class InventoryViewModel
{
    /// <summary>Список еды с названиями и характеристиками.</summary>
    public List<string> FoodItems { get; set; } = [];

    /// <summary>Список зелий с названиями и характеристиками.</summary>
    public List<string> PotionItems { get; set; } = [];

    /// <summary>Список свитков с названиями и характеристиками.</summary>
    public List<string> ScrollItems { get; set; } = [];

    /// <summary>Список оружия с названиями и характеристиками.</summary>
    public List<string> WeaponItems { get; set; } = [];

    /// <summary>Список сокровищ с названиями и характеристиками.</summary>
    public List<string> TreasureItems { get; set; } = [];
}