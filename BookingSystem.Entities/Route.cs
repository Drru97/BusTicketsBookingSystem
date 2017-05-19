using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingSystem.Entities
{
    [Table("Route")]
    public class Route
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Route()
        {
            Journey = new HashSet<Journey>();
        }

        public int Id { get; set; }

        public int DeparturePointId { get; set; }

        public int ArrivalPointId { get; set; }

        public double? Length { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Journey> Journey { get; set; }

        public virtual RoutePoint RoutePoint { get; set; }

        public virtual RoutePoint RoutePoint1 { get; set; }
    }
}
