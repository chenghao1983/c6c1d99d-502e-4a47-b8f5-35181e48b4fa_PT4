using System;

namespace ActiveLearning.Business.Interface
{
    public interface IManagerFactoryBase<TEntity> where TEntity : class
    {
        TEntity Create();


    }
}
