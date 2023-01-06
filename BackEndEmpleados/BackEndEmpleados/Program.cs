namespace BackEndEmpleados
{
    using BackEndEmpleados.Models;
    using Microsoft.EntityFrameworkCore;
    using BackEndEmpleados.Service.Contract;
    using BackEndEmpleados.Service.Implementation;
    using BackEndEmpleados.DTOs;
    using BackEndEmpleados.Utilities;
    using AutoMapper;
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            //Data Source = localhost\\local; Initial Catalog = DBEmpleado; Integrated Security = True; TrustServerCertificate = true

            builder.Services.AddDbContext<DbempleadoContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("SQLServerConnection"));
            });

            builder.Services.AddScoped<IDepartamentoService, DepartamentoService>();
            builder.Services.AddScoped<IEmpleadoService, EmpleadoService>();
            builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

            #region CORS

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CORSPolicy", x => { x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod(); });
            });
            #endregion

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            

            //app.UseAuthorization();



            #region Peticiones API REST
            app.MapGet("/departamento/lista", async (IDepartamentoService _departamentoService, IMapper _mapper) =>//Inyectamos servicio y Automappoer
            {
                var listDepartamentos = await _departamentoService.GetDepartamentos();
                var listDepartamentoDTO = _mapper.Map<List<DepartamentoDTO>>(listDepartamentos);

                if (listDepartamentoDTO.Count() > 0)
                    return Results.Ok(listDepartamentoDTO);
                else
                    return Results.NotFound();
            });

            app.MapGet("/empleados/lista", async (IEmpleadoService _empleadoService, IMapper _mapper) =>//Inyectamos servicio y Automappoer
            {
                var listEmpleados = await _empleadoService.GetEmpleados();
                var listDepartamentoDTO = _mapper.Map<List<EmpleadoDTO>>(listEmpleados);

                if (listDepartamentoDTO.Count() > 0)
                    return Results.Ok(listDepartamentoDTO);
                else
                    return Results.NotFound();
            });

            app.MapPost("/empleado/crear", async (
                EmpleadoDTO modelo,
                IEmpleadoService _empleadoService, 
                IMapper _mapper) => 
            {
                var empleado = _mapper.Map<Empleado>(modelo);
                var empleadoCreado = await _empleadoService.AddEmpleado(empleado);

                if(empleadoCreado.IdEmpleado != 0)
                    return Results.Ok(_mapper.Map<EmpleadoDTO>(empleadoCreado));
                else
                    return Results.StatusCode(StatusCodes.Status500InternalServerError);
            });

            app.MapPut("/empleado/modificar/{idEmpleado}", async (
                int idEmpleado,
                EmpleadoDTO modelo,
                IEmpleadoService _empleadoService,
                IMapper _mapper) => 
            {

                var empleadoEncontrado = await _empleadoService.GetEmpleado(idEmpleado); 

                if(empleadoEncontrado is null)
                    return Results.NotFound();

                var _empleado = _mapper.Map<Empleado>(modelo);

                empleadoEncontrado.NombreCompleto = _empleado.NombreCompleto;
                empleadoEncontrado.Sueldo = _empleado.Sueldo;
                empleadoEncontrado.FechaContrato = _empleado.FechaContrato;
                empleadoEncontrado.IdDepartamento = _empleado.IdDepartamento;

                var updateEmpleadoResult = await _empleadoService.UpdateEmpleado(empleadoEncontrado);

                if (updateEmpleadoResult)
                    return Results.Ok(_mapper.Map<EmpleadoDTO>(updateEmpleadoResult));
                else
                    return Results.StatusCode(StatusCodes.Status500InternalServerError);
            });

            app.MapDelete("/empleado/eliminar/{idEmpleado}", async (
                int idEmpleado,
                IEmpleadoService _empleadoService) =>
            {
                var empleadoEncontrado = await _empleadoService.GetEmpleado(idEmpleado);

                if (empleadoEncontrado is null)
                    return Results.NotFound();

                var deleteEmpleadoResult = await _empleadoService.DeleteEmpleado(empleadoEncontrado);

                if (deleteEmpleadoResult)
                    return Results.Ok();
                else
                    return Results.StatusCode(StatusCodes.Status500InternalServerError);
            });

            #endregion

            app.UseCors("CORSPolicy");
            app.Run();
        }
    }
}