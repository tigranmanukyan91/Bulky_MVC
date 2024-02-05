using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository.IRepository
{
	public interface IRepository<T> where T : class
	{
		//T - Category or any other generic model on which we want to do CRUD operation
		IEnumerable<T> GetAll();
		// TODO
		T Get(Expression<Func<T,bool>> filter);
		void Add(T entity);
		void Remove(T entity);
		void RemoveRange(IEnumerable<T> entity);
	}
}
