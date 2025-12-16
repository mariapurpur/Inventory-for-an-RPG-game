using Xunit;
using Moq;
using InventorySystem.Core.Factories;
using InventorySystem.Core.Factories.Interfaces;
using InventorySystem.Core.Items.Armor;
using InventorySystem.Core.Items.Enchantment;
using InventorySystem.Core.Items.Interfaces;
using InventorySystem.Core.Items.Weapon;
using System;

namespace InventorySystem.Tests.Factories
{
    public class ItemFactoryTests
    {
        private readonly ItemFactory _factory;

        public ItemFactoryTests()
        {
            _factory = new ItemFactory();
        }

        [Fact]
        public void CreateSword()
        {
            var result = _factory.CreateSword();

            Assert.IsType<Sword>(result);
            Assert.Equal("меч", result.Name);
        }

        [Fact]
        public void CreateBow()
        {
            var result = _factory.CreateBow();

            Assert.IsType<Bow>(result);
            Assert.Equal("лук", result.Name);
        }

        [Fact]
        public void CreateHammer()
        {
            var result = _factory.CreateHammer();

            Assert.IsType<Hammer>(result);
            Assert.Equal("молот", result.Name);
        }

        [Fact]
        public void CreateHelmet()
        {
            var result = _factory.CreateHelmet();

            Assert.IsType<Helmet>(result);
            Assert.Equal("укреплённый шлем", result.Name);
        }

        [Fact]
        public void CreateChestplate()
        {
            var result = _factory.CreateChestplate();

            Assert.IsType<Chestplate>(result);
            Assert.Equal("кожаные наплечники", result.Name);
        }

        [Fact]
        public void CreateItem_SwordType()
        {
            var result = _factory.CreateItem(ItemType.Sword);

            Assert.IsType<Sword>(result);
        }

        [Fact]
        public void CreateItem_BowType()
        {
            var result = _factory.CreateItem(ItemType.Bow);

            Assert.IsType<Bow>(result);
        }

        [Fact]
        public void CreateItem_InvalidType()
        {
            Assert.Throws<ArgumentException>(() => _factory.CreateItem((ItemType)(-1)));
        }
    }

    public class EnchantedItemBuilderTests
    {
        private readonly EnchantedItemBuilder _builder;
        private readonly Mock<IEquippable> _mockItem;

        public EnchantedItemBuilderTests()
        {
            _builder = new EnchantedItemBuilder();
            _mockItem = new Mock<IEquippable>();
            _mockItem.Setup(i => i.Clone()).Returns(_mockItem.Object);
        }

        [Fact]
        public void SetBaseItem_SetsItem()
        {
            _mockItem.Setup(i => i.Name).Returns("test_item");

            var result = _builder.SetBaseItem(_mockItem.Object);

            Assert.Same(_builder, result);
        }

        [Fact]
        public void AddSharpnessEnchantment_WithoutBaseItem()
        {
            Assert.Throws<InvalidOperationException>(() => _builder.AddSharpnessEnchantment());
        }

        [Fact]
        public void AddSharpnessEnchantment()
        {
            _builder.SetBaseItem(_mockItem.Object);
            _builder.AddSharpnessEnchantment();

            var result = _builder.Build();

            Assert.NotNull(result);
        }

        [Fact]
        public void SetDurability_NegativeValue()
        {
            Assert.Throws<ArgumentException>(() => _builder.SetDurability(-1));
        }

        [Fact]
        public void SetDurability_ValidValue()
        {
            var durability = 50;
            _builder.SetBaseItem(_mockItem.Object);
            _builder.SetDurability(durability);

            var result = _builder.Build();

            Assert.NotNull(result);
        }

        [Fact]
        public void Build_WithoutBaseItem()
        {
            Assert.Throws<InvalidOperationException>(() => _builder.Build());
        }

        [Fact]
        public void Reset()
        {
            _builder.SetBaseItem(_mockItem.Object);
            _builder.AddSharpnessEnchantment();
            _builder.SetDurability(50);

            _builder.Reset();

            Assert.Throws<InvalidOperationException>(() => _builder.Build());
        }

        [Fact]
        public void BuildSharpSword()
        {
            var result = _builder.BuildSharpSword();

            Assert.NotNull(result);
        }

        [Fact]
        public void BuildFlameBow()
        {
            var result = _builder.BuildFlameBow();

            Assert.NotNull(result);
        }
    }
}