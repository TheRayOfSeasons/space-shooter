using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Constants
{
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
            {blue, "#4d4dff"},
            {green, "#00b359"},
            {red, "#ff6666"},
            {yellow, "#ffff4d"}
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
