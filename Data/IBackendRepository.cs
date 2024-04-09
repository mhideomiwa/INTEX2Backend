using Intex2Backend.Models;

namespace Intex2Backend.Data;

public interface IBackendRepository
{
    IEnumerable<Customer> Customers { get; set; }
    IEnumerable<LineItem> LineItems { get; set; }
    IEnumerable<Order> Orders { get; set; }
    IEnumerable<Product> Products { get; set; }
}