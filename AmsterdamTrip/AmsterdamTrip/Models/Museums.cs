using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AmsterdamTrip.Models
{
    [Table("Museums")]
    public class Museums
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(100), Unique]
        public string Name { get; set; }

        public byte[] Image { get; set; }

        [MaxLength(100)]
        public string Address { get; set; }

        [MaxLength(100)]
        public string Hourly { get; set; }

        public int Expectation { get; set; }

        public int IsChecked { get; set; }

        public string Date { get; set; }
    }
}
