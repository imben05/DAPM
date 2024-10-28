﻿//------------------------------------------------------------------------------
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
    using System.ComponentModel;

    public partial class Customer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Customer()
        {
            this.Bookings = new HashSet<Booking>();
        }    
        public int customerID { get; set; }
        [DisplayName("ID")]
        public Nullable<int> accountID { get; set; }
        [DisplayName("Tên")]
        public string firstName { get; set; }
        [DisplayName("Họ")]
        public string lastName { get; set; }
        [DisplayName("Ngày sinh")]
        public Nullable<System.DateTime> dateOfBirth { get; set; }
        [DisplayName("Địa chỉ")]
        public string address { get; set; }
        [DisplayName("Email")]
        public string email { get; set; }
        [DisplayName("SĐT")]
        public string phone { get; set; }
    
        public virtual Account Account { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
