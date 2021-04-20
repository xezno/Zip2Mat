using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace Zip2Mat
{
    public static class MatGen
    {
        public static void Generate(string fileName, string matName, bool convertToTga = false, bool normalizeFileNames = false)
        {
            var zipArchive = ZipFile.OpenRead(fileName);

            // Enumerate textures / types
            Dictionary<string, TextureData> textureDictionary = new Dictionary<string, TextureData>();
            foreach (var file in zipArchive.Entries)
            {
                var textureName = file.Name;
                var textureData = new TextureData(textureName);
                textureDictionary.Add(textureData.type.ToString(), textureData);
            }

            // Convert texture dict to texture collection
            var textures = new TextureCollection(matName, textureDictionary, zipArchive);

            // Write to vmat
            var generatedMaterial = GenerateMaterial(textures);
            var filePath = Path.Combine(MainSettings.Instance.MatLocation, $"{matName}.vmat");
            File.WriteAllText(filePath, generatedMaterial);

            zipArchive.Dispose();
        }

        private static string GenerateMaterial(TextureCollection textures)
        {
            var vrTemplate = new VrSimple(textures);
            return vrTemplate.TransformText();
        }
    }
}
