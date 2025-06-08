// Content/NPCs/SummonedZombie.cs
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace test.Content.NPCs
{
    public class SummonedZombie : ModNPC, ISummonableAbility
    {
        public override string Texture => "Terraria/Images/NPC_" + NPCID.Zombie;

        public override void SetDefaults()
        {
            NPC.width = 18;
            NPC.height = 40;
            NPC.aiStyle = 3;
            AIType = NPCID.Zombie;
            AnimationType = NPCID.Zombie;
            NPC.friendly = false;
            NPC.damage = 15;
            NPC.defense = 5;
            NPC.lifeMax = 100;
            NPC.noGravity = false;
            NPC.knockBackResist = 0.5f;
        }

        public override void AI()
        {
            NPC.TargetClosest(true);
        }

        public void ActivateAbility()
        {
            // Ability contoh: heal 20 HP saat dipanggil
            CombatText.NewText(NPC.getRect(), Microsoft.Xna.Framework.Color.Green, "+20");
            NPC.life += 20;
            if (NPC.life > NPC.lifeMax) NPC.life = NPC.lifeMax;
        }
    }
}
