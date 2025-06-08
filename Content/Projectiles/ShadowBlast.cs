using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using test.Content.Items;

namespace test.Content.Projectiles
{
    public class ShadowBlast : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 3; // 3 animation frames
        }

        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.aiStyle = 1; // Follows bullet behavior
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = 4;
            Projectile.timeLeft = 300;
            Projectile.light = 0.3f;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            AIType = ProjectileID.MagicMissile;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Player player = Main.player[Projectile.owner];

            if (player.HeldItem.ModItem is ShadowSword)
            {
                var global = player.HeldItem.GetGlobalItem<ShadowSwordGlobal>();

                // Level 5+: Tambah debuff ShadowFlame
                if (global.level >= 5)
                    target.AddBuff(BuffID.ShadowFlame, 300);
            }

            // Cek apakah target mati
            if (target.life <= 0 && Projectile.owner == Main.myPlayer)
            {
                if (player.HeldItem.ModItem is ShadowSword)
                {
                    var global = player.HeldItem.GetGlobalItem<ShadowSwordGlobal>();
                    global.AddKill();

                    // Level 15+: Ledakan saat membunuh musuh
                    if (global.level >= 15)
                    {
                        for (int i = 0; i < 20; i++)
                        {
                            int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Shadowflame);
                            Main.dust[dust].velocity *= 1.5f;
                        }
                        // Tambahkan efek ledakan damage jika mau
                        // Projectile.NewProjectile(...);
                    }
                }
            }
        }


        public override void AI()
        {

            Projectile.rotation = Projectile.velocity.ToRotation();


            // Animation
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 6)
            {
                Projectile.frameCounter = 0;
                Projectile.frame++;
                if (Projectile.frame >= Main.projFrames[Projectile.type])
                {
                    Projectile.frame = 0;
                }
            }

            // Optional: Add a shadowy dust effect
            int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Shadowflame, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100, default, 1f);
            Main.dust[dust].noGravity = true;
        }
    }
}
