using System;

namespace Backend.Repository;

public interface IRepository<TEntity>
{
    Task<IEnumerable<TEntity>> Get();
    Task<TEntity> GetById(int id);
    Task Add(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
    Task Save();

    Task<bool> ExistsByName(string name);

    Task<bool> HasBeersByBrandId(int brandId);

    Task<IEnumerable<TEntity>> GetByBrandId(int brandId);

}
