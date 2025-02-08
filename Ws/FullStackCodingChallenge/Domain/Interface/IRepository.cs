using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface
{
    public interface IRepository<T>
    {
      
            Task<IEnumerable<T>> GetAllAsync();
            Task<T> GetByIdAsync(int id);
            Task<int> AddAsync(T entity); // Devuelve el ID insertado
            Task<bool> UpdateAsync(T entity); // Permite actualización
            Task<bool> DeleteAsync(int id);
        
    }
}
