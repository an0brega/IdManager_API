using System.ComponentModel.DataAnnotations;

namespace MeuTodo.ViewModels
{
    public class CreateIdViewModel
    {
        [Required]
        public string Title { get; set; }
    }
}
