using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingSystem.Entities
{
    [Table("Ticket")]
    public class Ticket
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Ticket()
        {
            Traffic = new HashSet<Traffic>();
        }

        public int Id { get; set; }

        public int JourneyId { get; set; }

        public int PassengerId { get; set; }

        [Column(TypeName = "money")]
        public decimal? Price { get; set; }

        public int? Seat { get; set; }

        public DateTime PurchaseDateTime { get; set; }

        public virtual Journey Journey { get; set; }

        public virtual Passenger Passenger { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Traffic> Traffic { get; set; }
    }
}
