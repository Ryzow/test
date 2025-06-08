// Content/NPCs/TestGlobalNPC.cs
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using test.Content.Players;

namespace test.Content.NPCs
{
    public class TestGlobalNPC : GlobalNPC
{
    public override void OnKill(NPC npc)
    {
        Player killer = npc.lastInteraction != -1 ? Main.player[npc.lastInteraction] : null;
        if (killer == null) return;

        var modPlayer = killer.GetModPlayer<TestPlayer>();
        if (!modPlayer.hasArise) return;

        // Pastikan undead atau monster yang boleh disummon
        int[] summonableNPCs = { NPCID.Zombie, NPCID.Skeleton, NPCID.EyeofCthulhu }; // Tambah sesuai

        if (!summonableNPCs.Contains(npc.type)) return;

        // Masukkan NPC ke UI pending summon (bisa berupa list ID NPC yang ditunggu spawn)
        modPlayer.pendingSummonNPCTypes.Add(npc.type);

        // Beri notifikasi
        Main.NewText($"{killer.name} mendapatkan opsi summon {Lang.GetNPCNameValue(npc.type)}! Buka Army UI untuk summon.", 50, 255, 130);
    }
}

}
