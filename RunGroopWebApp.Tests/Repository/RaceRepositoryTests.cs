using Microsoft.EntityFrameworkCore;
using RunGroopWebApp.Data.Enum;
using RunGroopWebApp.Data;
using RunGroopWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RunGroopWebApp.Repository;
using FluentAssertions;

namespace RunGroopWebApp.Tests.Repository
{
    public class RaceRepositoryTests
    {
        private async Task<ApplicationDbContext> GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new ApplicationDbContext(options);
            databaseContext.Database.EnsureCreated();
            if (await databaseContext.Races.CountAsync() < 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    databaseContext.Races.Add(
                        new Race()
                        {
                            Title = "Running Race 1",
                            Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",
                            Description = "This is the description of the first race",
                            RaceCategory = RaceCategory.Marathon,
                            Address = new Address()
                            {
                                Street = "123 Main St",
                                City = "Charlotte",
                                State = "NC"
                            }
                        }
                    );
                    await databaseContext.SaveChangesAsync();
                }
            }
            return databaseContext;
        }
        [Fact]
        public async void RaceRepository_Add_ReturnsBool()
        {
            //Arrange 
            var race = new Race()
            {
                Title = "Running Race 1",
                Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",
                Description = "This is the description of the first run",
                RaceCategory = RaceCategory.FiveK,
                Address = new Address()
                {
                    Street = "123 Main St",
                    City = "Charlotte",
                    State = "NC"
                }
            };
            var dbContext = await GetDbContext();
            var raceRepository = new RaceRepository(dbContext);

            //Act
            var result = raceRepository.Add(race);//it's a bool 
            //Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async void RaceRepository_GetByAsync_ReturnBool()
        {
            //Arrange
            var id = 1;
            var dbContext = await GetDbContext();
            var raceRepository = new RaceRepository(dbContext);
            //Act
            var result = raceRepository.GetByIdAsync(id);
            //Assert 
            //check the type, not check the real data, check that's null, not returning the real data
            result.Should().NotBeNull();
            result.Should().BeOfType<Task<Race>>();

        }

    }
}
