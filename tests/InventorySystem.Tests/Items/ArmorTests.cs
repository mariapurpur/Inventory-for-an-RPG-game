using Xunit;
using Moq;
using InventorySystem.Core.Items.Armor;
using InventorySystem.Core.Items.Enchantment;
using InventorySystem.Core.Items.Interfaces;
using InventorySystem.Core.Player.Equipment;

namespace InventorySystem.Tests.Items
{
    public class ArmorTests
    {
        [Fact]
        public void Helmet()
        {
            var helmet = new Helmet(101, "тестовый шлем");

            Assert.Equal(101, helmet.Id);
            Assert.Equal("тестовый шлем", helmet.Name);
            Assert.Equal(5, helmet.BaseDefense);
            Assert.Equal(EquipmentSlot.Head, helmet.Slot);
            Assert.Equal(100, helmet.MaxDurability);
            Assert.Equal(100, helmet.Durability);
            Assert.Equal(2, helmet.MaxEnchantments);
            Assert.Equal(ItemRarity.Common, helmet.Rarity);
        }

        [Fact]
        public void Chestplate()
        {
            var chestplate = new Chestplate(102, "тестовая броня");

            Assert.Equal(102, chestplate.Id);
            Assert.Equal("тестовая броня", chestplate.Name);
            Assert.Equal(10, chestplate.BaseDefense);
            Assert.Equal(EquipmentSlot.Chest, chestplate.Slot);
            Assert.Equal(150, chestplate.MaxDurability);
            Assert.Equal(150, chestplate.Durability);
            Assert.Equal(3, chestplate.MaxEnchantments);
            Assert.Equal(ItemRarity.Common, chestplate.Rarity);
        }

        [Fact]
        public void Equip_NotBroken()
        {
            var helmet = new Helmet();

            var result = helmet.Equip();

            Assert.True(result);
        }

        [Fact]
        public void Equip_Broken()
        {
            var helmet = new Helmet();
            helmet.Durability = 0;

            var result = helmet.Equip();

            Assert.False(result);
        }

        [Fact]
        public void IsBroken_DurabilityZero()
        {
            var helmet = new Helmet();
            helmet.Durability = 0;

            Assert.True(helmet.IsBroken());
        }

        [Fact]
        public void IsBroken_DurabilityPositive()
        {
            var helmet = new Helmet();

            Assert.False(helmet.IsBroken());
        }

        [Fact]
        public void CalculateDefense_NoEnchantments()
        {
            var chestplate = new Chestplate();

            var defense = chestplate.CalculateDefense();

            Assert.Equal(10, defense);
        }

        [Fact]
        public void AddEnchantment_WithinLimit()
        {
            var helmet = new Helmet();
            var enchantment = new Mock<IEnchantment>().Object;

            var result = helmet.AddEnchantment(enchantment);

            Assert.True(result);
            Assert.Single(helmet.Enchantments);
        }

        [Fact]
        public void AddEnchantment_ExceedsLimit()
        {
            var helmet = new Helmet();
            var enchantment1 = new Mock<IEnchantment>().Object;
            var enchantment2 = new Mock<IEnchantment>().Object;
            var enchantment3 = new Mock<IEnchantment>().Object;

            helmet.AddEnchantment(enchantment1);
            helmet.AddEnchantment(enchantment2);

            var result = helmet.AddEnchantment(enchantment3);

            Assert.False(result);
        }

        [Fact]
        public void RemoveEnchantment()
        {
            var helmet = new Helmet();
            var enchantment = new Mock<IEnchantment>().Object;
            helmet.AddEnchantment(enchantment);

            var result = helmet.RemoveEnchantment(enchantment);

            Assert.True(result);
            Assert.Empty(helmet.Enchantments);
        }

        [Fact]
        public void HasEnchantment_True()
        {
            var mockEnchantment = new Mock<IEnchantment>();
            mockEnchantment.Setup(e => e.Type).Returns(EnchantmentType.Protection);

            var helmet = new Helmet();
            helmet.AddEnchantment(mockEnchantment.Object);

            var hasEnchantment = helmet.HasEnchantment("защита");

            Assert.True(hasEnchantment);
        }

        [Fact]
        public void Clone_NewInstance()
        {
            var original = new Helmet(101, "шлем");
            original.Durability = 50;

            var clone = original.Clone() as Helmet;

            Assert.NotNull(clone);
            Assert.NotSame(original, clone);
            Assert.Equal(original.Id, clone.Id);
            Assert.Equal(original.Name, clone.Name);
            Assert.Equal(original.Durability, clone.Durability);
        }
    }
}