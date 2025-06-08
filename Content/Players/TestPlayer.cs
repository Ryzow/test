// Content/Players/TestPlayer.cs
using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;
using test.Content.NPCs;
using Terraria.ID;

namespace test.Content.Players
{
    public class TestPlayer : ModPlayer
    {
        public bool hasArise;
        public int ariseLevel = 1; // 1-25, default level 1
        public List<int> summonedNPCs = new List<int>();

        public int ariseExp;
        public int maxAriseExp => GetMaxExpForLevel(ariseLevel);

        // Dapatkan max exp per level
        public int GetMaxExpForLevel(int level)
        {
            if (level >= 25) return 0; // max
            return 100 + (level - 1) * 50; // contoh: level 1 = 100, level 2 = 150, dst
        }

        // Tambahkan EXP
        public void GainAriseExp(int amount)
        {
            if (Main.hardMode)
                amount *= 2;

            ariseExp += amount;

            // Cek level up
            while (ariseLevel < 25 && ariseExp >= maxAriseExp)
            {
                ariseExp -= maxAriseExp;
                ariseLevel++;
                CombatText.NewText(Player.getRect(), Microsoft.Xna.Framework.Color.Gold, $"Arise Level Up! Level {ariseLevel}");
            }
        }


        // Menghitung slot maksimum yang boleh dipakai sesuai level arise
        public int GetMaxSummonSlots()
        {
            if (ariseLevel >= 15)
                return 20;
            if (ariseLevel >= 10)
                return 4;
            return 2;
        }

        // Slot yang terpakai sekarang
        public int GetUsedSummonSlots()
        {
            int used = 0;
            foreach (int id in summonedNPCs)
            {
                if (id >= 0 && id < Main.npc.Length)
                {
                    var npc = Main.npc[id];
                    if (npc.active && npc.life > 0)
                    {
                        used += GetNPCCost(npc);
                    }
                }
            }
            return used;
        }

        // Biaya slot summon per NPC
        public int GetNPCCost(NPC npc)
        {
            if (npc.boss)
            {
                // Contoh cek pre-hardmode / hardmode, asumsikan
                if (!Main.hardMode) return 10;
                return 13;
            }
            else
            {
                if (!Main.hardMode) return 1;
                return 2;
            }
        }

        public List<int> pendingSummonNPCTypes = new List<int>();

        // Tambahkan fungsi untuk spawn dari UI
        public bool TrySummonNPC(int npcType)
        {
            int slotCost = GetNPCCostByType(npcType);
            if (!CanSummon(slotCost)) return false;

            int npcID = NPC.NewNPC(null, (int)Player.Center.X, (int)Player.Center.Y, npcType);
            Main.npc[npcID].GetGlobalNPC<SummonedFlag>().owner = Player.whoAmI;

            summonedNPCs.Add(npcID);
            pendingSummonNPCTypes.Remove(npcType);

            // Jika arise level >= 15, aktifkan ability summoned (custom property bisa kamu tambah)
            if (ariseLevel >= 15)
            {
                // Contoh trigger ability, kamu bisa sesuaikan di NPC itu sendiri
                var summoned = Main.npc[npcID].ModNPC as ISummonableAbility;
                summoned?.ActivateAbility();
            }
            return true;
        }

        public int GetNPCCostByType(int npcType)
        {
            NPC npc = new NPC();
            npc.SetDefaults(npcType);
            if (npc.boss)
            {
                if (!Main.hardMode) return 10;
                return 13;
            }
            else
            {
                if (!Main.hardMode) return 1;
                return 2;
            }
        }


        // Mengecek apakah masih bisa summon NPC dengan slot cost tertentu
        public bool CanSummon(int cost)
        {
            return GetUsedSummonSlots() + cost <= GetMaxSummonSlots();
        }

        // Fungsi update untuk bersihkan summonedNPCs yg sudah mati
        public override void PostUpdate()
        {
            summonedNPCs.RemoveAll(id => id < 0 || id >= Main.npc.Length || !Main.npc[id].active || Main.npc[id].life <= 0);
        }
    }

}
