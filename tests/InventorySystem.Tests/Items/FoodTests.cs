using Xunit;
using InventorySystem.Core.Items.Food;

namespace InventorySystem.Tests.Items
{
    public class FoodTests
    {
        [Fact]
        public void Apple()
        {
            var apple = new Apple(101, "тестовое яблоко");

            Assert.Equal(101, apple.Id);
            Assert.Equal("тестовое яблоко", apple.Name);
            Assert.Equal(2, apple.HealthRestore);
            Assert.Equal(3, apple.HungerRestore);
            Assert.Equal(1, apple.MaxUses);
            Assert.Equal(1, apple.UsesRemaining);
            Assert.Equal(64, apple.MaxStackSize);
        }

        [Fact]
        public void Bread()
        {
            var bread = new Bread(102, "тестовый хлеб");

            Assert.Equal(102, bread.Id);
            Assert.Equal("тестовый хлеб", bread.Name);
            Assert.Equal(5, bread.HealthRestore);
            Assert.Equal(7, bread.HungerRestore);
            Assert.Equal(3, bread.MaxUses);
            Assert.Equal(3, bread.UsesRemaining);
            Assert.Equal(64, bread.MaxStackSize);
        }

        [Fact]
        public void Steak()
        {
            var steak = new Steak(103, "тестовый стейк", true);

            Assert.Equal(103, steak.Id);
            Assert.Equal("тестовый стейк", steak.Name);
            Assert.Equal(8, steak.HealthRestore);
            Assert.Equal(10, steak.HungerRestore);
            Assert.Equal(2, steak.MaxUses);
            Assert.Equal(2, steak.UsesRemaining);
            Assert.Equal(64, steak.MaxStackSize);
            Assert.True(steak.IsCooked);
        }

        [Fact]
        public void Use_Food()
        {
            var player = new InventorySystem.Core.Player.Player("test_player");
            var initialHealth = 80;
            var initialHunger = 80;

            var apple = new Apple();
            apple.Use(player);

            Assert.True(player.Health > initialHealth);
            Assert.True(player.Hunger > initialHunger);
            Assert.Equal(0, apple.UsesRemaining);
        }

        [Fact]
        public void Use_ConsumedFood()
        {
            var player = new InventorySystem.Core.Player.Player("test_player");
            var initialHealth = player.Health;

            var apple = new Apple();
            apple.UsesRemaining = 0;
            apple.Use(player);

            Assert.Equal(initialHealth, player.Health);
        }

        [Fact]
        public void Use_NullPlayer()
        {
            var apple = new Apple();
            var initialUses = apple.UsesRemaining;

            Assert.Equal(initialUses, apple.UsesRemaining);
        }

        [Fact]
        public void IsConsumed_UsesZero()
        {
            var apple = new Apple();
            apple.UsesRemaining = 0;

            Assert.True(apple.IsConsumed());
        }

        [Fact]
        public void IsConsumed_UsesRemaining()
        {
            var apple = new Apple();

            Assert.False(apple.IsConsumed());
        }

        [Fact]
        public void Clone_CreatesNewInstance()
        {
            var original = new Apple(101, "яблоко");
            original.UsesRemaining = 0;

            var clone = original.Clone() as Apple;

            Assert.NotNull(clone);
            Assert.NotSame(original, clone);
            Assert.Equal(original.Id, clone.Id);
            Assert.Equal(original.Name, clone.Name);
            Assert.Equal(original.UsesRemaining, clone.UsesRemaining);
        }
    }
}