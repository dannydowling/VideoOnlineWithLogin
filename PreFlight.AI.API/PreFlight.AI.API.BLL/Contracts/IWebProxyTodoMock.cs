using PreFlight.AI.API.BLL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PreFlight.AI.API.BLL.Contracts
{
    public interface ITodosMockProxyService
    {
        Task<IEnumerable<Todo>> GetTodos();
        Task<Todo> GetTodoById(int id);
    }
}
