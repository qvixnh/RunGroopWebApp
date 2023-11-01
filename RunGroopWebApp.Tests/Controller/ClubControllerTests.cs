using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RunGroopWebApp.Controllers;
using RunGroopWebApp.Interfaces;
using RunGroopWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunGroopWebApp.Tests.Controller
{
    public  class ClubControllerTests
    {
        private readonly IClubRepository _clubRepository;
        private readonly IPhotoService _photoService;
        private readonly HttpContextAccessor _httpContextAccessor;
        private readonly ClubController _clubController;

        public ClubControllerTests()
        {
            _clubRepository = A.Fake<IClubRepository>();
            _photoService = A.Fake<IPhotoService>();
            _httpContextAccessor = A.Fake<HttpContextAccessor>();
            //SUT
            _clubController = new ClubController(_clubRepository, _photoService, _httpContextAccessor);
        }
        [Fact]
        public void ClubController_Index_ReturnsSuccess()
        {
            //Arrange
            //mock the club
            var clubs = A.Fake<IEnumerable<Club>>();
            A.CallTo(() => _clubRepository.GetAll()).Returns(clubs);
            //Act
            var result = _clubController.Index();
            //Assert - object check actions()
            result.Should().BeOfType<Task<IActionResult>>();

        }

        [Fact]
        public void ClubController_Detail_ReturnsSuccess()
        {
            //arrange
            var id = 1;
            var club = A.Fake<Club>();
            A.CallTo(()=> _clubRepository.GetByIdAsync(id)).Returns(club);
            //Act
            var result = _clubController.Detail(id);
            //Assert
            result.Should().BeOfType<Task<IActionResult>>();
        }
    }
}
