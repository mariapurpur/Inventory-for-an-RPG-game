using InventorySystem.Core.Items.Abstractions;
using InventorySystem.Core.Items.Interfaces;

namespace InventorySystem.Core.Items.Food
{
    public class Bread : FoodBase
    {
        public Bread(int id = 203, string name = "багет") 
            : base(
                id: id,
                name: name,
                healthRestore: 5,
                hungerRestore: 7,
                maxStackSize: 64,
                maxUses: 3,
                rarity: ItemRarity.Common)
        {
        }
        
        public override IItem Clone()
        {
            return new Bread(Id, Name)
            {
                CurrentStack = this.CurrentStack,
                UsesRemaining = this.UsesRemaining,
                Rarity = this.Rarity
            };
        }
    }
}