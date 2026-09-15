using Application.Contracts.EmployeeContract;
using Application.Contracts.TenantContract;
using Application.Interfaces.Base;
using Infrastructure.Persistence;
using Infrastructure.Services.EmployeeService;
using Infrastructure.Services.TenantService;

namespace Infrastructure.Services.Base
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly MasterDbContext _masterContext;
        private readonly TenantDbContext _tenantContext;

        public RepositoryManager(MasterDbContext masterContext, TenantDbContext tenantContext)
        {
            _masterContext = masterContext;
            _tenantContext = tenantContext;
        }

        public ITenantRepository tenantRepository => new TenantRepository(_masterContext);
        public IEmployeeRepository employeeRepository => new EmployeeRepository(_tenantContext);

        public async Task SaveAsync()
        {
            await _masterContext.SaveChangesAsync();
            await _tenantContext.SaveChangesAsync();
        }
    }
}