using InventorySystem.Core.Items.Abstractions;
using InventorySystem.Core.Items.Interfaces;

namespace InventorySystem.Core.Items.Food
{
    public class Apple : FoodBase
    {
        public Apple(int id = 201, string name = "яблочко") 
            : base(
                id: id,
                name: name,
                healthRestore: 2,
                hungerRestore: 3,
                maxStackSize: 64,
                maxUses: 1,
                rarity: ItemRarity.Common)
        {
        }
        
        public override IItem Clone()
        {
            return new Apple(Id, Name)
            {
                CurrentStack = this.CurrentStack,
                UsesRemaining = this.UsesRemaining,
                Rarity = this.Rarity
            };
        }
    }
}