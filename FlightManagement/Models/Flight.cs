//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FlightManagement.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Flight
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Flight()
        {
            this.Tickets = new HashSet<Ticket>();
        }
    
        public int flightID { get; set; }
        public string departureCity { get; set; }
        public string arrivalCity { get; set; }
        public System.DateTime departureTime { get; set; }
        public System.DateTime arrivalTime { get; set; }
        public System.TimeSpan flightDuration { get; set; }
        public decimal flightPrice { get; set; }
        public Nullable<int> aircraftID { get; set; }
        public Nullable<int> airlineID { get; set; }
    
        public virtual Aircraft Aircraft { get; set; }
        public virtual Airline Airline { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
