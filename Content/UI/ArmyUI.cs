// Content/UI/ArmyUI.cs
using Terraria.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using test.Content.Players;
using Terraria.GameContent.UI.Elements;
using System.Linq;

namespace test.Content.UI
{
    public class ArmyUI : UIState
    {
        public static bool Visible;
        private UITextPanel<string> panel;

        public override void OnInitialize()
        {
            panel = new UITextPanel<string>("Your Army", 0.8f, true)
            {
                Width = new(300f, 0f),
                Height = new(400f, 0f),
                Left = new(500f, 0f),
                Top = new(100f, 0f),
                BackgroundColor = new Color(50, 50, 50, 200)
            };

            Append(panel);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            panel.RemoveAllChildren();

            var player = Main.LocalPlayer.GetModPlayer<TestPlayer>();

            // Pending summon (belum spawn)
            foreach (int npcType in player.pendingSummonNPCTypes.ToList())
            {
                var text = new UIText($"[Spawn] {Lang.GetNPCNameValue(npcType)}");
                text.OnLeftClick += (_, _) =>
                {
                    if (player.TrySummonNPC(npcType))
                        Main.NewText($"{Lang.GetNPCNameValue(npcType)} berhasil dipanggil!", 50, 255, 130);
                    else
                        Main.NewText($"Slot summon tidak cukup untuk {Lang.GetNPCNameValue(npcType)}!", 255, 50, 50);
                };
                panel.Append(text);
            }

            // Summoned NPCs aktif
            foreach (int id in player.summonedNPCs)
            {
                if (id < 0 || id >= Main.maxNPCs) continue;

                NPC npc = Main.npc[id];
                if (!npc.active) continue;

                var text = new UIText($"{npc.TypeName} [Remove]");
                text.OnLeftClick += (_, _) =>
                {
                    npc.active = false;
                };
                panel.Append(text);
            }
        }

    }
}
