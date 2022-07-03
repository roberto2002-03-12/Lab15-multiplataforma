using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TodoREST.Model;

namespace TodoREST.Data
{
    public interface IRestService
    {
        Task<List<TodItem>> RefreshDataAsync();
        Task SaveTodoItemAsync(TodItem item, bool isNewItem);
        Task DeleteTodoItemAsync(string id);
    }
}
