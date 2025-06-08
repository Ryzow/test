// test.cs
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Input;
using Terraria.UI;
using System.Collections.Generic;
using test.Content.UI;

namespace test
{
    public class Test : Mod
    {
        // Keybind to toggle the Army UI
        public static ModKeybind ToggleArmyUI;

        public override void Load()
        {
            // Register keybind for toggling Army UI with default key 'I'
            ToggleArmyUI = KeybindLoader.RegisterKeybind(this, "Open Army", "I");
        }

        public override void Unload()
        {
            // Clean up keybind reference
            ToggleArmyUI = null;
        }
    }

    public class InputSystem : ModSystem
    {
        // UserInterface and UIState for Army UI
        private UserInterface _armyInterface;
        private ArmyUI _armyUI;

        public override void Load()
        {
            if (!Main.dedServ)
            {
                _armyUI = new ArmyUI();
                _armyInterface = new UserInterface();
                _armyInterface.SetState(_armyUI);
            }
        }

        public override void Unload()
        {
            _armyUI = null;
            _armyInterface = null;
        }

        public override void PostUpdateInput()
        {
            if (Test.ToggleArmyUI?.JustPressed == true)
            {
                ArmyUI.Visible = !ArmyUI.Visible;
            }
        }

        public override void UpdateUI(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (ArmyUI.Visible)
            {
                _armyInterface?.Update(gameTime);
            }
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
                        _armyInterface.Draw(Main.spriteBatch, new Microsoft.Xna.Framework.GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI));
            }
        }
    }
}

