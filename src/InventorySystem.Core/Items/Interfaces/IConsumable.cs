namespace InventorySystem.Core.Items.Interfaces
{
    public interface IConsumable : IItem
    {
        int UsesRemaining { get; set; }
        int MaxUses { get; }
        int HealthRestore { get; }
        int HungerRestore { get; }
        void Use(ICharacter target);
        bool IsConsumed();
    }

    public interface ICharacter
    {
        string Name { get; }
        float Health { get; set; }
        float MaxHealth { get; }
        float Hunger { get; set; }
        float MaxHunger { get; }
        float DamageMultiplier { get; set; }
    }
}