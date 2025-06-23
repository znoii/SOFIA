using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportSofia.Models
{
    public enum InventoryState
    {
        В_наличии,
        Выдана,
        На_обслуживании
    }

    public class Inventory
    {
        public int Id { get; set; }
        public string InventoryNumber { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Type { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateOnly PublicationDate { get; set; }
        public InventoryState State { get; set; }
        public User? Reader { get; set; }
        public int? ReaderId { get; set; }
    }
}