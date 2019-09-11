using Asp_Task.Models;

namespace Asp_Task.Services
{
    public interface ICookieProvider
    {
        User GetUser();

        void SaveUser(User user);

        void IncrementValidationCounter();

        int GetValidationCount();
    }
}
