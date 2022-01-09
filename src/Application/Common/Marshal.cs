using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using Domain.Models;

namespace Application.Common
{
    public class Marshal
    {
        private string GetDataStorage(string file)
        {
            string directory = ".datastorage";
            string storage = Directory.GetCurrentDirectory();
            storage = Path.GetFullPath(Path.Combine(storage, @"..\..\"));

            return Path.GetFullPath(Path.Combine(storage, directory, file));
        }

        private string Read(string file)
            => File.ReadAllText(GetDataStorage(file));

        public User[] FetchUsers(string file)
        {
            string json = Read(file);
            if(json is null)
                return null!;
            else
                return JsonSerializer.Deserialize<User[]>(json)!;
        }

    }
}