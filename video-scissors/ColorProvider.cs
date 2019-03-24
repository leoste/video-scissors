using System;
using System.Drawing;

namespace Scissors
{
    public static class ColorProvider
    {
        private static int[,] sliceColors = new int[,]
        {
            { 54, 78, 85 },
            { 85, 82, 51 },
            { 77, 63, 41 },
            { 85, 55, 54 },
            { 63, 55, 77 },
            { 55, 66, 85 },
            { 41, 85, 54 },
            { 63, 85, 55 }
        };

        private static int[,] layerColors = new int[,]
        {
            { 162, 235, 255 },
            { 255, 248, 153 },
            { 232, 189, 123 },
            { 255, 167, 164 },
            { 195, 166, 232 },
            { 166, 200, 255 },
            { 123, 255, 163 },
            { 189, 255, 166 }
        };

        private static Color GetRandomColor(int[,] colors)
        {
            Random rnd = new Random();
            int id = rnd.Next(0, colors.GetUpperBound(0));
            Color color = Color.FromArgb(colors[id, 0], colors[id, 1], colors[id, 2]);
            return color;
        }

        public static Color GetRandomSliceColor()
        {
            return GetRandomColor(sliceColors);
        }

        public static Color GetRandomLayerColor()
        {
            return GetRandomColor(layerColors);
        }
        
        public static Color GetToggleColor(bool toggle)
        {
            if (toggle) return Color.LightGreen;
            else return Color.LightCoral;
        }

        public static Color Mix(Color from, Color to, float percent)
        {
            float amountFrom = 1.0f - percent;

            return Color.FromArgb(
                (int)(from.A * amountFrom + to.A * percent),
                (int)(from.R * amountFrom + to.R * percent),
                (int)(from.G * amountFrom + to.G * percent),
                (int)(from.B * amountFrom + to.B * percent));
        }
    }
}
