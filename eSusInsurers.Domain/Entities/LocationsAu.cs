﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace eSusInsurers.Domain.Entities;

public partial class LocationsAu : BaseAuditableEntity
{
    public DateTime HistoryCreatedDate { get; set; }

    public int LocationId { get; set; }

    public string LocationName { get; set; }

    public int RegionId { get; set; }

    public int DistrictId { get; set; }

    public int? SubCountyId { get; set; }

    public string Latitude { get; set; }

    public string Longitude { get; set; }

    public bool? IsActive { get; set; }

    public virtual District District { get; set; }

    public virtual Region Region { get; set; }

    public virtual SubCounty SubCounty { get; set; }
}