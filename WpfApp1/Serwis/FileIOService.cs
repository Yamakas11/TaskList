using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.IO;
using WpfApp1.Models;

namespace WpfApp1.Serwis
{
    internal class FileIOService
    {
        private readonly string PATH;

        public FileIOService(string path)
        {
            PATH = path;
        }

        public BindingList<ToDoModel> ToDoList()
        {
            var fileExists = File.Exists(PATH);
            if (!fileExists)
            {
                File.CreateText(PATH).Dispose();
                return new BindingList<ToDoModel>();
            }
            using (var reader = File.OpenText(PATH))
            {
                var fileText = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<BindingList<ToDoModel>>(fileText);
            }
        }

        public void SaveData(Object toDoModels)
        {
            using (StreamWriter write = File.CreateText(PATH))
            {
                string output = JsonConvert.SerializeObject(toDoModels);
                write.WriteLine(output);
            }
        }
    }
}
