namespace BackEndEmpleados.Service.Contract
{
    using BackEndEmpleados.Models;
    public interface IDepartamentoService
    {
        Task<List<Departamento>> GetDepartamentos();
    }
}
