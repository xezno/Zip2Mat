using System.Collections.Generic;
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


        public TextureCollection(string matName, Dictionary<string, TextureData> textureDictionary, ZipArchive zipArchive)
        {
            foreach (var property in typeof(TextureCollection).GetProperties())
            {
                if (textureDictionary.TryGetValue(property.Name, out var val))
                {
                    property.SetValue(this, $"materials/{matName}/{val.name}");

                    // Copy material to HLA location
                    Directory.CreateDirectory(Path.Combine(MainSettings.Default.MatLocation, matName));
                    var destPath = Path.Combine(MainSettings.Default.MatLocation, matName, val.name);

                    zipArchive.Entries.Where(t => t.Name == val.name).First().ExtractToFile(destPath, overwrite: true);
                }
            }
        }
    }
}
