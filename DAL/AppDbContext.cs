using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        public Product Get_Product(int Product_Id)
        {
            Product product = new Product();
            using(var command = Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "Get_Product";
                command.CommandType = CommandType.StoredProcedure;
                SqlParameter param = new SqlParameter("Product_Id", Product_Id);
                command.Parameters.Add(param);
                Database.OpenConnection();
                using(var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        product.ProductId = reader.GetInt32("ProductId");
                        product.Name = reader.GetString("Name");
                        product.Description = reader.GetString("Description");
                        product.UnitPrice = reader.GetDecimal("UnitPrice");
                        product.CategoryId = reader.GetInt32("CategoryId");
                    }
                }
            }
            return product;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"data source=192.168.1.7; initial catalog=EFCore6AM;persist security info=True;user id=sa;password=nrhm123;");
        }
    }

}
