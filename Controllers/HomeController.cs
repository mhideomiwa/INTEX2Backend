using Intex2Backend.Data;
using Intex2Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Azure;
namespace Intex2Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private IBackendRepository _repo;
        public HomeController(IBackendRepository temp)
        {
            _repo = temp;
        }

        //
        //Customer section
        //
        //Returns all customer info
         [HttpGet("GetAllCustomers")]
         public IEnumerable<Customer> GetCustomers()
        {
            var customerData = _repo.Customers
                .ToArray();
            return customerData;
        }
        //Returns one customers info
        [HttpGet("GetOneCustomer")]
        public IEnumerable<Customer> GetCustomers(int id)
        {
            var customerData = _repo.Customers
                .Where(x => x.CustomerId == id).ToArray();
            return customerData;
        }
        // Edit a Customer
        [HttpPut("EditCustomer")]
        public IActionResult EditCustomer(int id, Customer customer)
        {
            if (id != customer.CustomerId)
            {
                return BadRequest();
            }
            var customerToUpdate = _repo.Customers.FirstOrDefault(c => c.CustomerId == id);
            if (customerToUpdate == null)
            {
                return NotFound();
            }
            // Update the properties of the Customer
            customerToUpdate.FirstName = customer.FirstName;
            customerToUpdate.LastName = customer.LastName;
            customerToUpdate.BirthDate = customer.BirthDate;
            customerToUpdate.CountryOfResidence = customer.CountryOfResidence;
            customerToUpdate.Gender = customer.Gender;
            customerToUpdate.Age = customer.Age;
            _repo.UpdateCustomer(customerToUpdate);
            return NoContent();
        }
        // Delete a Customer
        [HttpDelete("DeleteCustomer")]
        public IActionResult DeleteCustomer(int id)
        {
            var customerToUpdate = _repo.Customers.FirstOrDefault(c => c.CustomerId == id);
            if (customerToUpdate == null)
            {
                return NotFound();
            }
            _repo.DeleteCustomer(customerToUpdate);
            return NoContent();
        }
        // Create Customer
        [HttpPost("CreateCustomer")]
        public IActionResult CreateCustomer(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _repo.AddCustomer(customer);
                return CreatedAtAction(nameof(GetCustomers), new { id = customer.CustomerId }, customer);
            }
            return BadRequest(ModelState);
        }

        //
        //Line Item section
        //
        //Returns all line item info
        [HttpGet("GetAllLineItems")]
        public IEnumerable<LineItem> GetLineItems()
        {
            var lineItemData = _repo.LineItems
                .ToArray();
            return lineItemData;
        }
        //Returns one line item
        [HttpGet("GetOneLineItem")]
        public IEnumerable<LineItem> GetLineItems(int id)
        {
            var lineItemData = _repo.LineItems
                .Where(x => x.TransactionId == id).ToArray();
            return lineItemData;
        }
        // Edit a line item
        [HttpPut("EditLineItem")]
        public IActionResult EditLineItem(int id, LineItem lineItem)
        {
            if (id != lineItem.TransactionId)
            {
                return BadRequest();
            }
            var lineItemToUpdate = _repo.LineItems.FirstOrDefault(li => li.TransactionId == id);
            if (lineItemToUpdate == null)
            {
                return NotFound();
            }
            // Update the properties of the Line Item
            lineItemToUpdate.Qty = lineItem.Qty;
            lineItemToUpdate.Rating = lineItem.Rating;
            _repo.UpdateLineItem(lineItemToUpdate);
            return NoContent();
        }
        // Delete a Line Item
        [HttpDelete("DeleteLineItem")]
        public IActionResult DeleteLineItem(int id)
        {
            var lineItemToUpdate = _repo.LineItems.FirstOrDefault(li => li.TransactionId == id);
            if (lineItemToUpdate == null)
            {
                return NotFound();
            }
            _repo.DeleteLineItem(lineItemToUpdate);
            return NoContent();
        }
        // Create line item
        [HttpPost("CreateLineItem")]
        public IActionResult CreateLineItem(LineItem lineItem)
        {
            if (ModelState.IsValid)
            {
                _repo.AddLineItem(lineItem);
                return CreatedAtAction(nameof(GetLineItems), new { id = lineItem.TransactionId }, lineItem);
            }
            return BadRequest(ModelState);
        }

        //
        //Order section
        //
        //Returns all Orders
        [HttpGet("GetAllOrders")]
        public IEnumerable<Order> GetOrders()
        {
            var orderData = _repo.Orders
                .ToArray();
            return orderData;
        }
        //Returns one order
        [HttpGet("GetOneOrder")]
        public IEnumerable<Order> GetOrders(int id)
        {
            var orderData = _repo.Orders
                .Where(x => x.TransactionId == id).ToArray();
            return orderData;
        }
        // Edit a order
        [HttpPut("EditOrder")]
        public IActionResult EditOrder(int id, Order order)
        {
            if (id != order.TransactionId)
            {
                return BadRequest();
            }
            var orderToUpdate = _repo.Orders.FirstOrDefault(o => o.TransactionId == id);
            if (orderToUpdate == null)
            {
                return NotFound();
            }
            // Update the properties of the order
            orderToUpdate.Date = order.Date;
            orderToUpdate.DayOfWeek = order.DayOfWeek;
            orderToUpdate.Time = order.Time;
            orderToUpdate.EntryMode = order.EntryMode;
            orderToUpdate.Amount = order.Amount;
            orderToUpdate.TypeOfTransaction = order.TypeOfTransaction;
            orderToUpdate.CountryOfTransaction = order.CountryOfTransaction;
            orderToUpdate.ShippingAddress = order.ShippingAddress;
            orderToUpdate.Bank = order.Bank;
            orderToUpdate.TypeOfCard = order.TypeOfCard;
            orderToUpdate.Fraud = order.Fraud;
            _repo.UpdateOrder(orderToUpdate);
            return NoContent();
        }
        // Delete a Order
        [HttpDelete("DeleteOrder")]
        public IActionResult DeleteOrder(int id)
        {
            var orderToDelete = _repo.Orders.FirstOrDefault(o => o.TransactionId == id);
            if (orderToDelete == null)
            {
                return NotFound();
            }
            _repo.DeleteOrder(orderToDelete);
            return NoContent();
        }
        // Create Order
        [HttpPost("CreateOrder")]
        public IActionResult CreateOrder(Order order)
        {
            if (ModelState.IsValid)
            {
                _repo.AddOrder(order);
                return CreatedAtAction(nameof(GetOrders), new { id = order.TransactionId }, order);
            }
            return BadRequest(ModelState);
        }

        //
        //Product section
        //
        [HttpGet("GetAllProducts")]
        public IEnumerable<Product> GetProducts()
        {
            var productData = _repo.Products
                .ToArray();
            return productData;
        }
        //Returns one product
        [HttpGet("GetOneProduct")]
        public IEnumerable<Product> GetProducts(int id)
        {
            var productData = _repo.Products
                .Where(x => x.ProductId == id).ToArray();
            return productData;
        }
        // Edit a product
        [HttpPut("EditProduct")]
        public IActionResult EditProduct(int id, Product product)
        {
            if (id != product.ProductId)
            {
                return BadRequest();
            }
            var productToUpdate = _repo.Products.FirstOrDefault(p => p.ProductId == id);
            if (productToUpdate == null)
            {
                return NotFound();
            }
            // Update the properties of the product
            productToUpdate.Name = product.Name;
            productToUpdate.Year = product.Year;
            productToUpdate.NumParts = product.NumParts;
            productToUpdate.Price = product.Price;
            productToUpdate.ImgLink = product.ImgLink;
            productToUpdate.PrimaryColor = product.PrimaryColor;
            productToUpdate.SecondaryColor = product.SecondaryColor;
            productToUpdate.Description = product.Description;
            productToUpdate.Category = product.Category;
            _repo.UpdateProduct(productToUpdate);
            return NoContent();
        }
        // Delete a product
        [HttpDelete("DeleteProduct")]
        public IActionResult DeleteProduct(int id)
        {
            var productToDelete = _repo.Products.FirstOrDefault(p => p.ProductId == id);
            if (productToDelete == null)
            {
                return NotFound();
            }
            _repo.DeleteProduct(productToDelete);
            return NoContent();
        }
        // Create Product
        [HttpPost("CreateProduct")]
        public IActionResult CreateProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                _repo.AddProduct(product);
                return CreatedAtAction(nameof(GetProducts), new { id = product.ProductId }, product);
            }
            return BadRequest(ModelState);
        }
    }
}