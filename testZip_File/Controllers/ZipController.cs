using System;
using System.IO;
using Ionic.Zip;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

namespace testZip_File.Controllers
{
    public class ZipController : Controller
    {
        private readonly NasFileProvider _nasFileProvider;
        private readonly NasFileProviderNew _fileProviderNew;
        private readonly IWebHostEnvironment _environment;

        public ZipController(NasFileProvider nasFileProvider, NasFileProviderNew fileProviderNew, IWebHostEnvironment environment)
        {
            _nasFileProvider = nasFileProvider;
            _fileProviderNew = fileProviderNew;
            _environment = environment;
        }

        [Route("/zip")]
        public IActionResult Index()
        {
            var fileNmae = "Secret.zip";
            var rootPath = "/Users/cash/Downloads";
            var path = Path.Combine(rootPath, fileNmae);

            Zip(path);

            var physicalFileProvider = new PhysicalFileProvider(rootPath);
            var fileInfo = physicalFileProvider.GetFileInfo(fileNmae);
            using var steam = fileInfo.CreateReadStream();

            return File(steam, "application/zip", "cash_file.zip");
        }

        [Route("/zip2")]
        public IActionResult Index2()
        {
            var fileNmae = "Secret.zip";

            var path = Path.Combine(_nasFileProvider.RootPath, fileNmae);

            Zip(path);

            var fileInfo = _nasFileProvider.Provider.GetFileInfo(fileNmae);
            using var steam = fileInfo.CreateReadStream();

            return File(steam, "application/zip", "cash_file.zip");
        }

        [Route("/zip3")]
        public IActionResult Index3()
        {
            var fileNmae = "Secret.zip";

            var path = Path.Combine(_fileProviderNew.Root, fileNmae);

            Zip(path);

            var fileInfo = _nasFileProvider.Provider.GetFileInfo(fileNmae);
            using var steam = fileInfo.CreateReadStream();

            return File(steam, "application/zip", "cash_file.zip");
        }
        
        [Route("/zip4")]
        public IActionResult Index4()
        {
            var fileNmae = "Secret.zip";

            var path = Path.Combine(_environment.WebRootPath, fileNmae);

            Zip(path);

            return File(fileNmae, "application/zip", "cash_3w_file.zip");
        }

        private static void Zip(string path)
        {
            using var zip = new ZipFile();

            zip.Password = "123456";

            zip.AddEntry("index.html", $@"
<html><body>
    <h1>Hello World</h1>
    <h2>{DateTime.Now:yyyy-MM-dd HH:mm:ss}</h2>
</body>");

            zip.Save(path);
        }
    }
}