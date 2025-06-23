using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportSofia.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string AccessRights { get; set; } = null!;
        public List<User> Users { get; set; } = null!;
    }
}
