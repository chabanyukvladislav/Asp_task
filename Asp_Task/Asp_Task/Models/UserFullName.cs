using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Asp_Task.Models
{
    public class UserFullName
    {
        [Required(ErrorMessage = "Введите имя")]
        [JsonProperty("name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введите фамилию")]
        [JsonProperty("surname")]
        public string Surname { get; set; }
    }
}
