using AutoMapper;
using Grupp4Lms.Core.Dto;
using Grupp4Lms.Core.Entities;

namespace Grupp4Lms.Data.MapperProfiles
{
    /// <summary>
    /// Map metoder för automapper
    /// </summary>
    public class MapperProfile : Profile
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public MapperProfile() 
        {
            OtherMap();
            ForfattareMap();
            LitteraturMap();
        }

        /// <summary>
        /// Mappers för Niva till NivaDto och
        /// Amne till AmneDto
        /// </summary>
        private void OtherMap()
        {
            CreateMap<Niva, NivaDto>();
            CreateMap<Amne, AmneDto>();
        }


        /// <summary>
        /// Mappers för Forfattare till ForfattareDto och
        /// Forfattare till ForfattareInklusiveLitteraturDto
        /// </summary>
        private void ForfattareMap()
        {
            CreateMap<Forfattare, ForfattareDto>();

            CreateMap<Forfattare, ForfattareInklusiveLitteraturDto>();
        }


        /// <summary>
        /// Mappers för Litteratur till LitteraturDto och 
        /// Litteratur till LitteraturInklusiveForfattareDto
        /// </summary>
        private void LitteraturMap()
        {
            CreateMap<Litteratur, LitteraturDto>()
                .ForMember(dest => dest.Amne, from => from.MapFrom(a => a.Amne.Namn))
                .ForMember(dest => dest.Niva, from => from.MapFrom(n => n.Niva.Namn));

            CreateMap<Litteratur, LitteraturInklusiveForfattareDto>()
                .ForMember(dest => dest.Amne, from => from.MapFrom(a => a.Amne.Namn))
                .ForMember(dest => dest.Niva, from => from.MapFrom(n => n.Niva.Namn));
        }
    }
}