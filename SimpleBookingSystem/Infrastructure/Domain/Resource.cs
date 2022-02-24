using System;
using System.Collections.Generic;

namespace SimpleBookingSystem.Infrastructure.Domain
{
    public partial class Resource
    {
        public Resource()
        {
            Booking = new HashSet<Booking>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Quantity { get; set; }

        public virtual ICollection<Booking> Booking { get; set; }
    }
}
