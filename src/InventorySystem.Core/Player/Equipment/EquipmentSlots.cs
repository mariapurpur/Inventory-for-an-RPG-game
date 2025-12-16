namespace InventorySystem.Core.Player.Equipment
{
    public enum EquipmentSlot
    {
        Head,
        Chest,
        Hand,
        BothHands
    }

    public static class EquipmentSlots
    {
        public static EquipmentSlot[] AllSlots = (EquipmentSlot[])System.Enum.GetValues(typeof(EquipmentSlot));

        public static EquipmentSlot[] ArmorSlots = { 
            EquipmentSlot.Head,
            EquipmentSlot.Chest,
        };

        public static EquipmentSlot[] WeaponSlots = { 
            EquipmentSlot.Hand,
            EquipmentSlot.BothHands
        };
        
        public static string GetSlotName(EquipmentSlot slot)
        {
            return slot switch
            {
                EquipmentSlot.Head => "голова",
                EquipmentSlot.Chest => "грудь",
                EquipmentSlot.Hand => "рука",
                EquipmentSlot.BothHands => "обе руки",
                _ => "неизвестный слот"
            };
        }

        public static bool CanEquipInSlot(EquipmentSlot itemSlot, EquipmentSlot targetSlot)
        {
            return itemSlot == targetSlot;
        }
    }
}