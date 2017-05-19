using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingSystem.Entities
{
    [Table("Bus")]
    public class Bus
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Bus()
        {
            Journey = new HashSet<Journey>();
        }

        public int Id { get; set; }

        [StringLength(20)]
        public string Vin { get; set; }

        [StringLength(100)]
        public string Model { get; set; }

        [StringLength(10)]
        public string AutomobileNumber { get; set; }

        public int? PassengersCount { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Journey> Journey { get; set; }
    }
}
