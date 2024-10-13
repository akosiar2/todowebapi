using Application.Interfaces;
using Domain;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.ViewModel;
using ToDoApp.Wrapper;

namespace ToDoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _todoService;

        public TodoController(ITodoService todoService) { _todoService = todoService; }

        [HttpGet]
        public Response<IEnumerable<TodoItem>> GetTodos()
        {
            return new Response<IEnumerable<TodoItem>>( _todoService.GetTodos(), "Success");
        }

        [HttpPost]
        public Response<TodoItemModel> AddTodo(TodoItemModel item)
        {
            var todoItem = new TodoItem() { Title = item.Title ?? "", IsCompleted = item.IsCompleted};

            var resp = _todoService.InsertTodo(todoItem);
            item.Id = resp.Id;

            return new Response<TodoItemModel>(item);
        }

        [HttpPut("{id}")]
        public Response<bool> UpdateTodo(int id, [FromBody] TodoItemModel item )
        {
            var todoItem = new TodoItem() { Id = item.Id, Title = item.Title ?? "", IsCompleted = item.IsCompleted };
            var resp = _todoService.UpdateTodo(todoItem);

            return new Response<bool>(resp);
        }

        [HttpDelete]
        public Response<bool> DeleteTodo(int id)
        {
            var resp = _todoService.DeleteTodo(id);

            return new Response<bool>(resp);
        }
    }
}
