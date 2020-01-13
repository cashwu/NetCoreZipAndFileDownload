using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.FileProviders.Physical;

namespace testZip_File
{
    public class NasFileProvider
    {
        public NasFileProvider(string rootPath)
        {
            RootPath = rootPath;
            Provider = new PhysicalFileProvider(rootPath);
        }

        public IFileProvider Provider { get; }

        public string RootPath { get; }
    }
    
    public class NasFileProviderNew : PhysicalFileProvider
    {
        public NasFileProviderNew(string root)
            : base(root)
        {
        }

        public NasFileProviderNew(string root, ExclusionFilters filters)
            : base(root, filters)
        {
        }
    }
}