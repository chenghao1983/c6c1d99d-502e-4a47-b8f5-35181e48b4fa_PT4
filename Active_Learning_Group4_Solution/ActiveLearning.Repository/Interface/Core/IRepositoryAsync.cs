using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActiveLearning.Repository.Interface.Core
{
    public interface IRepositoryAsync<TEntity> where TEntity : class
    {
        Task<TEntity> FindAsync(params object[] keyValues);

        Task<bool> DeleteAsync(params object[] keyValues);


    }
}
