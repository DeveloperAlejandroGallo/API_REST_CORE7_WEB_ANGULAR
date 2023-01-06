namespace BackEndEmpleados.Service.Implementation
{
    using Microsoft.EntityFrameworkCore;
    using BackEndEmpleados.Models;
    using BackEndEmpleados.Service.Contract;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    public class EmpleadoService : IEmpleadoService
    {
        private DbempleadoContext _dbContext;
        public EmpleadoService(DbempleadoContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Empleado> AddEmpleado(Empleado empleado)
        {
            try
            {
                var result = _dbContext.Empleados.Add(empleado);
                await _dbContext.SaveChangesAsync();
                return result.Entity;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteEmpleado(Empleado empleado)
        {
            try
            {
                _dbContext.Empleados.Remove(empleado);
                return await _dbContext.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Empleado> GetEmpleado(int idEmpleado)
        {
            try
            {
                var result = new Empleado();

                result = await _dbContext.Empleados
                    .Include(dpt => dpt.IdDepartamentoNavigation)
                    .Where(emp => emp.IdEmpleado == idEmpleado)
                    .FirstOrDefaultAsync();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Empleado>> GetEmpleados()
        {
            try
            {
                var lista = new List<Empleado>();
                lista = await _dbContext.Empleados.Include(x => x.IdDepartamentoNavigation).ToListAsync();

                return lista;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateEmpleado(Empleado empleado)
        {
            try
            {
                _dbContext.Empleados.Update(empleado);
                return await _dbContext.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
