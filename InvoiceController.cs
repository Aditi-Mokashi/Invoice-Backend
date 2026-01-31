
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using Dapper;
using System.Data;
using System.Linq;

namespace BuggyApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoiceController : ControllerBase
    {
        public List<Item> getData()
        {
            string sql = "SELECT CustomerName, Name as ItemName, Price as ItemPrice FROM Invoices i JOIN InvoiceItems ii ON i.InvoiceID = ii.InvoiceID;";
            string dbPath = "Data Source=init.sql";
            string connectionString = $"Data Source={dbPath}";

            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                var invoices = connection.Query<Item>(sql).ToList();
                return invoices;
            }
        }
        [HttpGet]
        public IActionResult GetInvoice()
        {
            List<Item> items = getData();
            if (items.Count == 0) // NullReferenceException
            {
                return Ok(new { items });
            }
            // return NotFound("No invoice found");
            return Ok("Ok");
        }

        public class Item
        {
            public int CustomerName { get; set; }
            public string ItemName { get; set; }
            public double ItemPrice { get; set; }

        }
    }
}
