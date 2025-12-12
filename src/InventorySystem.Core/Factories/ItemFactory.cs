using InventorySystem.Core.Factories.Interfaces;
using InventorySystem.Core.Items.Armor;
using InventorySystem.Core.Items.Food;
using InventorySystem.Core.Items.Interfaces;
using InventorySystem.Core.Items.Potion;
using InventorySystem.Core.Items.Weapon;

namespace InventorySystem.Core.Factories
{
    public class ItemFactory : IItemFactory
    {
        private int _currentId = 100;
        
        public IEquippable CreateSword()
        {
            return new Sword(_currentId++, "меч");
        }
        
        public IEquippable CreateBow()
        {
            return new Bow(_currentId++, "лук");
        }
        
        public IEquippable CreateHammer()
        {
            return new Hammer(_currentId++, "молот");
        }
        
        public IEquippable CreateHelmet()
        {
            return new Helmet(_currentId++, "укреплённый шлем");
        }
        
        public IEquippable CreateChestplate()
        {
            return new Chestplate(_currentId++, "кожаные наплечники");
        }
        
        public IConsumable CreateApple()
        {
            return new Apple(_currentId++, "яблочко");
        }
        
        public IConsumable CreateSteak()
        {
            return new Steak(_currentId++, "жареный стейк", isCooked: true);
        }
        
        public IConsumable CreateBread()
        {
            return new Bread(_currentId++, "багет");
        }
        
        public IConsumable CreateHealthPotion()
        {
            return new HealthPotion(_currentId++, "зелье здоровья", healAmount: 20);
        }
        
        public IConsumable CreateStrengthPotion()
        {
            return new StrengthPotion(_currentId++, "зелье силы", damageMultiplier: 2.0f, duration: 20f);
        }
        
        public IItem CreateItem(ItemType type)
        {
            return type switch
            {
                ItemType.Sword => CreateSword(),
                ItemType.Bow => CreateBow(),
                ItemType.Hammer => CreateHammer(),
                ItemType.Helmet => CreateHelmet(),
                ItemType.Chestplate => CreateChestplate(),
                ItemType.Apple => CreateApple(),
                ItemType.Steak => CreateSteak(),
                ItemType.Bread => CreateBread(),
                ItemType.HealthPotion => CreateHealthPotion(),
                ItemType.StrengthPotion => CreateStrengthPotion(),
                _ => throw new ArgumentException($"такого предмета в игре ещё нет...")
            };
        }
    }
}