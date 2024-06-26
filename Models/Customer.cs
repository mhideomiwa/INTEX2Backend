﻿using System;
using System.Collections.Generic;

namespace Intex2Backend.Models;

public partial class Customer
{
    public short CustomerId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateOnly? BirthDate { get; set; }

    public string? CountryOfResidence { get; set; }

    public string? Gender { get; set; }

    public double? Age { get; set; }

    public string? Email { get; set; }
}
