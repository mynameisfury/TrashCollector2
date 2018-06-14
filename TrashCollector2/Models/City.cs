using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrashCollector2.Models
{
    public class City
    {
        [Key]
        public int ID { get; set; }
        public string CityName { get; set; }

        public int? StateID { get; set; }
        [ForeignKey("StateID")]
        public virtual State State { get; set; }
    }
}