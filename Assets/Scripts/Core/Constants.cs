using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Constants
{
    public class Levels
    {
        public static readonly byte MainMenu = 0x0;
        public static readonly byte SampleScene = 0x1;
    }

    public class ColorLibrary
    {
        public static readonly string blue = "blue";
        public static readonly string green = "green";
        public static readonly string red = "red";
        public static readonly string yellow = "yellow";
    }

    public class EntityColor
    {
        public static readonly string blue = "blue";
        public static readonly string green = "green";
        public static readonly string red = "red";
        public static readonly string yellow = "yellow";
        public static Dictionary<string, string> hexCode = new Dictionary<string, string>()
        {
            {blue, "#4D4DFF"},
            {green, "#00B359"},
            {red, "#FF6666"},
            {yellow, "#FFFF4D"}
        };

        public static List<string> GetAllColorKeys()
        {
            return new List<string>(){
                blue,
                green,
                red,
                yellow
            };
        }

        private static string GetRandomColorHexFromList(List<string> colors)
        {
            int index = Random.Range(0, colors.Count);
            string key = colors[index];
            return hexCode[key];
        }

        public static string GetRandomColorHex()
        {
            List<string> colors = GetAllColorKeys();
            return GetRandomColorHexFromList(colors);
        }

        public static string GetRandomColorHex(List<string> excludes)
        {
            List<string> colors = GetAllColorKeys();
            colors.RemoveAll(color => excludes.Contains(color));
            return GetRandomColorHexFromList(colors);
        }

        public static Color TranslateHexToColor(string hex)
        {
            Color color;
            ColorUtility.TryParseHtmlString(hex, out color);
            return color;
        }
    }
}
