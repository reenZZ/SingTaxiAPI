using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InterfaceSingTaxitoSQL.Model
{
    [Table("TaxiHistory")]
    public class TaxiHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string lat { get; set; }
        public string lon { get; set; }
        public string suburb { get; set; }
        public string status { get; set; }
        public DateTime last_updated_time  { get; set; }

    }
}
