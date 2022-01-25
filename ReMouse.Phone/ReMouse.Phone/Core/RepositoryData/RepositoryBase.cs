using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace ReMouse.Phone.Core.RepositoryData
{
    public abstract class RepositoryBase<T> where T : class, new()
    {
        private const string RepositoryFolder = "ReMouseData";

        private static readonly JsonSerializerSettings _settings = new JsonSerializerSettings()
        {
            Formatting = Formatting.Indented
        };

        private readonly string _path;
        private readonly List<T> items;

        public RepositoryBase(string repositoryName)
        {
            _path = RepositoryPath(repositoryName);

            if (!File.Exists(_path))
            {
                Directory.CreateDirectory(DirectoryPath());
                File.Create(_path);
                items = new List<T>();
            }
            else
            {
                string json = File.ReadAllText(_path);
                items = JsonConvert.DeserializeObject<List<T>>(json) ?? new List<T>();
            }
        }

        private string DirectoryPath()
        {
            string basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            return Path.Combine(basePath, RepositoryFolder);
        }

        private string RepositoryPath(string repositoryName)
        {
            return Path.Combine(DirectoryPath(), repositoryName) + ".json";
        }

        public List<T> GetAll()
        {
            return items;
        }

        public void Save()
        {
            string json = JsonConvert.SerializeObject(items, _settings);
            File.WriteAllText(_path, json);
        }

        public abstract void Reset();
    }
}
