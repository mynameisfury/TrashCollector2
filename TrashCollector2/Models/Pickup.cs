using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrashCollector2.Models
{
    public class Pickup
    {
        [Key]
        public int ID { get; set; }
        public DateTime PickupDate { get; set; }

        public int CustomerID { get; set; }
        [ForeignKey("CustomerID")]
        public virtual Customer Customer { get; set; }

        public int? WorkerID { get; set; }
        [ForeignKey("WorkerID")]
        public virtual Worker Worker { get; set; }

        public int Charge { get; set; }
        public bool Complete { get; set; }
        public bool Suspended { get; set; }
        public DateTime? SuspensionDate { get; set; }
        public bool OneTime { get; set; }
    }
}