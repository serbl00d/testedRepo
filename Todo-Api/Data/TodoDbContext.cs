using System;
using Microsoft.EntityFrameworkCore;
namespace Todo_Api.Data
{
	public class TodoDbContext:DbContext
	{
        public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options){ }
        public DbSet<ToDo> ToDos { get; set; }
    }
}

