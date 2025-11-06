using CLDV7112_PracticumGuide.Models; 
using Microsoft.EntityFrameworkCore.SqlServer; //import nuget package
using Microsoft.EntityFrameworkCore;



namespace CLDV7112_PracticumGuide.Data
{
    public class AppDbContext : DbContext //defines custom context of db , deriving methods from entity framework core
    {
     
            public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { } //constructor , basically saying which db to connect to 
            public DbSet<SensorReading> SensorReadings { get; set; } //list of objects, saying what you can query 
    }


    
}
