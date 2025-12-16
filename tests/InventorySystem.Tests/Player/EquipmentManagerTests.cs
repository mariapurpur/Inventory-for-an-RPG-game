using Xunit;
using Moq;
using InventorySystem.Core.Player.Equipment;
using InventorySystem.Core.Items.Interfaces;

namespace InventorySystem.Tests.Player
{
    public class EquipmentManagerTests
    {
        private readonly InventorySystem.Core.Player.Player _player;
        private readonly EquipmentManager _equipmentManager;

        public EquipmentManagerTests()
        {
            _player = new InventorySystem.Core.Player.Player("test_player");
            _equipmentManager = _player.Equipment;
        }

        [Fact]
        public void Equip_Item()
        {
            var mockItem = new Mock<IEquippable>();
            mockItem.Setup(i => i.Slot).Returns(EquipmentSlot.Head);
            mockItem.Setup(i => i.Equip()).Returns(true);

            var result = _equipmentManager.Equip(mockItem.Object);

            Assert.True(result);
            Assert.NotNull(_equipmentManager.GetEquippedItem(EquipmentSlot.Head));
        }

        [Fact]
        public void Unequip_Slot()
        {
            var mockItem = new Mock<IEquippable>();
            mockItem.Setup(i => i.Slot).Returns(EquipmentSlot.Head);
            mockItem.Setup(i => i.Equip()).Returns(true);

            _equipmentManager.Equip(mockItem.Object);
            var result = _equipmentManager.Unequip(EquipmentSlot.Head);

            Assert.True(result);
            Assert.Null(_equipmentManager.GetEquippedItem(EquipmentSlot.Head));
        }

        [Fact]
        public void GetEquippedItem()
        {
            var mockItem = new Mock<IEquippable>();
            mockItem.Setup(i => i.Slot).Returns(EquipmentSlot.Head);
            mockItem.Setup(i => i.Equip()).Returns(true);

            _equipmentManager.Equip(mockItem.Object);

            var item = _equipmentManager.GetEquippedItem(EquipmentSlot.Head);

            Assert.Same(mockItem.Object, item);
        }

        [Fact]
        public void GetTotalBonus_EnchantmentsSum()
        {
            var mockItem = new Mock<IEquippable>();
            var mockEnchantable = new Mock<IEnchantable>();
            mockItem.As<IEnchantable>().Setup(e => e.GetEnchantmentBonus("урон")).Returns(5);
            mockItem.Setup(i => i.Slot).Returns(EquipmentSlot.Hand);
            mockItem.Setup(i => i.Equip()).Returns(true);

            _equipmentManager.Equip(mockItem.Object);

            var totalBonus = _equipmentManager.GetTotalBonus("урон");

            Assert.Equal(5, totalBonus);
        }

        [Fact]
        public void IsEquipped_Equipped()
        {
            var mockItem = new Mock<IEquippable>();
            mockItem.Setup(i => i.Slot).Returns(EquipmentSlot.Head);
            mockItem.Setup(i => i.Equip()).Returns(true);

            _equipmentManager.Equip(mockItem.Object);

            var isEquipped = _equipmentManager.IsEquipped(mockItem.Object);

            Assert.True(isEquipped);
        }

        [Fact]
        public void GetEquipmentStats()
        {
            var mockItem = new Mock<IEquippable>();
            mockItem.Setup(i => i.Slot).Returns(EquipmentSlot.Head);
            mockItem.Setup(i => i.Equip()).Returns(true);

            _equipmentManager.Equip(mockItem.Object);

            var stats = _equipmentManager.GetEquipmentStats();

            Assert.Equal(1, stats.TotalItems);
            Assert.Equal(4, stats.TotalSlots);
        }
    }
}