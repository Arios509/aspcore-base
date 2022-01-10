using Domain;
using Domain.Aggregate.SmsMessage;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity.Infrastructure;

namespace Api.Infrastructure.Repositories
{
    public class DataContext : DbContext
    {
        public DbSet<SmsMessage> SmsMessage { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }
    }
}
