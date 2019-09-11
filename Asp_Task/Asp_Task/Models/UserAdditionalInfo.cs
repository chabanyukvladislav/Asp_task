using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Asp_Task.Models
{
    public class UserAdditionalInfo
    {
        [Required(ErrorMessage = "Введите email")]
        [EmailAddress(ErrorMessage = "Введите email")]
        [JsonProperty("email")]
        public string Email { get; set; }
    }
}
