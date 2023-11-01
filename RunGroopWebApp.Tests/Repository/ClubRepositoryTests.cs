using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using RunGroopWebApp.Data;
using RunGroopWebApp.Data.Enum;
using RunGroopWebApp.Models;
using RunGroopWebApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RunGroopWebApp.Tests.Repository
{
    public class ClubRepositoryTests
    {

        private async Task<ApplicationDbContext> GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new ApplicationDbContext(options);
            databaseContext.Database.EnsureCreated();
            if(await databaseContext.Clubs.CountAsync() < 0)
            {
                for (int i = 0; i< 10; i ++) { 
                    databaseContext.Clubs.Add(
                        new Club()
                        {
                            Title = "Running Club 1",
                            Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",
                            Description = "This is the description of the first run",
                            ClubCategory = ClubCategory.City,
                            Address = new Address()
                            {
                                Street = "123 Main St",
                                City = "Charlotte",
                                State = "NC"
                            }
                        }); 
                    await databaseContext.SaveChangesAsync();
                }
            }
            return databaseContext; 
        }
        [Fact]
        public async void ClubRepository_Add_ReturnsBool()
        {
            //Arrange 
            var club = new Club()
            {
                Title = "Running Club 1",
                Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",
                Description = "This is the description of the first run",
                ClubCategory = ClubCategory.City,
                Address = new Address()
                {
                    Street = "123 Main St",
                    City = "Charlotte",
                    State = "NC"
                }
            };
            var dbContext= await GetDbContext();
            var clubRepository = new ClubRepository(dbContext);
            
            //Act
            var result = clubRepository.Add(club);//it's a bool 
            //Assert
            result.Should().BeTrue(); 
        }
        [Fact]
        public async void ClubRepository_GetByAsync_ReturnBôl()
        {
            //Arrange
            var id = 1;
            var dbContext = await GetDbContext();
            var clubRepository = new ClubRepository(dbContext);
            //Act
            var result = clubRepository.GetByIdAsync(id);
            //Assert 
            //check the type, not check the real data, check that's null, not returning the real data
            result.Should().NotBeNull();
            result.Should().BeOfType<Task<Club>>();

        }
    }
}
