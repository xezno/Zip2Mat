using System;

namespace Zip2Mat
{
    public class NormalizeAsAttribute : Attribute
    {
        public string NormalizedType { get; }

        public NormalizeAsAttribute(string normalizedType)
        {
            NormalizedType = normalizedType;
        }
    }
}