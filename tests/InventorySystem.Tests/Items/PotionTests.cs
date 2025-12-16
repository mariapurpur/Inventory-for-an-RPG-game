using Xunit;
using InventorySystem.Core.Items.Potion;

namespace InventorySystem.Tests.Items
{
    public class PotionTests
    {
        [Fact]
        public void HealthPotion()
        {
            var potion = new HealthPotion(101, "тестовое зелье", 25, 0f);

            Assert.Equal(101, potion.Id);
            Assert.Equal("тестовое зелье", potion.Name);
            Assert.Equal(25, potion.HealAmount);
            Assert.Equal(0f, potion.Duration);
            Assert.Equal(4, potion.MaxStackSize);
            Assert.Equal(1, potion.MaxUses);
            Assert.Equal(1, potion.UsesRemaining);
        }

        [Fact]
        public void StrengthPotion()
        {
            var potion = new StrengthPotion(102, "тестовая сила", 2.5f, 30f);

            Assert.Equal(102, potion.Id);
            Assert.Equal("тестовая сила", potion.Name);
            Assert.Equal(2.5f, potion.DamageMultiplier);
            Assert.Equal(30f, potion.Duration);
            Assert.Equal(4, potion.MaxStackSize);
            Assert.Equal(1, potion.MaxUses);
            Assert.Equal(1, potion.UsesRemaining);
        }

        [Fact]
        public void HealthPotion_UseInstantHeal()
        {
            var player = new InventorySystem.Core.Player.Player("test_player");
            var initialHealth = player.Health;
            var potion = new HealthPotion(healAmount: 10);

            potion.Use(player);

            Assert.False(player.Health > initialHealth);
            Assert.Equal(0, potion.UsesRemaining);
        }

        [Fact]
        public void StrengthPotion_UseEffect()
        {
            var player = new InventorySystem.Core.Player.Player("test_player");
            var initialMultiplier = player.DamageMultiplier;
            var potion = new StrengthPotion(damageMultiplier: 2.0f);

            potion.Use(player);

            Assert.False(player.DamageMultiplier > initialMultiplier);
            Assert.Equal(0, potion.UsesRemaining);
        }

        [Fact]
        public void Potion_UseConsumed()
        {
            var player = new InventorySystem.Core.Player.Player("test_player");
            var initialHealth = player.Health;
            var potion = new HealthPotion();

            potion.UsesRemaining = 0;
            potion.Use(player);

            Assert.Equal(initialHealth, player.Health);
        }

        [Fact]
        public void IsConsumed_UsesZero()
        {
            var potion = new HealthPotion();
            potion.UsesRemaining = 0;

            Assert.True(potion.IsConsumed());
        }

        [Fact]
        public void IsConsumed_UsesRemaining()
        {
            var potion = new HealthPotion();

            Assert.False(potion.IsConsumed());
        }

        [Fact]
        public void Clone_NewPotion()
        {
            var original = new HealthPotion(101, "зелье", 20, 0f);
            original.UsesRemaining = 0;

            var clone = original.Clone() as HealthPotion;

            Assert.NotNull(clone);
            Assert.NotSame(original, clone);
            Assert.Equal(original.Id, clone.Id);
            Assert.Equal(original.Name, clone.Name);
            Assert.Equal(original.HealAmount, clone.HealAmount);
            Assert.Equal(original.UsesRemaining, clone.UsesRemaining);
        }

        [Fact]
        public void CanStackWith_PotionsFull()
        {
            var potion1 = new HealthPotion(101, "зелье");
            var potion2 = new HealthPotion(101, "зелье");

            var result = potion1.CanStackWith(potion2);

            Assert.True(result);
        }

        [Fact]
        public void CanStackWith_PotionPartial()
        {
            var potion1 = new HealthPotion(101, "зелье");
            var potion2 = new HealthPotion(101, "зелье");
            potion2.UsesRemaining = 0;

            var result = potion1.CanStackWith(potion2);

            Assert.False(result);
        }
    }
}