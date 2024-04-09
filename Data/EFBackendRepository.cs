using Intex2Backend.Models;

namespace Intex2Backend.Data
{
    public class EFBackendRepository : IBackendRepository
    {
        private CPOLContext _context;
        public EFBackendRepository(CPOLContext temp)
        {
            _context = temp;
        }

        public IEnumerable<Customer> Customers => _context.Customers;
        public IEnumerable<LineItem> LineItem => _context.LineItems;
        public IEnumerable<Order> Orders => _context.Orders;
        public IEnumerable<Product> Products => _context.Products;

        public IEnumerable<LineItem> LineItems { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        IEnumerable<Customer> IBackendRepository.Customers { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        IEnumerable<Order> IBackendRepository.Orders { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        IEnumerable<Product> IBackendRepository.Products { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
