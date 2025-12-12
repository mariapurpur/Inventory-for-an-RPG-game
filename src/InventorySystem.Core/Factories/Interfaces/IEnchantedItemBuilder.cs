using InventorySystem.Core.Items.Interfaces;

namespace InventorySystem.Core.Factories.Interfaces
{
    public interface IEnchantedItemBuilder
    {
        IEnchantedItemBuilder SetBaseItem(IEquippable baseItem);
        IEnchantedItemBuilder AddSharpnessEnchantment();
        IEnchantedItemBuilder AddProtectionEnchantment();
        IEnchantedItemBuilder AddFlameEnchantment();
        IEnchantedItemBuilder SetDurability(int durability);
        IEnchantedItemBuilder SetRarity(ItemRarity rarity);
        IEquippable Build();
        void Reset();
    }
}