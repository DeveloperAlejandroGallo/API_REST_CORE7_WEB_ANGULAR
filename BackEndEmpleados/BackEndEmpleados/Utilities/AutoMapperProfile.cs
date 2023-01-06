namespace BackEndEmpleados.Utilities
{
    using AutoMapper;
    using BackEndEmpleados.DTOs;
    using BackEndEmpleados.Models;
    using System.Globalization;

    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Departamento, DepartamentoDTO>().ReverseMap();

            CreateMap<Empleado, EmpleadoDTO>()
                .ForMember(destino => destino.NombreDepartamento, 
                           opciones => opciones.MapFrom(origen => origen.IdDepartamentoNavigation!.Nombre))
                .ForMember(destino => destino.FechaContrato,
                           opciones => opciones.MapFrom(origen => origen.FechaContrato!.Value.ToString("dd/MM/yyyy")));

            CreateMap<EmpleadoDTO, Empleado>()
                .ForMember(destino => destino.IdDepartamentoNavigation, 
                           opcion => opcion.Ignore())
                .ForMember(destino => destino.FechaContrato,
                           opcion => opcion.MapFrom(origen => DateTime.ParseExact(origen.FechaContrato!, "dd/MM/yyyy",CultureInfo.InvariantCulture)));
        }
    }
}
