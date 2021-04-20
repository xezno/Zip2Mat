using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace Zip2Mat
{
    public class TextureCollection
    {
        [NormalizeAs("color")]
        public string Diffuse { get; set; } = null;

        [NormalizeAs("normal")]
        public string Normal { get; set; } = null;

        [NormalizeAs("metal")]
        public string Metallic { get; set; } = null;

        [NormalizeAs("rough")]
        public string Roughness { get; set; } = null;

        [NormalizeAs("ao")]
        public string AmbientOcclusion { get; set; } = null;

        [NormalizeAs("height")]
        public string Height { get; set; } = null;


        public TextureCollection(string matName, Dictionary<string, TextureData> textureDictionary, ZipArchive zipArchive, bool convertToTga, bool normalizeFileNames)
        {
            foreach (var property in typeof(TextureCollection).GetProperties())
            {
                if (textureDictionary.TryGetValue(property.Name, out var val))
                {
                    var destTextureFileName = val.name;
                    if (convertToTga)
                    {
                        destTextureFileName = $"{Path.GetFileNameWithoutExtension(destTextureFileName)}.tga";
                    }
                    if (normalizeFileNames)
                    {
                        var normalizeAsAttribute = property.GetCustomAttributes(typeof(NormalizeAsAttribute), inherit: false).First() as NormalizeAsAttribute;

                        destTextureFileName = $"{matName}_{normalizeAsAttribute.NormalizedType}{Path.GetExtension(destTextureFileName)}";
                    }

                    property.SetValue(this, $"materials/{matName}/{destTextureFileName}");

                    // Copy material to HLA location
                    Directory.CreateDirectory(Path.Combine(MainSettings.Instance.MatLocation, matName));
                    var destPath = Path.Combine(MainSettings.Instance.MatLocation, matName, destTextureFileName);
                    ZipArchiveEntry materialFile = zipArchive.Entries.Where(t => t.Name == val.name).First();

                    if (convertToTga)
                    {
                        // Read to bitmap, export as tga
                        using var materialBitmap = new Bitmap(materialFile.Open());
                        var tgaFile = TgaSharp.TGA.FromBitmap(materialBitmap);
                        tgaFile.Save(destPath);
                    }
                    else
                    {
                        // Extract directly
                        materialFile.ExtractToFile(destPath, overwrite: true);
                    }
                }
            }
        }
    }
}
