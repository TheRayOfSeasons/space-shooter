using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Constants
{
    public class EntityColor
    {
        public static readonly string blue = "";
        public static readonly string green = "";
        public static readonly string red = "";
        public static readonly string yellow = "";

        public static List<string> GetAllColors()
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
            return colors[index];
        }

        public static string GetRandomColorHex()
        {
            List<string> colors = GetAllColors();
            return GetRandomColorHexFromList(colors);
        }

        public static string GetRandomColorHex(List<string> excludes)
        {
            List<string> colors = GetAllColors();
            colors.RemoveAll(color => excludes.Contains(color));
            return GetRandomColorHexFromList(colors);
        }
    }
}
