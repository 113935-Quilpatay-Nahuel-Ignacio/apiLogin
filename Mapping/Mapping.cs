using ApiLogin.DTOs;
using ApiLogin.Models;
using ApiLogin.Services.PersonaService.Commands;
using AutoMapper;

namespace ApiLogin.Mapping
{
    public class Mapping : Profile
    {
        public Mapping() 
        {
            CreateMap<Persona, PersonaDTO>().ReverseMap();
            CreateMap<PostPersonaCommand, Persona>().ReverseMap();
            CreateMap<LoginPersonaCommand, Persona>().ReverseMap();
        }
    }
}
