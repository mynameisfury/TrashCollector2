using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrashCollector2.Models
{
    public class Customer
    {
        [Key]
        public int ID { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public int? Balance { get; set; }

        public int? AddressID { get; set; }
        [ForeignKey("AddressID")]
        public virtual Address Address { get; set; }

        public string UserID { get; set; }

    }
}