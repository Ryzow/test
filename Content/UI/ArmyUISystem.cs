// Content/UI/ArmyUISystem.cs
using Terraria.ModLoader;
using Terraria.UI;
using Microsoft.Xna.Framework.Input;
using Terraria;
using Microsoft.Xna.Framework;
using System.Collections.Generic;


namespace test.Content.UI
{
    public class ArmyUISystem : ModSystem
    {
        internal UserInterface armyInterface;
        internal ArmyUI armyUI;

        public override void Load()
        {
            if (!Main.dedServ)
            {
                armyUI = new ArmyUI();
                armyInterface = new UserInterface();
                armyInterface.SetState(armyUI);
            }
        }

        public override void UpdateUI(GameTime gameTime)
        {
            if (ArmyUI.Visible)
                armyInterface?.Update(gameTime);
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int inventoryIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Inventory"));
            if (inventoryIndex != -1 && ArmyUI.Visible)
            {
                layers.Insert(inventoryIndex, new LegacyGameInterfaceLayer(
                    "Army UI",
                    () =>
                    {
                        armyInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI));
            }
        }
    }
}
