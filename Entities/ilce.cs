using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ilce
    {
        [Key]
        public int Id { get; set; }
        public string Ad { get; set; }
        public int ilId { get; set; }
    }
}
