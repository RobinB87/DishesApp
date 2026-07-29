using AutoMapper;
using MyDishesApp.Repository.Repositories.Interfaces;
using MyDishesApp.Service.Dtos;
using MyDishesApp.Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyDishesApp.Service.Services
{
    public class IngredientService : IIngredientService
    {
        private readonly IMapper _mapper;
        private readonly IIngredientRepository _ingredientRepository;

        /// <summary>
        /// Initializes a new instance of <see cref="IngredientService" />
        /// </summary>
        /// <param name="mapper">The mapper</param>
        /// <param name="ingredientRepository">The ingredient repository</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="mapper" /> is null.</exception>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="ingredientRepository" /> is null.</exception>
        public IngredientService(IMapper mapper, IIngredientRepository ingredientRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _ingredientRepository = ingredientRepository ?? throw new ArgumentNullException(nameof(ingredientRepository));
        }

        /// <inheritdoc />
        public async Task<IEnumerable<IngredientDto>> GetAllAsync()
        {
            var ingredientEntities = await _ingredientRepository.GetIngredientsAsync();
            return _mapper.Map<IEnumerable<IngredientDto>>(ingredientEntities);
        }
    }
}
