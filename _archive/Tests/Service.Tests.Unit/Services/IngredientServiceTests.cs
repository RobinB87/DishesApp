using AutoMapper;
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
    public class IngredientServiceTests
    {
        public class Constructor : IngredientServiceTestsBase
        {
            [Fact]
            public void InitializeIngredientServiceCorrectly() => Assert.NotNull(
                new IngredientService(MapperMock.Object, IngredientRepositoryMock.Object));

            [Fact]
            public void ThrowsArgumentNullExceptionWhenMapperIsNull() => Assert.Throws<ArgumentNullException>("mapper",
                () => new IngredientService(null, IngredientRepositoryMock.Object));

            [Fact]
            public void ThrowsArgumentNullExceptionWhenIngredientRepositoryIsNull() => Assert.Throws<ArgumentNullException>("ingredientRepository",
                () => new IngredientService(MapperMock.Object, null));
        }

        public class GetIngredients : IngredientServiceTestsBase
        {
            private const string Input = "Pizza";

            [Fact]
            public async Task CallsServiceCorrectly()
            {
                // Arrange
                var serviceResponse = new List<Ingredient> { new Ingredient { Name = Input } };
                var mappedResult = new List<IngredientDto> { new IngredientDto { Name = Input } };

                IngredientRepositoryMock
                    .Setup(s => s.GetIngredientsAsync())
                    .ReturnsAsync(serviceResponse)
                    .Verifiable();

                MapperMock
                    .Setup(m => m.Map<IEnumerable<IngredientDto>>(serviceResponse))
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

        public abstract class IngredientServiceTestsBase
        {
            protected readonly Mock<IMapper> MapperMock = new Mock<IMapper>();
            protected readonly Mock<IIngredientRepository> IngredientRepositoryMock = new Mock<IIngredientRepository>();

            protected IngredientService CreateService()
            {
                return new IngredientService(MapperMock.Object, IngredientRepositoryMock.Object);
            }

            protected void VerifyMocks()
            {
                MapperMock.Verify();
                IngredientRepositoryMock.Verify();
            }
        }
    }
}