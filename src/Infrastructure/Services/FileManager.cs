using System.IO;
using  Microsoft.AspNetCore.Components.Forms;

namespace Infrastructure.Services
{
    public class FileManager
    {
        // to do :
        // check if exist or add GUID to file name

        public string GetDataStoragePath(string subfolder)
        {
            string directory = ".datastorage";
            string storage = Directory.GetCurrentDirectory();
            storage = Path.GetFullPath(Path.Combine(storage, @"..\..\..\"));

            return Path.GetFullPath(Path.Combine(storage, directory, subfolder));
        }

        public string GetFilePath(IBrowserFile file, string subfolder)
        {
            string directoryPath = GetDataStoragePath(subfolder);
            return Path.GetFullPath(Path.Combine(directoryPath, file.Name));
        }

        public async Task UploadAsync(IBrowserFile file, string subfolder)
        {
            string filePath = GetFilePath(file, subfolder);
            Stream stream = file.OpenReadStream();
            FileStream fileStream = File.Create(filePath);
            // System.Console.WriteLine($"PATH: {filePath}");
            await stream.CopyToAsync(fileStream);

            stream.Close();
            fileStream.Close();
        }
    }
}