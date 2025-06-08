// Content/NPCs/AriseGlobalNPC.cs
using Terraria;
using Terraria.ModLoader;
using test.Content.Players;

public class AriseGlobalNPC : GlobalNPC
{
    public override void OnKill(NPC npc)
    {
        if (npc.playerInteraction[Main.myPlayer]) // pastikan player bunuh
        {
            int exp = 0;

            if (npc.boss)
                exp = 500;
            else if (npc.lifeMax >= 200)
                exp = 50;
            else
                exp = 10;

            var modPlayer = Main.LocalPlayer.GetModPlayer<TestPlayer>();
            modPlayer.GainAriseExp(exp);
        }
    }
}
