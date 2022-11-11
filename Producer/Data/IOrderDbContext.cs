using Microsoft.EntityFrameworkCore;
using Producer.Model;
using RabbitMQService.Model;
using System.Collections.Generic;

namespace Producer.Data
{
    public interface IOrderDbContext
    {
        DbSet<Order> Order { get; set; }
        DbSet<User> Users { get; set; }
        Task<int> SaveChangesAsync();
    }
}
