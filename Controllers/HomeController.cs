using Intex2Backend.Data;
using Intex2Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Intex2Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HomeController : ControllerBase
{
    private readonly IBackendRepository _repo;

    public HomeController(IBackendRepository temp)
    {
        _repo = temp;
    }

    //Customer section
    //Returns all customer info
    [HttpGet]
    public IEnumerable<Customer> GetCustomers()
    {
        var customerData = _repo.Customers
            .ToArray();

        return customerData;
    }

    //Returns one customers info
    [HttpGet]
    public IEnumerable<Customer> GetCustomers(int id)
    {
        var customerData = _repo.Customers
            .Where(x => x.CustomerId == id).ToArray();
        return customerData;
    }
    //Add a user
    //[HttpPost]
    //public ActionResult<Customer> addUser(Customer customer)
    //{
    //    _repo.Customers.Add(customer);

    //}


    //Line Item section
    //Returns all line item info
    [HttpGet]
    public IEnumerable<LineItem> GetLineItems()
    {
        var lineItemData = _repo.LineItems
            .ToArray();

        return lineItemData;
    }
    //Returns one line item

    [HttpGet]
    public IEnumerable<LineItem> GetLineItems(int id)
    {
        var lineItemData = _repo.LineItems
            .Where(x => x.TransactionId == id).ToArray();

        return lineItemData;
    }


    //Order section
    //Returns all Orders
    [HttpGet]
    public IEnumerable<Order> GetOrders()
    {
        var orderData = _repo.Orders
            .ToArray();

        return orderData;
    }

    //Returns one order
    [HttpGet]
    public IEnumerable<Order> GetOrders(int id)
    {
        var orderData = _repo.Orders
            .Where(x => x.TransactionId == id).ToArray();

        return orderData;
    }

    //Product section
    //Returns all products
    [HttpGet]
    public IEnumerable<Product> GetProducts()
    {
        var productData = _repo.Products
            .ToArray();

        return productData;
    }

    //Returns one product
    [HttpGet]
    public IEnumerable<Product> GetProducts(int id)
    {
        var productData = _repo.Products
            .Where(x => x.ProductId == id).ToArray();

        return productData;
    }
}