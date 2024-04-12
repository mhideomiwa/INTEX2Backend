using System;
using System.Collections.Generic;

namespace Intex2Backend.Models;

public partial class UserCollab
{
    public short UserId { get; set; }

    public byte TopRatedProduct { get; set; }

    public byte Recommendation1 { get; set; }

    public byte Recommendation2 { get; set; }

    public byte Recommendation3 { get; set; }

    public byte Recommendation4 { get; set; }

    public byte Recommendation5 { get; set; }
}
