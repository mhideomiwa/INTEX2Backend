using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Intex2Backend.Models;

public partial class LineItem
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int TransactionId { get; set; }

    public byte ProductId { get; set; }

    public byte Qty { get; set; }

    public byte Rating { get; set; }
}
