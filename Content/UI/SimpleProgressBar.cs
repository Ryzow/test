using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;
using Terraria;

public class SimpleProgressBar : UIElement
{
    private float progress;
    public Color BackColor = Color.Gray;
    public Color FillColor = Color.Cyan;
    private static Texture2D _whiteTexture;

    private void EnsureWhiteTexture()
    {
        if (_whiteTexture == null || _whiteTexture.IsDisposed)
        {
            _whiteTexture = new Texture2D(Main.graphics.GraphicsDevice, 1, 1);
            _whiteTexture.SetData(new[] { Color.White });
        }
    }



    public void SetProgress(float value)
    {
        progress = MathHelper.Clamp(value, 0f, 1f);
    }

    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
        base.DrawSelf(spriteBatch);
        EnsureWhiteTexture(); // Pastikan tekstur putih tersedia

        CalculatedStyle dim = GetInnerDimensions();

        // Draw background
        spriteBatch.Draw(_whiteTexture, new Rectangle((int)dim.X, (int)dim.Y, (int)dim.Width, (int)dim.Height), BackColor);

        // Draw fill
        spriteBatch.Draw(_whiteTexture, new Rectangle((int)dim.X, (int)dim.Y, (int)(dim.Width * progress), (int)dim.Height), FillColor);
    }


}
