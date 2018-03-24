using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shop.DAL.Interfaces
{
    /// <summary>
    /// Manages access to data source
    /// </summary>
    /// <typeparam name="TEntity">Сlass of domain model</typeparam>
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Add new item in db
        /// </summary>
        /// <param name="item">Сlass of domain model</param>
        void Create(TEntity item);

        /// <summary>
        /// Find item in db by id 
        /// </summary>
        /// <param name="id">Item id</param>
        /// <returns></returns>
        TEntity FindById(int id);

        /// <summary>
        /// Return all item in db
        /// </summary>
        /// <returns>Collection of TEntity </returns>
        IEnumerable<TEntity> Get();

        /// <summary>
        /// Return items in db by predicate
        /// </summary>
        /// <returns>Collection of TEntity </returns>
        IEnumerable<TEntity> Get(Func<TEntity, bool> predicate);

        /// <summary>
        /// Remove item from db
        /// </summary>
        /// <param name="item">Class of domain model</param>
        void Remove(TEntity item);

        /// <summary>
        /// Edit item information in db
        /// </summary>
        /// <param name="item">Class of domain model</param>
        void Update(TEntity item);
    }
}
