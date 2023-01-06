namespace BackEndEmpleados.Service.Implementation
{
    using Microsoft.EntityFrameworkCore;
    using BackEndEmpleados.Models;
    using BackEndEmpleados.Service.Contract;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    public class DepartamentoService : IDepartamentoService
    {
        private DbempleadoContext _dbContext;

        public DepartamentoService(DbempleadoContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Departamento>> GetDepartamentos()
        {
            try
            {
                var lista = new List<Departamento>();
                lista = await _dbContext.Departamentos.ToListAsync();

                return lista;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
