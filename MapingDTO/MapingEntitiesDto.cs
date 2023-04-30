using AutoMapper;
using TelaLogin.DTO;
using TelaLogin.Model;

namespace TelaLogin.MapingDTO
{
    public class MapingEntitiesDto:Profile
    {
        public MapingEntitiesDto()
        {
            //permite mapear de um para outro e virse -versa
            //CreateMap<Usuario, UsuarioResponseDto>().ReverseMap();
            CreateMap<Usuario, UsuarioResponse>();
            CreateMap<UsuarioRequest, Usuario>();
        }
    }
}
