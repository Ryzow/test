// Content/Items/Arise.cs
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using test.Content.Players;

namespace test.Content.Items
{
    public class Arise : ModItem
    {
        public override void SetStaticDefaults()
        {
            
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 30;
            Item.accessory = true;
            Item.rare = ItemRarityID.LightRed;
            Item.value = Item.buyPrice(gold: 5);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            var modPlayer = player.GetModPlayer<TestPlayer>();
            modPlayer.hasArise = true;

            // Set the level based on the accessory's prefix or any other logic
            modPlayer.ariseLevel = Item.prefix + 1; // Example logic, adjust as needed
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.PlatinumBar, 50)
                .AddIngredient(ItemID.SilverBar, 20)
                .AddIngredient(ItemID.CopperBar, 10)
                .AddIngredient(ItemID.Chain, 15)
                .AddIngredient(ItemID.Leather, 3)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}
