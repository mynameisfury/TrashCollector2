using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrashCollector2.Models
{
    public class Worker
    {
        [Key]
        public int ID { get; set; }
        public string Username { get; set; }
        public int? AddressID { get; set; }
        [ForeignKey("AddressID")]
        public virtual Address Address { get; set; }

        public string UserID { get; set; }
    }
}