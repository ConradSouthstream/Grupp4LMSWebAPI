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
            ForfattareMap();
            LitteraturMap();
        }

        private void ForfattareMap()
        {
            CreateMap<Forfattare, ForfattareDto>();

            CreateMap<Forfattare, ForfattareInklusiveLitteraturDto>();
        }

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
