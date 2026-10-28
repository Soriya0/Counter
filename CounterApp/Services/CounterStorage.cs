using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CounterApp.Models;
using System.Text.Json;

namespace CounterApp.Services
{
    class CounterStorage
    {
        private static readonly string filePath =
            Path.Combine(
                FileSystem.AppDataDirectory,
                "counters.json");

        public static async Task SaveAsync(
            IEnumerable<Counter> counters)
        {
            string json = JsonSerializer.Serialize(counters);

            await File.WriteAllTextAsync(filePath, json);
        }

        public static async Task<List<Counter>> LoadAsync()
        {
            if(!File.Exists(filePath))
            {
                return new List<Counter>();
            }

            string json = await File.ReadAllTextAsync(filePath);

            return JsonSerializer.Deserialize<List<Counter>>(json)
                ?? new List<Counter>();
        }
    }
}
