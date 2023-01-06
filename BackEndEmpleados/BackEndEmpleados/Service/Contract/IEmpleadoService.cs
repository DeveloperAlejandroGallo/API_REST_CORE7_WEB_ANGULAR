namespace BackEndEmpleados.Service.Contract
{
    using BackEndEmpleados.Models;
    public interface IEmpleadoService
    {
        Task<List<Empleado>> GetEmpleados();
        Task<Empleado> GetEmpleado(int idEmpleado);
        Task<Empleado> AddEmpleado(Empleado empleado);
        Task<bool> UpdateEmpleado(Empleado empleado);
        Task<bool> DeleteEmpleado(Empleado empleado);
    }
}
