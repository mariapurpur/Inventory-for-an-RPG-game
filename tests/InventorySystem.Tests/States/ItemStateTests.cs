using Xunit;
using Moq;
using InventorySystem.Core.Player.States.ItemStates;
using InventorySystem.Core.Items.Interfaces;

namespace InventorySystem.Tests.Player
{
    public class ItemStateTests
    {
        [Fact]
        public void NewState()
        {
            var state = new NewState();

            Assert.Equal("новое", state.StateName);
            Assert.Equal(1f, state.GetEffectivenessMultiplier());
            Assert.True(state.CanUse());
            Assert.False(state.CanRepair());
            Assert.True(state.CanBreak());
        }

        [Fact]
        public void DamagedState()
        {
            var state = new DamagedState();

            Assert.Equal("испорчен, на грани поломки", state.StateName);
            Assert.Equal(0.5f, state.GetEffectivenessMultiplier());
            Assert.True(state.CanUse());
            Assert.True(state.CanRepair());
            Assert.True(state.CanBreak());
        }

        [Fact]
        public void BrokenState()
        {
            var state = new BrokenState();

            Assert.Equal("сломався", state.StateName);
            Assert.Equal(0f, state.GetEffectivenessMultiplier());
            Assert.False(state.CanUse());
            Assert.True(state.CanRepair());
            Assert.False(state.CanBreak());
        }

        [Fact]
        public void NewState_Break()
        {
            var mockStatefulItem = new Mock<IStatefulItem>();
            var state = new NewState();

            state.OnBreak(mockStatefulItem.Object);

            mockStatefulItem.Verify(s => s.SetState(It.IsAny<DamagedState>()), Times.Once);
        }

        [Fact]
        public void DamagedState_Break()
        {
            var mockStatefulItem = new Mock<IStatefulItem>();
            var state = new DamagedState();

            state.OnBreak(mockStatefulItem.Object);

            mockStatefulItem.Verify(s => s.SetState(It.IsAny<BrokenState>()), Times.Once);
        }

        [Fact]
        public void DamagedState_Repair()
        {
            var mockStatefulItem = new Mock<IStatefulItem>();
            var state = new DamagedState();

            state.OnRepair(mockStatefulItem.Object);

            mockStatefulItem.Verify(s => s.SetState(It.IsAny<NewState>()), Times.Once);
        }

        [Fact]
        public void BrokenState_Repair()
        {
            var mockStatefulItem = new Mock<IStatefulItem>();
            var state = new BrokenState();

            state.OnRepair(mockStatefulItem.Object);

            mockStatefulItem.Verify(s => s.SetState(It.IsAny<DamagedState>()), Times.Once);
        }

        [Fact]
        public void NewState_Use()
        {
            var mockItem = new Mock<IItem>();
            var state = new NewState();

            state.OnUse(mockItem.Object);

            Assert.True(true);
        }

        [Fact]
        public void DamagedState_Use()
        {
            var mockItem = new Mock<IItem>();
            var state = new DamagedState();

            state.OnUse(mockItem.Object);

            Assert.True(true);
        }
    }
}