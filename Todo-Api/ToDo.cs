using System;
namespace Todo_Api
{
	public class ToDo
	{
		public int Id { get; set; }
		public string Task { get; set; } = string.Empty;
		public bool IsActive { get; set; }
	}
}

