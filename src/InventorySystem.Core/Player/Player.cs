using InventorySystem.Core.Items.Interfaces;
using InventorySystem.Core.Inventory.Interfaces;
using InventorySystem.Core.Player.States.PlayerStates;
using InventorySystem.Core.Player.Equipment;
using InventorySystem.Core.Player.States;

namespace InventorySystem.Core.Player
{
    public class Player : ICharacter
    {
        public string Name { get; private set; }
        public float Health { get; set; }
        public float MaxHealth { get; private set; }
        public float Hunger { get; set; }
        public float MaxHunger { get; private set; }
        public float DamageMultiplier { get; set; }
        
        public IInventory Inventory { get; private set; }
        public EquipmentManager Equipment { get; private set; }
        public PlayerState CurrentState { get; set; }
        
        public Player(string name, float maxHealth = 100, float maxHunger = 100)
        {
            Name = name;
            MaxHealth = maxHealth;
            Health = maxHealth;
            MaxHunger = maxHunger;
            Hunger = maxHunger;
            DamageMultiplier = 1;
            Inventory = new InventorySystem.Core.Inventory.Inventory(20);
            Equipment = new EquipmentManager(this);
            CurrentState = new HealthyState();
        }
        
        public bool Equip(IEquippable item)
        {
            if (item == null) 
                return false;
            
            return Equipment.Equip(item);
        }
        
        public bool Unequip(InventorySystem.Core.Player.Equipment.EquipmentSlot slot)
        {
            return Equipment.Unequip(slot);
        }

        public void Heal(float amount)
        {
            Health += amount;
            if (Health > MaxHealth) Health = MaxHealth;
        }
        
        public void TakeDamage(float amount)
        {
            Health -= amount;
            if (Health < 0) Health = 0;
        }
        
        public void ConsumeHunger(float amount)
        {
            Hunger += amount;
            if (Hunger > MaxHunger) Hunger = MaxHunger;
            else if (Hunger < 0) Hunger = 0;
        }
        
        public bool IsAlive => Health > 0;
    }
}