using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using MyDishesApp.Service.Dtos;
using MyDishesApp.Service.Services.Interfaces;
using MyDishesApp.WebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace WebApi.Tests.Unit.Controllers
{
    public class IngredientControllerTests
    {
        public class Constructor : IngredientControllerTestBase
        {
            [Fact]
            public void InitializeIngredientControllerCorrectly() => Assert.NotNull(
                new IngredientController(IngredientServiceMock.Object));
            
            [Fact]
            public void ThrowsArgumentNullExceptionWhenIngredientServiceIsNull() => Assert.Throws<ArgumentNullException>("ingredientService",
                () => new IngredientController(null));
        }

        public class GetIngredients : IngredientControllerTestBase
        {
            private const string Input = "Pizza";

            [Fact]
            public async Task CallsServiceCorrectly()
            {
                // Arrange
                var serviceResponse = new List<IngredientDto> { new IngredientDto { Name = Input } };

                IngredientServiceMock
                    .Setup(s => s.GetAllAsync())
                    .ReturnsAsync(serviceResponse)
                    .Verifiable();

                var sut = CreateController();

                // Act
                var result = await sut.GetAllAsync();

                // Assert
                VerifyMocks();
                Assert.NotNull(result);
                Assert.Equal(serviceResponse, result.Value);
            }

            [Fact]
            public void LogsAndRethrowsException()
            {
                var sut = CreateController();
                Assert.ThrowsAsync<Exception>(() => sut.GetAllAsync());
            }
        }

        public abstract class IngredientControllerTestBase
        {
            protected readonly Mock<ILogger<IngredientController>> LoggerMock = new Mock<ILogger<IngredientController>>();
            protected readonly Mock<IMapper> MapperMock = new Mock<IMapper>();
            protected readonly Mock<IIngredientService> IngredientServiceMock = new Mock<IIngredientService>();

            protected IngredientController CreateController()
            {
                return new IngredientController(IngredientServiceMock.Object);
            }

            protected void VerifyMocks()
            {
                IngredientServiceMock.Verify();
            }
        }
    }
}