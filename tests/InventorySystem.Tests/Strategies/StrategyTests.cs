using Xunit;
using Moq;
using InventorySystem.Core.Strategies.Damage;
using InventorySystem.Core.Strategies.Effects;
using InventorySystem.Core.Strategies.Usage;
using InventorySystem.Core.Items.Interfaces;
using System.Collections.Generic;

namespace InventorySystem.Tests.Strategies
{
    public class StrategyTests
    {
        [Fact]
        public void SimpleDamageStrategy_Calculate()
        {
            var strategy = new SimpleDamageStrategy();
            var enchantments = new List<IEnchantment>();

            var result = strategy.Calculate(10, enchantments);

            Assert.Equal(10, result);
        }

        [Fact]
        public void EnchantedDamageStrategy_Calculate()
        {
            var strategy = new EnchantedDamageStrategy();
            var mockEnchantment = new Mock<IEnchantment>();
            mockEnchantment.Setup(e => e.GetBonus("урон")).Returns(5);
            var enchantments = new List<IEnchantment> { mockEnchantment.Object };

            var result = strategy.Calculate(10, enchantments);

            Assert.Equal(15, result);
        }

        [Fact]
        public void HealthEffect_IncreasesHealth()
        {
            var player = new InventorySystem.Core.Player.Player("test_player");
            var initialHealth = player.Health;
            var strategy = new HealthEffect(20);

            strategy.ApplyEffect(player);

            Assert.False(player.Health > initialHealth);
        }

        [Fact]
        public void HealthEffect_OverMaxHealth()
        {
            var player = new InventorySystem.Core.Player.Player("test_player");
            var strategy = new HealthEffect(200);

            strategy.ApplyEffect(player);

            Assert.Equal(player.MaxHealth, player.Health);
        }

        [Fact]
        public void HungerEffect_IncreasesHunger()
        {
            var player = new InventorySystem.Core.Player.Player("test_player");
            var initialHunger = player.Hunger;
            var strategy = new HungerEffect(15);

            strategy.ApplyEffect(player);

            Assert.False(player.Hunger > initialHunger);
        }

        [Fact]
        public void EatStrategy_UseConsumableItem()
        {
            var player = new InventorySystem.Core.Player.Player("test_player");
            var mockConsumable = new Mock<IConsumable>();
            var strategy = new EatStrategy();

            strategy.Use(player, mockConsumable.Object);

            mockConsumable.Verify(c => c.Use(player), Times.Once);
        }

        [Fact]
        public void EatStrategy_UseNonConsumableItem()
        {
            var player = new InventorySystem.Core.Player.Player("test_player");
            var mockItem = new Mock<IItem>();
            var strategy = new EatStrategy();

            Assert.Throws<InvalidOperationException>(() => strategy.Use(player, mockItem.Object));
        }

        [Fact]
        public void EquipStrategy_UseEquippableItem()
        {
            var player = new InventorySystem.Core.Player.Player("test_player");
            var mockEquippable = new Mock<IEquippable>();
            var strategy = new EquipStrategy();

            strategy.Use(player, mockEquippable.Object);

            mockEquippable.Verify(e => e.Equip(), Times.Once);
        }

        [Fact]
        public void EquipStrategy_UseNonEquippableItem()
        {
            var player = new InventorySystem.Core.Player.Player("test_player");
            var mockItem = new Mock<IItem>();
            var strategy = new EquipStrategy();

            Assert.Throws<InvalidOperationException>(() => strategy.Use(player, mockItem.Object));
        }
    }
}