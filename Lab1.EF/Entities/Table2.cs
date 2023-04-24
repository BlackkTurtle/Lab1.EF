using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.EF.Entities
{
    public class Table2
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string Name { get; set; }
        [ForeignKey("Table1")]
        public int Table1Id { get; set; }
        public Table1 Table1 { get; set; }
    }
}
