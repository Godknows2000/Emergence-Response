using System;
using System.Collections.Generic;

namespace EmergenceResponse.Data;

public partial class Location
{
    public Guid Id { get; set; }

    public string Address { get; set; }

    public string City { get; set; }

    public double? Latitude { get; set; }

    public double? Longitude { get; set; }

    public virtual ICollection<Emergency> Emergencies { get; } = new List<Emergency>();

    public virtual ICollection<ServiceProvider> ServiceProviders { get; } = new List<ServiceProvider>();
}
