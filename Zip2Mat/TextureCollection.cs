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
        public string Diffuse { get; set; } = null;
        public string Normal { get; set; } = null;
        public string Metallic { get; set; } = null;
        public string Roughness { get; set; } = null;
        public string AmbientOcclusion { get; set; } = null;
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
                        destTextureFileName = $"{Path.GetFileNameWithoutExtension(val.name)}.tga";
                    }
                    if (normalizeFileNames)
                    {
                        // TODO
                    }

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
