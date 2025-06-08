// Content/NPCs/SummonedFlag.cs
using Terraria;
using Terraria.ModLoader;

namespace test.Content.NPCs
{
    public class SummonedFlag : GlobalNPC
    {
        public int owner = -1;

        public override bool InstancePerEntity => true;

        public override bool CanBeHitByNPC(NPC npc, NPC attacker)
        {
            // Don’t let army hurt each other
            if (attacker.GetGlobalNPC<SummonedFlag>().owner == owner)
                return false;

            return base.CanBeHitByNPC(npc, attacker);
        }
    }
}
