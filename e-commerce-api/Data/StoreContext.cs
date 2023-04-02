using e_commerce_api.Entities;
using Microsoft.EntityFrameworkCore;

namespace e_commerce_api.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
    }    

}
