using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.ModLoader.IO;
using test.Content.Items;

namespace test.Content.Items
{
    public class ShadowSwordGlobal : GlobalItem
    {
        public int killCount = 0;
        public int level = 1;

        public List<int> requiredEvo = new List<int> { 5, 10, 15 };


        public override bool InstancePerEntity => true;

        public override GlobalItem Clone(Item item, Item itemClone)
        {
            var clone = (ShadowSwordGlobal)base.Clone(item, itemClone);
            clone.killCount = killCount;
            clone.level = level;
            return clone;
        }

        public override void ModifyWeaponDamage(Item item, Player player, ref StatModifier damage)
        {
            if (item.ModItem is ShadowSword)
            {
                damage += 0.05f * level; // Damage bertambah 5% per level
            }
        }
        public void AddKill()
        {
            killCount++;

            // Hitung jumlah kill yang dibutuhkan untuk level berikutnya
            int requiredKills = 10 + (level - 1) * 5; // Level 1 butuh 10, level 2 butuh 15, dst.

            if (killCount >= requiredKills && level < 20) // Maks level 20 misalnya
            {
                level++;
                killCount = 0; // Reset killCount setelah naik level
                if (requiredEvo.Contains(level))
                {
                    Main.NewText($"Shadow Sword Have Been Evolved");
                }
                Main.NewText($"Shadow Sword leveled up! Now Level {level}", 180, 85, 250);
            }
        }



        public override void ModifyHitNPC(Item item, Player player, NPC target, ref NPC.HitModifiers modifiers)
        {
            if (item.ModItem is ShadowSword)
            {
                int predictedLife = target.life - (int)(item.damage * player.GetDamage(item.DamageType).Additive);

                if (predictedLife <= 0)
                {
                    var global = item.GetGlobalItem<ShadowSwordGlobal>();
                    global.AddKill();
                }
            }
        }




        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {

            if (item.ModItem is ShadowSword)
            {
                var global = item.GetGlobalItem<ShadowSwordGlobal>();
                int requiredKill = 10 + (global.level - 1) * 5;
                tooltips.Add(new TooltipLine(Mod, "Level", $"Level: {global.level} ({global.killCount}/{requiredKill})"));
            }
        }

        // (Optional) Untuk menyimpan data kalau kamu ingin permanen antar world
        public override void SaveData(Item item, TagCompound tag)
        {
            if (item.ModItem is ShadowSword)
            {
                tag["killCount"] = killCount;
                tag["level"] = level;
            }
        }

        public override void LoadData(Item item, TagCompound tag)
        {
            if (item.ModItem is ShadowSword)
            {
                killCount = tag.GetInt("killCount");
                level = tag.GetInt("level");
            }
        }
    }
}
