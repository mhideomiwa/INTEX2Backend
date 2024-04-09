using Intex2Backend.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Intex2Backend.Data
{
    public class EFBackendRepository : IBackendRepository
    {
        private CPOLContext _context;

        public EFBackendRepository(CPOLContext temp)
        {
            _context = temp;
        }

        public IEnumerable<Customer> Customers 
        { 
            get => _context.Customers;
            set => throw new NotImplementedException(); // Implement setter if needed
        }

        public IEnumerable<LineItem> LineItems
        {
            get=> _context.LineItems;
            set => throw new NotImplementedException(); // Implement setter if needed
        }

        public IEnumerable<Order> Orders 
        {
            get => _context.Orders;
            set => throw new NotImplementedException(); // Implement setter if needed
        }

        public IEnumerable<Product> Products 
        {
            get => _context.Products;
            set => throw new NotImplementedException(); // Implement setter if needed
        }
        
        public void UpdateProduct(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteProduct(Product product)
        {
            _context.Products.Remove(product);
            _context.SaveChanges();
        }

        public void AddProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void UpdateOrder(Order order)
        {
            _context.Entry(order).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteOrder(Order order)
        {
            _context.Orders.Remove(order);
            _context.SaveChanges();
        }

        public void AddOrder(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        public void UpdateLineItem(LineItem lineItem)
        {
            _context.Entry(lineItem).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteLineItem(LineItem lineItem)
        {
            _context.LineItems.Remove(lineItem);
            _context.SaveChanges();
        }

        public void AddLineItem(LineItem lineItem)
        {
            _context.LineItems.Add(lineItem);
            _context.SaveChanges();
        }

        public void UpdateCustomer(Customer customer)
        {
            _context.Entry(customer).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteCustomer(Customer customer)
        {
            _context.Customers.Remove(customer);
            _context.SaveChanges();
        }

        public void AddCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
        }
    }
}
