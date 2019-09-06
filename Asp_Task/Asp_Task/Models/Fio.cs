using System.ComponentModel.DataAnnotations;

namespace Asp_Task.Models
{
    public class Fio
    {
        [Required(ErrorMessage = "Введите имя")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введите фамилию")]
        public string Surname { get; set; }
    }
}
