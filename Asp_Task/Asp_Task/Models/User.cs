using System.ComponentModel.DataAnnotations;

namespace Asp_Task.Models
{
    public class User
    {
        public User()
        {
            Fio = new Fio();
        }

        public User(Fio fio)
        {
            Fio = fio ?? new Fio();
        }

        public Fio Fio { get; }

        [Required(ErrorMessage = "Введите email")]
        [EmailAddress(ErrorMessage = "Введите email")]
        public string Email { get; set; }
    }
}
