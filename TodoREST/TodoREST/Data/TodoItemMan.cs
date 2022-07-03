using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TodoREST.Model;

namespace TodoREST.Data
{
    public class TodoItemMan
    {
        IRestService restService;

        public TodoItemMan(IRestService service)
        {
            restService = service; 
        }

        public Task<List<TodItem>> GetTasksAsync()
        {
            return restService.RefreshDataAsync();
        }

        public Task SaveTaskAsync(TodItem item, bool isNewItem = false)
        {
            return restService.SaveTodoItemAsync(item, isNewItem);
        }

        public Task DeleteTaskAsync(TodItem item)
        {
            return restService.DeleteTodoItemAsync(item.ID);
        }
    }
}
