using System;
using System.Collections.Generic;

namespace Intex2Backend.Models;

public partial class ContentFiltering
{
    public byte Index { get; set; }

    public byte IfYouLiked { get; set; }

    public byte Recommendation1 { get; set; }

    public byte Recommendation2 { get; set; }

    public byte Recommendation3 { get; set; }

    public byte Recommendation4 { get; set; }

    public byte Recommendation5 { get; set; }
}
