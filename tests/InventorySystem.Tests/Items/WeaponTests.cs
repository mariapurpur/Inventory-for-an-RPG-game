using Xunit;
using Moq;
using InventorySystem.Core.Items.Weapon;
using InventorySystem.Core.Items.Enchantment;
using InventorySystem.Core.Items.Interfaces;
using InventorySystem.Core.Player.Equipment;

namespace InventorySystem.Tests.Items
{
    public class WeaponTests
    {
        [Fact]
        public void Sword()
        {
            var sword = new Sword(101, "тестовый меч");

            Assert.Equal(101, sword.Id);
            Assert.Equal("тестовый меч", sword.Name);
            Assert.Equal(10, sword.BaseDamage);
            Assert.Equal(EquipmentSlot.Hand, sword.Slot);
            Assert.Equal(150, sword.MaxDurability);
            Assert.Equal(150, sword.Durability);
            Assert.Equal(ItemRarity.Rare, sword.Rarity);
        }

        [Fact]
        public void Bow()
        {
            var bow = new Bow(102, "тестовый лук");

            Assert.Equal(102, bow.Id);
            Assert.Equal("тестовый лук", bow.Name);
            Assert.Equal(8, bow.BaseDamage);
            Assert.Equal(EquipmentSlot.BothHands, bow.Slot);
            Assert.Equal(120, bow.MaxDurability);
            Assert.Equal(120, bow.Durability);
            Assert.Equal(0, bow.ArrowCount);
            Assert.Equal(64, bow.MaxArrowCount);
            Assert.Equal(ItemRarity.Rare, bow.Rarity);
        }

        [Fact]
        public void Hammer()
        {
            var hammer = new Hammer(103, "тестовый молот");

            Assert.Equal(103, hammer.Id);
            Assert.Equal("тестовый молот", hammer.Name);
            Assert.Equal(15, hammer.BaseDamage);
            Assert.Equal(EquipmentSlot.BothHands, hammer.Slot);
            Assert.Equal(200, hammer.MaxDurability);
            Assert.Equal(200, hammer.Durability);
            Assert.False(hammer.IsCharged);
            Assert.Equal(ItemRarity.Rare, hammer.Rarity);
        }

        [Fact]
        public void Equip_NotBroken()
        {
            var sword = new Sword();

            var result = sword.Equip();

            Assert.True(result);
        }

        [Fact]
        public void Equip_Broken()
        {
            var sword = new Sword();
            sword.Durability = 0;

            var result = sword.Equip();

            Assert.False(result);
        }

        [Fact]
        public void IsBroken_DurabilityZero()
        {
            var sword = new Sword();
            sword.Durability = 0;

            Assert.True(sword.IsBroken());
        }

        [Fact]
        public void CalculateDamage_NoEnchantments()
        {
            var sword = new Sword();

            var damage = sword.CalculateDamage();

            Assert.Equal(10, damage);
        }

        [Fact]
        public void AddEnchantment()
        {
            var sword = new Sword();
            var enchantment = new Mock<IEnchantment>().Object;

            var result = sword.AddEnchantment(enchantment);

            Assert.True(result);
            Assert.Single(sword.Enchantments);
        }

        [Fact]
        public void RemoveEnchantment()
        {
            var sword = new Sword();
            var enchantment = new Mock<IEnchantment>().Object;
            sword.AddEnchantment(enchantment);

            var result = sword.RemoveEnchantment(enchantment);

            Assert.True(result);
            Assert.Empty(sword.Enchantments);
        }

        [Fact]
        public void Bow_LoadArrows()
        {
            var bow = new Bow();

            var result = bow.LoadArrows(10);

            Assert.True(result);
            Assert.Equal(10, bow.ArrowCount);
        }

        [Fact]
        public void Bow_LoadArrowsMax()
        {
            var bow = new Bow();

            var result = bow.LoadArrows(100);

            Assert.False(result);
        }

        [Fact]
        public void Bow_Shoot()
        {
            var bow = new Bow();
            bow.LoadArrows(5);

            var result = bow.Shoot();

            Assert.True(result);
            Assert.Equal(4, bow.ArrowCount);
            Assert.Equal(119, bow.Durability);
        }

        [Fact]
        public void Bow_CalculateDamage_FullArrows()
        {
            var bow = new Bow();
            bow.LoadArrows(64);

            var damage = bow.CalculateDamage();

            Assert.Equal(16, damage); // 8 * 2
        }

        [Fact]
        public void Hammer_Charge()
        {
            var hammer = new Hammer();

            hammer.Charge();

            Assert.True(hammer.IsCharged);
        }

        [Fact]
        public void Hammer_CalculateDamage_Charged()
        {
            var hammer = new Hammer();
            hammer.Charge();

            var damage = hammer.CalculateDamage();

            Assert.Equal(30, damage); // 15 * 2
            Assert.False(hammer.IsCharged);
        }

        [Fact]
        public void Clone_NewInstance()
        {
            var original = new Sword(101, "меч");
            original.Durability = 50;

            var clone = original.Clone() as Sword;

            Assert.NotNull(clone);
            Assert.NotSame(original, clone);
            Assert.Equal(original.Id, clone.Id);
            Assert.Equal(original.Name, clone.Name);
            Assert.Equal(original.Durability, clone.Durability);
        }
    }
}