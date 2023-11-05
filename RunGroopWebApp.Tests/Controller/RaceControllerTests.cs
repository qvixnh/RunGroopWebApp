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
    public  class RaceControllerTests
    {
        private readonly IRaceRepository _raceRepository;
        private readonly IPhotoService _photoService;
        private readonly HttpContextAccessor _httpContextAccessor;
        private readonly RaceController _raceController;

        public RaceControllerTests()
        {
            _raceRepository = A.Fake<IRaceRepository>();
            _photoService = A.Fake<IPhotoService>();
            _httpContextAccessor = A.Fake<HttpContextAccessor>();
            //SUT
            _raceController = new RaceController(_raceRepository, _photoService, _httpContextAccessor);
        }
        [Fact]
        public async void raceController_Index_ReturnsSuccess()
        {
            //Arrange
            //mock the race
            var races = A.Fake<IEnumerable<Race>>();
            A.CallTo(() => _raceRepository.GetAll()).Returns(races);
            //Act
            var result = await _raceController.IndexAsync();
            //Assert - object check actions()
            result.Should().BeOfType<ViewResult>();

        }

        [Fact]
        public async void raceController_Detail_ReturnsSuccess()
        {
            //arrange
            var id = 1;
            var race = A.Fake<Race>();
            A.CallTo(() => _raceRepository.GetByIdAsync(id)).Returns(race);
            //Act
            var result = await _raceController.DetailAsync(id);
            //Assert
            result.Should().BeOfType<ViewResult>();
        }

    }
}
