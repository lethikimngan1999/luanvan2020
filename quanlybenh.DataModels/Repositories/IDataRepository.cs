using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace quanlybenh.DataModels.Repositories
{
    public interface IDataRepository<T> where T : class
    {
        // Them doi tuong
        void Insert(T entity);

        // Cap nhat doi tuong
        void Update(T entity);


        //Delete doi tuong
        void Remove(T entity);

        //Delete doi tuong boi id
        void Remove(object id);

        //Get tat ca 
        IQueryable<T> GetAll();

        // Get mot doi tuong boi dieu kien nao do
        T Get(Expression<Func<T, bool>> where);

        // Get mot doi tuong boi id
        T GetById(object id);

        // Get nhieu doi tuong boi mot dieu kien nao do
        IQueryable<T> GetMany(Expression<Func<T, bool>> where);

        // Delete nhieu doi tuong cung luc
        void RemoveMultiple(List<T> entities);

        //
        T FindSingle(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);

        IQueryable<T> FindAll(params Expression<Func<T, object>>[] includeProperties);

        IQueryable<T> FindAll(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);

    }

}
