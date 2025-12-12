using InventorySystem.Core.Factories.Interfaces;
using InventorySystem.Core.Items.Enchantment;
using InventorySystem.Core.Items.Interfaces;
using System;
using System.Collections.Generic;

namespace InventorySystem.Core.Factories
{
    public class EnchantedItemBuilder : IEnchantedItemBuilder
    {
        private IEquippable? _baseItem;
        private readonly List<IEnchantment> _enchantments = new List<IEnchantment>();
        private int? _customDurability;
        private ItemRarity? _customRarity;
        
        public IEnchantedItemBuilder SetBaseItem(IEquippable baseItem)
        {
            _baseItem = baseItem ?? throw new ArgumentNullException(nameof(baseItem));
            return this;
        }
        
        public IEnchantedItemBuilder AddSharpnessEnchantment()
        {
            ValidateBaseItem();
            _enchantments.Add(new SharpnessEnchantment());
            return this;
        }
        
        public IEnchantedItemBuilder AddProtectionEnchantment()
        {
            ValidateBaseItem();
            _enchantments.Add(new ProtectionEnchantment());
            return this;
        }
        
        public IEnchantedItemBuilder AddFlameEnchantment()
        {
            ValidateBaseItem();
            _enchantments.Add(new FlameEnchantment());
            return this;
        }
        
        public IEnchantedItemBuilder SetDurability(int durability)
        {
            if (durability <= 0)
                throw new ArgumentException("прочность должна быть больше 0 :(", nameof(durability));
            
            _customDurability = durability;
            return this;
        }
        
        public IEnchantedItemBuilder SetRarity(ItemRarity rarity)
        {
            _customRarity = rarity;
            return this;
        }
        
        public IEquippable Build()
        {
            ValidateBaseItem();
            var result = (IEquippable)_baseItem!.Clone();
            
            foreach (var enchantment in _enchantments)
            {
                if (result is IEnchantable enchantable)
                {
                    enchantable.AddEnchantment(enchantment);
                }
            }
            
            if (_customDurability.HasValue)
            {
                result.Durability = Math.Min(_customDurability.Value, result.MaxDurability);
            }
            
            Reset();
            return result;
        }
        
        public void Reset()
        {
            _baseItem = null;
            _enchantments.Clear();
            _customDurability = null;
            _customRarity = null;
        }
        
        private void ValidateBaseItem()
        {
            if (_baseItem == null)
                throw new InvalidOperationException("выберите предмет, прежде чем добавлять зачарования");
        }
        
        public IEquippable BuildSharpSword()
        {
            var factory = new ItemFactory();
            return SetBaseItem(factory.CreateSword())
                  .AddSharpnessEnchantment()
                  .Build();
        }
        
        public IEquippable BuildFlameBow()
        {
            var factory = new ItemFactory();
            return SetBaseItem(factory.CreateBow())
                  .AddFlameEnchantment()
                  .Build();
        }
        
        public IEquippable BuildProtectedArmor(ItemType armorType)
        {
            var factory = new ItemFactory();
            var baseArmor = factory.CreateItem(armorType) as IEquippable;
            
            if (baseArmor == null)
                throw new ArgumentException($"этот предмет нельзя надеть");
            
            return SetBaseItem(baseArmor)
                  .AddProtectionEnchantment()
                  .Build();
        }
    }
}