using Intex2Backend.Data;
using Intex2Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using Microsoft.ML.OnnxRuntime;
using System;
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
        // Filter Customers
        [HttpGet("FilterCustomers")]
        public IEnumerable<Customer> FilterCustomers(
                string? firstName = null,
                int? age = null,
                string? lastName = null,
                string? birthDate = null,
                string? countryOfResidence = null,
                string? gender = null)
        {
            var customers = _repo.Customers.AsQueryable();

            if (!string.IsNullOrEmpty(firstName))
                customers = customers.Where(p => EF.Functions.Like(p.FirstName, $"%{firstName}%"));

            if (age.HasValue)
                customers = customers.Where(p => p.Age == age);

            if (!string.IsNullOrEmpty(lastName))
                customers = customers.Where(p => p.LastName == lastName);

            if (!string.IsNullOrEmpty(birthDate))
            {
                DateOnly parsedBirthDate;
                if (DateOnly.TryParse(birthDate, out parsedBirthDate))
                {
                    customers = customers.Where(p => p.BirthDate == parsedBirthDate);
                }
            }

            if (!string.IsNullOrEmpty(countryOfResidence))
                customers = customers.Where(p => EF.Functions.Like(p.CountryOfResidence, $"%{countryOfResidence}%"));

            if (!string.IsNullOrEmpty(gender))
                customers = customers.Where(p => EF.Functions.Like(p.Gender, $"%{gender}%"));

            return customers.ToArray();
        }
        // Search Customers
        [HttpGet("SearchCustomers")]
        public IEnumerable<Customer> FilterCustomers(string searchTerm)
        {
            var customers = _repo.Customers.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                customers = customers.Where(p =>
                    EF.Functions.Like(p.FirstName, $"%{searchTerm}%") ||
                    EF.Functions.Like(p.LastName, $"%{searchTerm}%") ||
                    EF.Functions.Like(p.CountryOfResidence, $"%{searchTerm}%") ||
                    EF.Functions.Like(p.Gender, $"%{searchTerm}%") ||
                    (p.Age != null && p.Age.ToString().Contains(searchTerm)) ||
                    EF.Functions.Like(p.BirthDate.Value.ToString("yyyy-MM-dd"), $"%{searchTerm}%") // Convert BirthDate to string using a specific format
                );
            }

            return customers.ToArray();
        }

        //
        //Line Item section \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
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
        public IEnumerable<LineItem> GetLineItems(int transactionId, int productId)
        {
            var lineItemData = _repo.LineItems
                .Where(x => x.TransactionId == transactionId && x.ProductId == productId)
                .ToArray();

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
                return CreatedAtAction(nameof(GetLineItems), new { transactionId = lineItem.TransactionId, productId = lineItem.ProductId }, lineItem);
            }
            return BadRequest(ModelState);
        }
        // Filter line items
        [HttpGet("FilterLineItems")]
        public IEnumerable<LineItem> FilterLineItems(
                int? qty = null,
                int? rating = null)
        {
            var lineItems = _repo.LineItems.AsQueryable();

            if (qty.HasValue)
                lineItems = lineItems.Where(li => li.Qty == qty);

            if (rating.HasValue)
                lineItems = lineItems.Where(li => li.Rating == rating);

            return lineItems.ToArray();
        }
        // Search line items
        [HttpGet("SearchLineItems")]
        public IEnumerable<LineItem> FilterLineItems(string searchTerm)
        {
            var lineItems = _repo.LineItems.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                lineItems = lineItems.Where(p =>
                    (p.Qty != null && p.Qty.ToString().Contains(searchTerm)) ||
                    (p.Rating != null && p.Rating.ToString().Contains(searchTerm))
                );
            }

            return lineItems.ToArray();
        }

        //
        //Order section /////////////////////////////////////////
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
        // Filter Orders
        [HttpGet("FilterOrders")]
        public IEnumerable<Order> FilterOrders(
                string? date = null,
                string? dayOfWeek = null,
                int? time = null,
                string? entryMode = null,
                int? amount = null,
                int? fraud = null,
                string? typeOfTransaction = null,
                string? countryOfTransaction = null,
                string? shippingAddress = null,
                string? bank = null,
                string? typeOfCard = null)
        {
            var orders = _repo.Orders.AsQueryable();

            if (!string.IsNullOrEmpty(date))
            {
                DateOnly parsedDate;
                if (DateOnly.TryParse(date, out parsedDate))
                {
                    orders = orders.Where(p => p.Date == parsedDate);
                }
                else
                {
                    // Handle invalid birthDate format
                }
            }

            if (!string.IsNullOrEmpty(dayOfWeek))
                orders = orders.Where(p => EF.Functions.Like(p.DayOfWeek, $"%{dayOfWeek}%"));

            if (time.HasValue)
                orders = orders.Where(p => p.Time == time);

            if (!string.IsNullOrEmpty(entryMode))
                orders = orders.Where(p => EF.Functions.Like(p.EntryMode, $"%{entryMode}%"));

            if (amount.HasValue)
                orders = orders.Where(p => p.Amount == amount);

            if (fraud.HasValue)
                orders = orders.Where(p => p.Fraud == fraud);

            if (!string.IsNullOrEmpty(typeOfTransaction))
                orders = orders.Where(p => p.TypeOfTransaction == typeOfTransaction);

            if (!string.IsNullOrEmpty(countryOfTransaction))
                orders = orders.Where(p => p.CountryOfTransaction == countryOfTransaction);

            if (!string.IsNullOrEmpty(shippingAddress))
                orders = orders.Where(p => EF.Functions.Like(p.ShippingAddress, $"%{shippingAddress}%"));

            if (!string.IsNullOrEmpty(bank))
                orders = orders.Where(p => EF.Functions.Like(p.Bank, $"%{bank}%"));

            if (!string.IsNullOrEmpty(typeOfCard))
                orders = orders.Where(p => EF.Functions.Like(p.TypeOfCard, $"%{typeOfCard}%"));

            return orders.ToArray();
        }
        // Search Orders
        [HttpGet("SearchOrders")]
        public IEnumerable<Order> FilterOrders(string searchTerm)
        {
            var orders = _repo.Orders.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                // Convert the searchTerm to a DateOnly object if possible
                DateOnly searchDate;
                bool isValidSearchDate = DateOnly.TryParse(searchTerm, out searchDate);

                // Filter orders based on searchTerm
                orders = orders.Where(o =>
                    EF.Functions.Like(o.DayOfWeek, $"%{searchTerm}%") ||
                    EF.Functions.Like(o.EntryMode, $"%{searchTerm}%") ||
                    EF.Functions.Like(o.TypeOfTransaction, $"%{searchTerm}%") ||
                    EF.Functions.Like(o.CountryOfTransaction, $"%{searchTerm}%") ||
                    EF.Functions.Like(o.ShippingAddress, $"%{searchTerm}%") ||
                    EF.Functions.Like(o.Bank, $"%{searchTerm}%") ||
                    (o.Time != null && o.Time.ToString().Contains(searchTerm)) ||
                    (o.Amount != null && o.Amount.ToString().Contains(searchTerm)) ||
                    (o.Fraud != null && o.Fraud.ToString().Contains(searchTerm))
                );
            }

            return orders.ToArray();
        }

        //
        //Product section ///////////////////////////////////////////
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
        [Authorize]
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
        // Filter Products
        [HttpGet("FilterProducts")]
        public IEnumerable<Product> FilterProducts(
                string? name = null,
                int? year = null,
                int? numParts = null,
                int? price = null,
                string? primaryColor = null,
                string? secondaryColor = null,
                string? description = null,
                string? category = null)
        {
            var products = _repo.Products.AsQueryable();

            if (!string.IsNullOrEmpty(name))
                products = products.Where(p => EF.Functions.Like(p.Name, $"%{name}%"));

            if (year.HasValue)
                products = products.Where(p => p.Year == year);

            if (numParts.HasValue)
                products = products.Where(p => p.NumParts == numParts);

            if (price.HasValue)
                products = products.Where(p => p.Price == price);

            if (!string.IsNullOrEmpty(primaryColor))
                products = products.Where(p => p.PrimaryColor == primaryColor);

            if (!string.IsNullOrEmpty(secondaryColor))
                products = products.Where(p => p.SecondaryColor == secondaryColor);

            if (!string.IsNullOrEmpty(description))
                products = products.Where(p => EF.Functions.Like(p.Description, $"%{description}%"));

            if (!string.IsNullOrEmpty(category))
                products = products.Where(p => EF.Functions.Like(p.Category, $"%{category}%"));

            return products.ToArray();
        }
        // Search Products
        [HttpGet("SearchProducts")]
        public IEnumerable<Product> FilterProducts(string searchTerm)
        {
            var products = _repo.Products.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                products = products.Where(p =>
                    EF.Functions.Like(p.Name, $"%{searchTerm}%") ||
                    EF.Functions.Like(p.Description, $"%{searchTerm}%") ||
                    EF.Functions.Like(p.Category, $"%{searchTerm}%") ||
                    (p.Year != null && p.Year.ToString().Contains(searchTerm)) ||
                    (p.NumParts != null && p.NumParts.ToString().Contains(searchTerm)) ||
                    (p.Price != null && p.Price.ToString().Contains(searchTerm)) ||
                    (p.PrimaryColor != null && p.PrimaryColor.Contains(searchTerm)) ||
                    (p.SecondaryColor != null && p.SecondaryColor.Contains(searchTerm))
                );
            }

            return products.ToArray();
        }

        //
        // Get Machine Learning Info
        //
        // Returns content filtering recommendations for a specific ID
        [HttpGet("GetOneContentFiltering")]
        public IActionResult GetContentFilterings(int id)
        {
            var recommendations = _repo.ContentFilterings.FirstOrDefault(r => r.IfYouLiked == id);
            if (recommendations == null)
            {
                return NotFound();
            }
            return Ok(recommendations);
        }

        [HttpGet("GetOneProductCollab")]
        public IActionResult GetProductCollab(int id)
        {
            var recommendations = _repo.ProductCollabs.FirstOrDefault(r => r.IfYouLiked == id);
            if (recommendations == null)
            {
                return NotFound();
            }
            return Ok(recommendations);
        }

        [HttpGet("GetOneUserCollab")]
        public IActionResult GetUserCollab(short userId)
        {
            var recommendations = _repo.UserCollabs.FirstOrDefault(r => r.UserId == userId);
            if (recommendations == null)
            {
                return NotFound();
            }
            return Ok(recommendations);
        }

            //
    //Onnx File Implementation
    //
    [HttpPost("detect-fraud")]
    public IActionResult DetectFraud([FromBody] FraudDetectionInput input)
    {
        // Load the ONNX model (you can do this once and keep it in memory)
        var modelPath = Path.Combine(Directory.GetCurrentDirectory(), "Onnx", "decision_tree_model.onnx");
        var session = new InferenceSession(modelPath);

        // Get the input name from the model's metadata
        var inputName = session.InputMetadata.Keys.First();

        // Prepare the input data
        var inputTensor = OnnxModelHelper.InputToTensor(input);
        var inputData = new List<NamedOnnxValue>
{
    NamedOnnxValue.CreateFromTensor(inputName, inputTensor)
};

        // Run the inference
        using (var outputData = session.Run(inputData))
        {
            if (outputData.Any())
            {
                // Get the output name from the model's metadata
                var outputName = session.OutputMetadata.Keys.First();

                // Find the output with the matching name
                var outputTensor = outputData.FirstOrDefault(x => x.Name == outputName);

                if (outputTensor != null)
                {
                    // Extract the fraud detection result
                    var fraudLabel = outputTensor.AsTensor<long>().ToArray()[0];

                    // Determine if fraud is detected based on the label
                    var fraudDetected = fraudLabel == 1;

                    // Return the result as the API response
                    return Ok(new { FraudDetected = fraudDetected, FraudLabel = fraudLabel });
                }
                else
                {
                    // Handle the case when the output with the expected name is not found
                    return BadRequest("Output not found");
                }
            }
            else
            {
                // Handle the case when outputData is empty
                return BadRequest("Invalid model output");
            }
        }
    }
    }
}
