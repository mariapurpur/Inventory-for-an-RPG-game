using Xunit;
using InventorySystem.Core.Player.States.PlayerStates;

namespace InventorySystem.Tests.Player
{
    public class PlayerStateTests
    {
        private readonly InventorySystem.Core.Player.Player _player;

        public PlayerStateTests()
        {
            _player = new InventorySystem.Core.Player.Player("test_player", 100, 100);
        }

        [Fact]
        public void HealthyState()
        {
            var state = new HealthyState();

            Assert.Equal("здоровый", state.Name);
        }

        [Fact]
        public void HealthyState_EffectiveMultiplier()
        {
            var state = new HealthyState();

            var attackMultiplier = state.GetEffectivenessMultiplier("атаковать");
            var craftMultiplier = state.GetEffectivenessMultiplier("крафтить");

            Assert.Equal(2, attackMultiplier);
            Assert.Equal(2, craftMultiplier);
        }

        [Fact]
        public void HealthyState_MaxOnEnter()
        {
            var state = new HealthyState();
            _player.Health = 50;

            state.OnEnter(_player);

            Assert.Equal(100, _player.Health);
        }

        [Fact]
        public void HealthyState_PerformAction()
        {
            var state = new HealthyState();

            var canAttack = state.CanPerformAction("атаковать");
            var canCraft = state.CanPerformAction("крафтить");

            Assert.True(canAttack);
            Assert.True(canCraft);
        }

        [Fact]
        public void HungryState()
        {
            var state = new HungryState();

            Assert.Equal("хочет кушац", state.Name);
        }

        [Fact]
        public void HungryState_EffectiveMultiplier()
        {
            var state = new HungryState();

            var craftMultiplier = state.GetEffectivenessMultiplier("крафтить");

            Assert.Equal(0, craftMultiplier);
        }

        [Fact]
        public void HungryState_CanPerformAction()
        {
            var state = new HungryState();

            var canCraft = state.CanPerformAction("крафтить");
            var canAttack = state.CanPerformAction("атаковать");

            Assert.False(canCraft);
            Assert.True(canAttack);
        }

        [Fact]
        public void HungryState_Damages()
        {
            var state = new HungryState();
            var initialHealth = _player.Health;

            state.OnEnter(_player);

            Assert.True(_player.Health < initialHealth);
        }
    }
}