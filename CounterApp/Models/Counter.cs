using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CounterApp.Models
{
    public class Counter
    {
        public string Filename { get; set; } = "";
        public string Name { get; set; } = "";
        public int Value { get; set; } = 0;
        public DateTime Date { get; set; }

        public string NamePlaceholder => "Enter counter name";
        public string InitialValuePlaceholder => "Enter initial value";
        public string SaveButtonText => "Save";
    }
}
