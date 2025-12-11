namespace InventorySystem.Core.Items.Interfaces
{
    public interface IEquippable : IItem
    {
        EquipmentSlot Slot { get; }
        int Durability { get; set; }
        int MaxDurability { get; }
        
        bool Equip();
        void Unequip();
        bool IsBroken();
    }

    public enum EquipmentSlot
    {
        Head,
        Chest,
        Hand,
        BothHands
    }
}