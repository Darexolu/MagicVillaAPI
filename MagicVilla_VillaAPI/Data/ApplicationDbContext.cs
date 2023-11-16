using MagicVilla_VillaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_VillaAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) 
        {
                
        }
        public DbSet<Villa> Villas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Villa>().HasData(
                new Villa()
                {
                    Id = 1,
                    Name = "Royal Villa",
                    Details = "lorem ipsum ie kafsj  jssv ",
                    ImageUrl= "https://dotnetmasteryimages.blob.core.windows.net/bluevillaimages/villa1.jpg",
                    Occupancy= 5,
                    Rate = 200,
                    Sqft= 550,
                    Amenity= "",
                    CreatedDate = DateTime.Now

                },
                 new Villa()
                 {
                     Id = 2,
                     Name = "Diamond Villa",
                     Details = "lorem ipsum ie kafsj  jssv ",
                     ImageUrl = "https://dotnetmasteryimages.blob.core.windows.net/bluevillaimages/villa2.jpg",
                     Occupancy = 10,
                     Rate = 230,
                     Sqft = 650,
                     Amenity = "",
                     CreatedDate = DateTime.Now

                 },
                  new Villa()
                  {
                      Id = 3,
                      Name = "Lucky Pool Villa",
                      Details = "lorem ipsum ie kafsj  jssv ",
                      ImageUrl = "https://dotnetmasteryimages.blob.core.windows.net/bluevillaimages/villa3.jpg",
                      Occupancy = 5,
                      Rate = 200,
                      Sqft = 550,
                      Amenity = "",
                      CreatedDate = DateTime.Now

                  },
                   new Villa()
                   {
                       Id = 4,
                       Name = "Diamond Pool Villa",
                       Details = "lorem ipsum ie kafsj  jssv ",
                       ImageUrl = "https://dotnetmasteryimages.blob.core.windows.net/bluevillaimages/villa4.jpg",
                       Occupancy = 5,
                       Rate = 300,
                       Sqft = 450,
                       Amenity = "",
                       CreatedDate = DateTime.Now

                   },
                    new Villa()
                    {
                        Id = 5,
                        Name = "Premium Pool Villa",
                        Details = "lorem ipsum ie kafsj  jssv ",
                        ImageUrl = "https://dotnetmasteryimages.blob.core.windows.net/bluevillaimages/villa5.jpg",
                        Occupancy = 5,
                        Rate = 200,
                        Sqft = 350,
                        Amenity = "",
                        CreatedDate = DateTime.Now

                    }
                );
        }
    }
}
