
// test.cs using Terraria.ModLoader;
using Terraria;
using Terraria.UI;
using Microsoft.Xna.Framework.Input;
using test.Content.UI;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;


namespace test
{
	public class Test : Mod
	{
		public static ModKeybind ToggleArmyUI;

		public override void Load()
		{
			ToggleArmyUI = KeybindLoader.RegisterKeybind(this, "Open Army", "I");
		}

		public override void Unload()
		{
			ToggleArmyUI = null;
		}
	}
	public class InputSystem : ModSystem
	{
		public override void PostUpdateInput()
		{
			if (Test.ToggleArmyUI.JustPressed)
				ArmyUI.Visible = !ArmyUI.Visible;
		}

		public override void Load()
		{
			if (!Main.dedServ)
			{
				ariseUI = new AriseUI();
				ariseUI.Activate();
				UserInterface = new UserInterface();
				UserInterface.SetState(ariseUI);
			}
		}

		UserInterface UserInterface;
		internal AriseUI ariseUI;

		public override void UpdateUI(GameTime gameTime)
		{
			UserInterface?.Update(gameTime);
		}

		public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
		{
			int inventoryIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Inventory"));
			if (inventoryIndex != -1)
			{
				layers.Insert(inventoryIndex, new LegacyGameInterfaceLayer(
					"Arise: UI",
					delegate
					{
						UserInterface.Draw(Main.spriteBatch, new GameTime());
						return true;
					},
					InterfaceScaleType.UI)
				);
			}
		}

	}

}

