using System;
using System.Collections.Generic;

namespace Intex2Backend.Models;

public partial class ContentFilteringRecommendation
{
    public byte Index { get; set; }

    public string IfYouLiked { get; set; } = null!;

    public string Recommendation1 { get; set; } = null!;

    public string Recommendation2 { get; set; } = null!;

    public string Recommendation3 { get; set; } = null!;

    public string Recommendation4 { get; set; } = null!;

    public string Recommendation5 { get; set; } = null!;
}
