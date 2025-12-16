using Xunit;
using Moq;
using InventorySystem.Core.Items.Enchantment;
using InventorySystem.Core.Items.Interfaces;

namespace InventorySystem.Tests.Enchantments
{
    public class EnchantmentTests
    {
        [Fact]
        public void EnchantmentDecorator_Name()
        {
            var baseEnchantment = new Mock<IEnchantment>();
            baseEnchantment.Setup(e => e.Name).Returns("base");

            var decorator = new EnchantmentDecorator(baseEnchantment.Object);

            Assert.Equal("base (зачаровано)", decorator.Name);
        }

        [Fact]
        public void EnchantmentDecorator_Type()
        {
            var baseEnchantment = new Mock<IEnchantment>();
            baseEnchantment.Setup(e => e.Type).Returns(EnchantmentType.Flame);

            var decorator = new EnchantmentDecorator(baseEnchantment.Object);

            Assert.Equal(EnchantmentType.Flame, decorator.Type);
        }

        [Fact]
        public void EnchantmentDecorator_GetBonus()
        {
            var baseEnchantment = new Mock<IEnchantment>();
            baseEnchantment.Setup(e => e.GetBonus("урон")).Returns(5);

            var additionalEnchantment = new Mock<IEnchantment>();
            additionalEnchantment.Setup(e => e.GetBonus("урон")).Returns(3);

            var decorator = new EnchantmentDecorator(baseEnchantment.Object);
            decorator.AddEnchantment(additionalEnchantment.Object);

            var bonus = decorator.GetBonus("урон");

            Assert.Equal(8, bonus);
        }

        [Fact]
        public void EnchantmentDecorator_ApplyBothEffects()
        {
            var player = new InventorySystem.Core.Player.Player("test_player");
            var baseEnchantment = new Mock<IEnchantment>();
            var additionalEnchantment = new Mock<IEnchantment>();

            var decorator = new EnchantmentDecorator(baseEnchantment.Object);
            decorator.AddEnchantment(additionalEnchantment.Object);

            decorator.ApplyEffect(player);

            baseEnchantment.Verify(e => e.ApplyEffect(player), Times.Once);
            additionalEnchantment.Verify(e => e.ApplyEffect(player), Times.Once);
        }

        [Fact]
        public void FlameEnchantment_NameAndType()
        {
            var enchantment = new FlameEnchantment();

            Assert.Equal("огненная стрела", enchantment.Name);
            Assert.Equal(EnchantmentType.Flame, enchantment.Type);
        }

        [Fact]
        public void FlameEnchantment_FireAndDamageBonuses()
        {
            var enchantment = new FlameEnchantment();

            Assert.Equal(3, enchantment.GetBonus("огненный урон"));
            Assert.Equal(1, enchantment.GetBonus("урон"));
        }

        [Fact]
        public void FlameEnchantment_PlayerHealth()
        {
            var player = new InventorySystem.Core.Player.Player("test_player");
            var initialHealth = player.Health;
            var enchantment = new FlameEnchantment();

            enchantment.ApplyEffect(player);

            Assert.True(player.Health < initialHealth);
        }

        [Fact]
        public void ProtectionEnchantment_NameAndType()
        {
            var enchantment = new ProtectionEnchantment();

            Assert.Equal("защита", enchantment.Name);
            Assert.Equal(EnchantmentType.Protection, enchantment.Type);
        }

        [Fact]
        public void ProtectionEnchantment_ProtectionAndDurabilityBonuses()
        {
            var enchantment = new ProtectionEnchantment();

            Assert.Equal(2, enchantment.GetBonus("защита"));
            Assert.Equal(2, enchantment.GetBonus("прочность"));
        }

        [Fact]
        public void SharpnessEnchantment_NameAndType()
        {
            var enchantment = new SharpnessEnchantment();

            Assert.Equal("острота", enchantment.Name);
            Assert.Equal(EnchantmentType.Sharpness, enchantment.Type);
        }

        [Fact]
        public void SharpnessEnchantment_DamageAndAttackBonuses()
        {
            var enchantment = new SharpnessEnchantment();

            Assert.Equal(3, enchantment.GetBonus("урон"));
            Assert.Equal(2, enchantment.GetBonus("атака"));
        }
    }
}