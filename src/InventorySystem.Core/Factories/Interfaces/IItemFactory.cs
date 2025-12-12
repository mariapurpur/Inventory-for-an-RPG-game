using InventorySystem.Core.Items.Interfaces;

namespace InventorySystem.Core.Factories.Interfaces
{
    public interface IItemFactory
    {
        IEquippable CreateSword();
        IEquippable CreateBow();
        IEquippable CreateHammer();
        IEquippable CreateHelmet();
        IEquippable CreateChestplate();
        IConsumable CreateApple();
        IConsumable CreateSteak();
        IConsumable CreateBread();
        IConsumable CreateHealthPotion();
        IConsumable CreateStrengthPotion();
        IItem CreateItem(ItemType type);
    }
    
    public enum ItemType
    {
        Sword,
        Bow,
        Hammer,
        Helmet,
        Chestplate,
        Apple,
        Steak,
        Bread,
        HealthPotion,
        StrengthPotion
    }
}