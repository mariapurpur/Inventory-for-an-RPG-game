using System;
using System.Collections.Generic;
using InventorySystem.Core.Player;
using InventorySystem.Core.Inventory;
using InventorySystem.Core.Items.Interfaces;
using InventorySystem.Core.Factories;
using InventorySystem.Core.Strategies.Usage;

namespace InventorySystem.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("ДОБРО ПОЖАЛОВАТЬ В НЕМАЙНКРАФТ!\n");

            var player = new Player("кирилл");
            System.Console.WriteLine($"наш подопытный: {player.Name}\n");

            var factory = new ItemFactory();

            var sword = (IEquippable)factory.CreateSword();
            var helmet = (IEquippable)factory.CreateHelmet();
            var apple = (IConsumable)factory.CreateApple();
            var healthPotion = (IConsumable)factory.CreateHealthPotion();
            var bow = (IEquippable)factory.CreateBow();

            System.Console.WriteLine("создаём предметы для тестов:");
            System.Console.WriteLine($"- {sword.Name}");
            System.Console.WriteLine($"- {helmet.Name}");
            System.Console.WriteLine($"- {apple.Name}");
            System.Console.WriteLine($"- {healthPotion.Name}");
            System.Console.WriteLine($"- {bow.Name}\n");

            var inventory = player.Inventory;
            inventory.AddItem(sword);
            inventory.AddItem(helmet);
            inventory.AddItem(apple);
            inventory.AddItem(healthPotion);
            inventory.AddItem(bow);

            DisplayInventory(inventory);

            var equipStrategy = new EquipStrategy();
            equipStrategy.Use(player, helmet);
            System.Console.WriteLine($"{player.Name} подумал и экипировал шлем.");

            var eatStrategy = new EatStrategy();
            eatStrategy.Use(player, apple);
            System.Console.WriteLine($"\nпосле этого {player.Name} проголодался и решил покушать яблочко!");
            System.Console.WriteLine($"его здоровье: {player.Health}, голод: {player.Hunger}, состояние: {player.CurrentState.Name}\n");

            eatStrategy.Use(player, healthPotion);
            System.Console.WriteLine($"{player.Name} испугался, что в яблоке живут злые бактерии, поэтому выпил зелье здоровья.");
            System.Console.WriteLine($"здоровье теперь: {player.Health}\n");

            var builder = new EnchantedItemBuilder();
            var enhancedSword = builder.SetBaseItem(sword)
                                      .AddSharpnessEnchantment()
                                      .Build();

            inventory.RemoveItem(sword.Id);
            inventory.AddItem(enhancedSword);

            System.Console.WriteLine($"что же это?! похоже, наш {player.Name} зачаровал свой меч! {enhancedSword.Name} теперь имеет остроту.\n");
            System.Console.WriteLine($"молодец, {player.Name}!\n");

            equipStrategy.Use(player, enhancedSword);
            System.Console.WriteLine($"{player.Name} экипировал улучшенный меч. выглядит устрашающе!\n");

            var equipment = player.Equipment;
            var stats = equipment.GetEquipmentStats();
            System.Console.WriteLine($"-- нынешняя экипировка --");
            System.Console.WriteLine($"всего экипировано: {stats.TotalItems}");
            System.Console.WriteLine($"бонус к урону: {equipment.GetTotalBonus("урон")}");
            System.Console.WriteLine($"бонус к защите: {equipment.GetTotalBonus("защита")}\n");

            // 10. Финальный отчет
            System.Console.WriteLine("-- состояние --");
            System.Console.WriteLine($"игрок: {player.Name}");
            System.Console.WriteLine($"здоровье: {player.Health}/{player.MaxHealth}");
            System.Console.WriteLine($"голод: {player.Hunger}/{player.MaxHunger}");
            System.Console.WriteLine($"состояние: {player.CurrentState.Name}");
            System.Console.WriteLine($"множитель урона: {player.DamageMultiplier}\n");

            DisplayInventory(inventory);

            System.Console.WriteLine("до встречи!");
        }

        static void DisplayInventory(InventorySystem.Core.Inventory.Interfaces.IInventory inventory)
        {
            var manager = new InventoryManager(inventory);
            System.Console.WriteLine(manager.GetInventorySummary());
        }
    }
}