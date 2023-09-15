using System;

namespace MeuTodo.Models
{
    /// <summary>
    /// Necessary to use the CREATE, READ, UPDATE, DELETE verbs
    /// </summary>
    public class IdModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool Done { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
    }
}