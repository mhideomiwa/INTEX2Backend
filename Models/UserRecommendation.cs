using System;
using System.Collections.Generic;

namespace Intex2Backend.Models;

public partial class UserRecommendation
{
    public short UserId { get; set; }

    public string TopRatedProduct { get; set; } = null!;

    public string Recommendation1 { get; set; } = null!;

    public string Recommendation2 { get; set; } = null!;

    public string Recommendation3 { get; set; } = null!;

    public string Recommendation4 { get; set; } = null!;

    public string Recommendation5 { get; set; } = null!;
}
