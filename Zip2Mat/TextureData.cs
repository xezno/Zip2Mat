using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Zip2Mat
{
    public class TextureData
    {
        public enum TextureType
        {
            Diffuse,
            Normal,
            Metallic,
            Roughness,
            AmbientOcclusion,
            Height,

            Unknown
        };

        // First alias entry is the 'normalized' one (i.e. the one that Source 2 looks for by default)
        private static Dictionary<TextureType, string[]> textureNames = new Dictionary<TextureType, string[]>()
        {
            { TextureType.Diffuse, new[] { "color" /* default */, "diffuse", "albedo", "col", "basecolor" } },
            { TextureType.Normal, new[] { "normal" /* default */, "bump", "norm" } },
            { TextureType.Metallic, new[] { "metal" /* default */, "metalness", "metallic" } },
            { TextureType.Roughness, new[] { "rough" /* default */, "roughness", "rgh" } },
            { TextureType.AmbientOcclusion, new[] { "ao" /* default */, "ambient", "ambientocclusion" } },
            { TextureType.Height, new[] { "height" /* default */, "parallax", "displacement", "displace" } },
        };

        public string name;
        public TextureType type;

        public TextureData(TextureType type, string name)
        {
            this.name = name;
            this.type = type;

            Trace.WriteLine($"\t{name}: {type}");
        }

        public TextureData(string path) : this(GetMatch(path), path) { }

        private static TextureType GetMatch(string fileName)
        {
            // Get type indicator
            fileName = Path.GetFileNameWithoutExtension(fileName); // Remove ext
            var typeIndicators = fileName.Split('_');

            // TODO: Linq-ify
            foreach (var texturePair in textureNames)
            {
                foreach (var textureAlias in texturePair.Value)
                {
                    foreach (var typeIndicator in typeIndicators)
                    {
                        if (textureAlias.Equals(typeIndicator, StringComparison.OrdinalIgnoreCase))
                        {
                            return texturePair.Key;
                        }
                    }
                }
            }

            return TextureType.Unknown;
        }
    }
}
