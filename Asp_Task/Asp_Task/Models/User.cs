using Newtonsoft.Json;

namespace Asp_Task.Models
{
    public class User
    {
        public User()
        {
            FullName = new UserFullName();
            AdditionalInfo = new UserAdditionalInfo();
        }

        public User(UserFullName fio, UserAdditionalInfo additionalInfo)
        {
            FullName = fio ?? new UserFullName();
            AdditionalInfo = additionalInfo ?? new UserAdditionalInfo();
        }

        [JsonProperty("full_name")]
        public UserFullName FullName { get; set; }

        [JsonProperty("additional_info")]
        public UserAdditionalInfo AdditionalInfo { get; set; }
    }
}
