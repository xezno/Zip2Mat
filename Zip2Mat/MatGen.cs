using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace Zip2Mat
{
    public static class MatGen
    {
        private const string matLocation = @"C:\Program Files (x86)\Steam\steamapps\common\Half-Life Alyx\content\hlvr_addons\sandbox_maps\materials";

        public static void Generate(string fileName, string matName, bool convertToTga = false, bool normalizeFileNames = false)
        {
            Trace.WriteLine($"Reading file {fileName}");

            var zipFile = ZipFile.OpenRead(fileName);
            Trace.WriteLine($"Using material name {matName}");

            // Enumerate textures / types
            Dictionary<string, TextureData> textureDictionary = new Dictionary<string, TextureData>();
            foreach (var file in zipFile.Entries)
            {
                var textureName = file.Name;
                var textureData = new TextureData(textureName);
                textureDictionary.Add(textureData.type.ToString(), textureData);
            }

            // Convert texture dict to collection
            var textures = new TextureCollection();
            foreach (var property in typeof(TextureCollection).GetProperties())
            {
                if (textureDictionary.TryGetValue(property.Name, out var val))
                {
                    property.SetValue(textures, $"materials/{matName}/{val.name}");

                    // Copy material to HLA location
                    Directory.CreateDirectory(Path.Combine(matLocation, matName));
                    var destPath = Path.Combine(matLocation, matName, val.name);

                    zipFile.Entries.Where(t => t.Name == val.name).First().ExtractToFile(destPath, overwrite: true);
                }
            }

            Trace.WriteLine(GenerateMaterial(textures));

            File.WriteAllText(Path.Combine(matLocation, $"{matName}.vmat"), GenerateMaterial(textures));

            zipFile.Dispose();
        }

        private static string GenerateMaterial(TextureCollection textures)
        {
            var vrTemplate = new VrSimple(textures);
            return vrTemplate.TransformText();
        }
    }
}
