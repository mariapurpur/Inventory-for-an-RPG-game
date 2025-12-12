using InventorySystem.Core.Items.Abstractions;
using InventorySystem.Core.Items.Interfaces;

namespace InventorySystem.Core.Items.Food
{
    public class Steak : FoodBase
    {
        public bool IsCooked { get; private set; }
        
        public Steak(int id = 202, string name = "жареный стейк", bool isCooked = true) 
            : base(
                id: id,
                name: name,
                healthRestore: 8,
                hungerRestore: 10,
                maxStackSize: 64,
                maxUses: 2,
                rarity: ItemRarity.Common)
        {
            IsCooked = isCooked;
        }
        
        // public override void Use(ICharacter target)
        // {            
        //    base.Use(target);
        // }
        
        public override IItem Clone()
        {
            return new Steak(Id, Name, IsCooked)
            {
                CurrentStack = this.CurrentStack,
                UsesRemaining = this.UsesRemaining,
                Rarity = this.Rarity
            };
        }
    }
}