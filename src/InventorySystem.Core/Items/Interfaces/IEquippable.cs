namespace InventorySystem.Core.Items.Interfaces
{
    public interface IEquippable : IItem
    {
        Player.Equipment.EquipmentSlot Slot { get; }
        int Durability { get; set; }
        int MaxDurability { get; }
        
        bool Equip();
        void Unequip();
        bool IsBroken();
    }
}