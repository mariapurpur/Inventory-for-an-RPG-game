using Xunit;
using InventorySystem.Core.Items.Armor;

namespace InventorySystem.Tests.Player
{
    public class PlayerTests
    {
        private readonly InventorySystem.Core.Player.Player _player;

        public PlayerTests()
        {
            _player = new InventorySystem.Core.Player.Player("test_player", 100, 100);
        }

        [Fact]
        public void Player()
        {
            Assert.Equal("test_player", _player.Name);
            Assert.Equal(100, _player.Health);
            Assert.Equal(100, _player.MaxHealth);
            Assert.Equal(100, _player.Hunger);
            Assert.Equal(100, _player.MaxHunger);
            Assert.Equal(1, _player.DamageMultiplier);
            Assert.NotNull(_player.Inventory);
            Assert.NotNull(_player.Equipment);
        }

        [Fact]
        public void Heal_IncreasesHealth()
        {
            _player.Health = 50;
            _player.Heal(20);

            Assert.Equal(70, _player.Health);
        }

        [Fact]
        public void Heal_MaxHealth()
        {
            _player.Health = 90;
            _player.Heal(50);

            Assert.Equal(100, _player.Health);
        }

        [Fact]
        public void TakeDamage()
        {
            _player.TakeDamage(20);

            Assert.Equal(80, _player.Health);
        }

        [Fact]
        public void TakeDamage_HealthZero()
        {
            _player.TakeDamage(200);

            Assert.Equal(0, _player.Health);
        }

        [Fact]
        public void Equip_Item()
        {
            var helmet = new Helmet();

            var result = _player.Equip(helmet);

            Assert.True(result);
            Assert.NotNull(_player.Equipment.GetEquippedItem(InventorySystem.Core.Player.Equipment.EquipmentSlot.Head));
        }

        [Fact]
        public void Unequip_Slot()
        {
            var helmet = new Helmet();
            _player.Equip(helmet);

            var result = _player.Unequip(InventorySystem.Core.Player.Equipment.EquipmentSlot.Head);

            Assert.True(result);
            Assert.Null(_player.Equipment.GetEquippedItem(InventorySystem.Core.Player.Equipment.EquipmentSlot.Head));
        }

        [Fact]
        public void IsAlive_HealthZero()
        {
            _player.Health = 0;

            Assert.False(_player.IsAlive);
        }

        [Fact]
        public void IsAlive_HealthPositive()
        {
            Assert.True(_player.IsAlive);
        }
    }
}