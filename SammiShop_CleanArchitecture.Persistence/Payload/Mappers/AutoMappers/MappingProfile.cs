using AutoMapper;

namespace SammiShop_CleanArchitecture.Persistence.Payload.Mappers.AutoMappers
{
    public class MappingProfile<TEntity, TEntityDTO> : Profile
    {
        public MappingProfile()
        {
            CreateMap<TEntity, TEntityDTO>();
        }
    }
}
