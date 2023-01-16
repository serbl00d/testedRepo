using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Todo_Api.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Todo_Api.Controllers
{
    [Route("[controller]")]
    public class ToDoController : ControllerBase
    {
        private readonly TodoDbContext _context;
        public ToDoController(TodoDbContext context)
        {
            _context = context;
        }
        // Tüm Todoları Listeler
        [HttpGet]
        public async Task<ActionResult<List<ToDo>>> Get()
        {
            return Ok(await _context.ToDos.ToListAsync());
        }
        // Id Ye göre todo yu listeler
        [HttpGet("{id}")]
        public async Task<ActionResult<List<ToDo>>> Get(int id)
        {
            var todo = await _context.ToDos.FindAsync(id);
            if (todo == null)
                return BadRequest("Todo Not Found");
            return Ok(await _context.ToDos.ToListAsync());
        }
        // Todo Eklemenizi Sağlar
        [HttpPost]
        public async Task<ActionResult<ToDo>> AddTodo(ToDo toDo)
        {
            await _context.ToDos.AddAsync(toDo);
            await _context.SaveChangesAsync();
            return Ok(toDo);
        }
        // İstediğiniz Todonun Taskını Değiştirmenizi Sağlar
        [HttpPut]
        public async Task<ActionResult<List<ToDo>>> UpdateTodo(ToDo request)
        {
            var dBtodo = await _context.ToDos.FindAsync(request.Id);
            if (dBtodo == null)
                return BadRequest("Todo Not Found");
            if (request.Task != "")
                dBtodo.Task = request.Task;
            if (request.IsActive != dBtodo.IsActive)
                dBtodo.IsActive = request.IsActive;
            await _context.SaveChangesAsync();
            return Ok(await _context.ToDos.ToListAsync());
        }
        // Verdiğiniz Id'ye göre Todonun IsActive Özelliğini Değiştirir(Toggle) görevi görür
        [HttpPut("{id}")]
        public async Task<ActionResult<List<ToDo>>> isActiveTodo(int id)
        {
            var dBtodo = await _context.ToDos.FindAsync(id);
            if (dBtodo == null)
                return BadRequest("Todo Not Found");
            if (dBtodo.IsActive == false)
            {
                dBtodo.IsActive = true;
                await _context.SaveChangesAsync();
                return Ok(await _context.ToDos.ToListAsync());
            }  
            dBtodo.IsActive = false;
            await _context.SaveChangesAsync();
            return Ok(await _context.ToDos.ToListAsync());
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<ToDo>>> DeleteTodo(int id)
        {
            var dBtodo = await _context.ToDos.FindAsync(id);
            if (dBtodo == null)
                return BadRequest("Todo Not Found");
            _context.ToDos.Remove(dBtodo);
            await _context.SaveChangesAsync();
            return Ok(await _context.ToDos.ToListAsync());

        }


    }
}

