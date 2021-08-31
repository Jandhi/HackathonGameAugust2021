using Microsoft.Xna.Framework;

namespace Game.UI
{
    public class Theme
    {
        public static Theme CurrentTheme = new Theme();

        public Color TextColor { get; set; } = Color.White;
        public Color MainColor { get; set; } = Color.Gold;
        public Color AccentColor { get; set; } = Color.Gray;
        public Color HoveredColor { get; set; } = Color.Red;
        private Theme() {}
        public Theme(Theme theme)
        {
            TextColor = theme.TextColor;
            MainColor = theme.MainColor;
            AccentColor = theme.AccentColor;
            HoveredColor = theme.HoveredColor;
        }

    }
}