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

        // Normalization is handled in TextureCollection.cs under the NormalizeAs attribute
        private static Dictionary<TextureType, string[]> textureNames = new Dictionary<TextureType, string[]>()
        {
            { TextureType.Diffuse, new[] { "color", "diffuse", "albedo", "col", "basecolor" } },
            { TextureType.Normal, new[] { "normal", "bump", "norm" } },
            { TextureType.Metallic, new[] { "metal", "metalness", "metallic" } },
            { TextureType.Roughness, new[] { "rough", "roughness", "rgh" } },
            { TextureType.AmbientOcclusion, new[] { "ao", "ambient", "ambientocclusion" } },
            { TextureType.Height, new[] { "height", "parallax", "displacement", "displace" } },
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
