using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CounterApp.Models
{
    internal class AllCounters
    {
        public ObservableCollection<Counter> Counters { get; set; } = new();
        public string AddButtonText => "Add";

        public AllCounters() =>
            LoadCounters();

        public void LoadCounters()
        {
            Counters.Clear();

            string appDataPath = FileSystem.AppDataDirectory;

            IEnumerable<Counter> counters = Directory
                .EnumerateFiles(appDataPath, "*.counter.txt")
                .Select(filename => new Counter()
                {
                    Filename = filename,
                    Name = File.ReadAllLines(filename).FirstOrDefault() ?? "",
                    Value = int.TryParse(
                        File.ReadAllLines(filename).Skip(1).FirstOrDefault(),
                        out int value) ? value : 0,
                    Date = File.GetCreationTime(filename)
                })
                .OrderBy(counter => counter.Date);

            foreach (Counter counter in counters)
            {
                Counters.Add(counter);
            }
        }
    }
}
