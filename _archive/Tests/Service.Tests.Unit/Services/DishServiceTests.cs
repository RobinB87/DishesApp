using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using MyDishesApp.Repository.Data.Entities;
using MyDishesApp.Repository.Repositories.Interfaces;
using MyDishesApp.Service.Dtos;
using MyDishesApp.Service.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Service.Tests.Unit.Services
{
    public class DishServiceTests
    {
        public class Constructor : DishServiceTestsBase
        {
            [Fact]
            public void InitializeDishServiceCorrectly() => Assert.NotNull(
                new DishService(LoggerMock.Object, MapperMock.Object, DishRepositoryMock.Object, IngredientRepositoryMock.Object));

            [Fact]
            public void ThrowsArgumentNullExceptionWhenLoggerIsNull() => Assert.Throws<ArgumentNullException>("logger",
                () => new DishService(null, MapperMock.Object, DishRepositoryMock.Object, IngredientRepositoryMock.Object));

            [Fact]
            public void ThrowsArgumentNullExceptionWhenMapperIsNull() => Assert.Throws<ArgumentNullException>("mapper",
                () => new DishService(LoggerMock.Object, null, DishRepositoryMock.Object, IngredientRepositoryMock.Object));

            [Fact]
            public void ThrowsArgumentNullExceptionWhenDishRepositoryIsNull() => Assert.Throws<ArgumentNullException>("dishRepository",
                () => new DishService(LoggerMock.Object, MapperMock.Object, null, IngredientRepositoryMock.Object));

            [Fact]
            public void ThrowsArgumentNullExceptionWhenIngredientRepositoryIsNull() => Assert.Throws<ArgumentNullException>("ingredientRepository",
                () => new DishService(LoggerMock.Object, MapperMock.Object, DishRepositoryMock.Object, null));
        }

        public class GetAllAsync : DishServiceTestsBase
        {
            private const string Input = "Pizza";

            [Fact]
            public async Task CallsServiceCorrectly()
            {
                // Arrange
                var serviceResponse = new List<Dish> { new Dish { Name = Input } };
                var mappedResult = new List<DishDto> { new DishDto { Name = Input } };

                DishRepositoryMock
                    .Setup(s => s.GetDishesAsync())
                    .ReturnsAsync(serviceResponse)
                    .Verifiable();

                MapperMock
                    .Setup(m => m.Map<IEnumerable<DishDto>>(serviceResponse))
                    .Returns(mappedResult)
                    .Verifiable();

                var sut = CreateService();

                // Act
                var result = await sut.GetAllAsync();

                // Assert
                VerifyMocks();
                Assert.NotNull(result);
                Assert.Equal(mappedResult, result);
            }

            [Fact]
            public void LogsAndRethrowsException()
            {
                var sut = CreateService();
                Assert.ThrowsAsync<Exception>(() => sut.GetAllAsync());
            }
        }

        public class GetByIdAsync : DishServiceTestsBase
        {
            private const string Input = "Pizza";

            [Fact]
            public async Task CallsServiceCorrectly()
            {
                // Arrange
                var serviceResponse = new Dish { Name = Input };
                var mappedResult = new DishDto { Name = Input };

                DishRepositoryMock
                    .Setup(s => s.GetDishAsync(3))
                    .ReturnsAsync(serviceResponse)
                    .Verifiable();

                MapperMock
                    .Setup(m => m.Map<DishDto>(serviceResponse))
                    .Returns(mappedResult)
                    .Verifiable();

                var sut = CreateService();

                // Act
                var result = await sut.GetByIdAsync(3);

                // Assert
                VerifyMocks();
                Assert.NotNull(result);
                Assert.Equal(mappedResult, result);
            }

            [Fact]
            public void LogsAndRethrowsException()
            {
                var sut = CreateService();
                Assert.ThrowsAsync<Exception>(() => sut.GetByIdAsync(3));
            }
        }

        public class PostAsync : DishServiceTestsBase
        {
            [Fact]
            public async Task CallsServiceCorrectly()
            {
                // Arrange
                var ingredientToAdd = new IngredientDto { Name = "Ansjovis" };
                var dishToAdd = new DishDto
                {
                    Name = "Pizza",
                    Country = "Italy",
                    Ingredients = new[]
                    {
                        ingredientToAdd
                    }
                };

                var mappedIngredient = new Ingredient { Name = "Ansjovis" };
                var mappedDish = new Dish
                {
                    Name = "Pizza",
                    Country = "Italy",
                    DishIngredients =
                    {
                        new DishIngredient
                        {
                            Ingredient = mappedIngredient
                        }
                    }
                };

                MapperMock
                    .Setup(m => m.Map<Dish>(dishToAdd))
                    .Returns(mappedDish)
                    .Verifiable();

                IngredientRepositoryMock
                    .Setup(s => s.GetIngredientAsync(ingredientToAdd.Name))
                    .ReturnsAsync(mappedIngredient)
                    .Verifiable();

                DishRepositoryMock
                    .Setup(s => s.AddDishAsync(mappedDish))
                    .Returns(Task.CompletedTask)
                    .Verifiable();

                MapperMock
                    .Setup(m => m.Map<DishDto>(mappedDish))
                    .Returns(dishToAdd)
                    .Verifiable();

                var sut = CreateService();

                // Act
                var result = await sut.PostAsync(dishToAdd);

                // Assert
                VerifyMocks();
                Assert.IsType<DishDto>(result);
            }

            [Fact]
            public void LogsAndRethrowsException()
            {
                var sut = CreateService();
                Assert.ThrowsAsync<Exception>(() => sut.PostAsync(new DishDto()));
            }
        }

        public abstract class DishServiceTestsBase
        {
            protected readonly Mock<ILogger<DishService>> LoggerMock = new Mock<ILogger<DishService>>();
            protected readonly Mock<IMapper> MapperMock = new Mock<IMapper>();
            protected readonly Mock<IDishRepository> DishRepositoryMock = new Mock<IDishRepository>();
            protected readonly Mock<IIngredientRepository> IngredientRepositoryMock = new Mock<IIngredientRepository>();

            protected DishService CreateService()
            {
                return new DishService(LoggerMock.Object, MapperMock.Object,
                    DishRepositoryMock.Object, IngredientRepositoryMock.Object);
            }

            protected void VerifyMocks()
            {
                LoggerMock.Verify();
                MapperMock.Verify();
                DishRepositoryMock.Verify();
                IngredientRepositoryMock.Verify();
            }
        }
    }
}