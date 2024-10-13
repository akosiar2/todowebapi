using Application.Interfaces;
using Domain;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class TodoService : ITodoService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public List<TodoItem> _items;
        public int _count;
        public TodoService(IHttpContextAccessor contextAccessor) 
        { 
            _contextAccessor = contextAccessor;

            _items = new List<TodoItem>() { 
                new TodoItem { Id = 1, Title = "Setup", IsCompleted = true }, 
                new TodoItem { Id = 2, Title = "Planning", IsCompleted = true }, 
                new TodoItem { Id = 3, Title = "Deployment", IsCompleted= false } 
            };
            _count = _items.Count;
  
        }

        public IEnumerable<TodoItem> GetTodos()
        {
            return _items;
        }

        public TodoItem InsertTodo(TodoItem item)
        {
            _items.Add(item);

            int next = _count + 1;
            item.Id = next;
            return item;
        }

        public bool UpdateTodo(TodoItem item)
        {
            var exist = _items.FirstOrDefault(x => x.Id == item.Id);
            if(exist != null)
            {
                exist.IsCompleted = !item.IsCompleted;
                return true;
            }
            return false;
        }

        public bool DeleteTodo(int id)
        {
            var todo = _items.FirstOrDefault(x => x.Id == id);
            if (todo != null)
            {
                _items.Remove(todo);
                return true;
            }
            return false;
        }
    }
}
