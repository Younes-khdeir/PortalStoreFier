using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PortalStoreFier.Models;

namespace PortalStoreFier.Data
{
    public class PortalContext : IdentityDbContext<ApplicationUser>
    {
        public PortalContext(DbContextOptions<PortalContext> options) : base(options)
        {

        }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<CustomerActivity>? CustomerActivitys { get; set; }
        public DbSet<RelationManagement>? RelationManagements { get; set; }
        public DbSet<Classification>? Classifications { get; set; }

        public DbSet<PricePack>? PricePacks { get; set; }
        public DbSet<NewPricePack>? NewPricePacks { get; set; }
    }
}

