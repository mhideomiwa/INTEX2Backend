using Intex2Backend.Models;
namespace Intex2Backend.Data
{
    public interface IBackendRepository
    {
        IEnumerable<Customer> Customers { get; set; }
        IEnumerable<LineItem> LineItems { get; set; }
        IEnumerable<Order> Orders { get; set; }
        IEnumerable<Product> Products { get; set; }
        void UpdateProduct(Product product) { }
        void DeleteProduct(Product product) { }
        void AddProduct(Product product) { }
        void UpdateOrder(Order order) { }
        void DeleteOrder(Order order) { }
        void AddOrder(Order order) { }
        void UpdateLineItem(LineItem lineItem) { }
        void DeleteLineItem(LineItem lineItem) { }
        void AddLineItem(LineItem lineItem) { }
        void UpdateCustomer(Customer customer) { }
        void DeleteCustomer(Customer customer) { }
        void AddCustomer(Customer customer) { }
        
    }
}