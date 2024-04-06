using GWMBackend.Core.Model.Base;
using GWMBackend.Data.Interface;
using GWMBackend.Data.Repository;
using GWMBackend.Domain.Models;
using Microsoft.Extensions.Options;

namespace GWMBackend.Data.Base
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private GWM_DBContext _repoContext;

        public RepositoryWrapper(GWM_DBContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }

        public IEmailRepository email => new EmailRepository(_repoContext);

        public IOrderRepository order => new OrderRepository(_repoContext);

        public void Save()
        {
            _repoContext.SaveChanges();
        }
    }
}
