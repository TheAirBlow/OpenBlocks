using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OpenBlocks.Main
{
    /// <summary>
    /// Helper class
    /// </summary>
    public class Helper
    {
        /// <summary>
        /// Switch wrapping textures. Should be disabled after done using it.
        /// </summary>
        /// <param name="manager">GraphicsDeviceManager instance</param>
        /// <param name="value">Enable/disable</param>
        public static void SwitchWrapTextures(GraphicsDeviceManager manager, bool value)
        {
            SamplerState s = new SamplerState();
            if (value)
            {
                s.AddressU = TextureAddressMode.Wrap;
                s.AddressV = TextureAddressMode.Wrap;
                s.AddressW = TextureAddressMode.Wrap;
                s.Filter = TextureFilter.Point;
            }

            manager.GraphicsDevice.SamplerStates[0] = s;
        }

        /// <summary>
        /// Switch transparency support. Should be disabled after done using it.
        /// </summary>
        /// <param name="manager">GraphicsDeviceManager instance</param>
        /// <param name="value">Enable/disable</param>
        public static void SwitchTransparency(GraphicsDeviceManager manager, bool value)
        {
            manager.GraphicsDevice.DepthStencilState.DepthBufferWriteEnable = value;
            manager.GraphicsDevice.DepthStencilState.DepthBufferEnable = value;
        }

        /// <summary>
        /// Change texture's brightness
        /// </summary>
        /// <param name="texture">Texture to modify</param>
        /// <param name="factor">Brightness factor</param>
        public static Texture2D TextureBrightness(Texture2D texture, float factor)
        {
            Color[] colorData = new Color[texture.Bounds.Width * texture.Bounds.Height];
            texture.GetData(0, texture.Bounds, colorData, 0, colorData.Length);
            for (int i = 0; i < colorData.Length; i++)
            {
                Color color = colorData[i];
                float red = color.R;
                float green = color.G;
                float blue = color.B;
                factor = 1 + factor;
                red *= factor;
                green *= factor;
                blue *= factor;
                colorData[i] = new Color(red, green, blue);
            }

            var clone = new Texture2D(Entrypoint.Game.GraphicsDevice, texture.Bounds.Width, texture.Bounds.Height);
            clone.SetData(colorData);
            return clone;
        }
    }
}