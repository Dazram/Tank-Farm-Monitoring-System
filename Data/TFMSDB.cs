using Microsoft.EntityFrameworkCore;
using Capstone.Models;

namespace TFMS.Data;

public class TFMSDBContext: DbContext
{
   public TFMSDBContext(DbContextOptions<TFMSDBContext> options) : base(options)
    {

    }
    public DbSet<Loading> Loading { get; set; }
    public DbSet<Offloading> Offloading { get; set; }
    public DbSet<StorageTank> StorageTank { get; set; }
    public DbSet<User> User { get; set; }
   
}
