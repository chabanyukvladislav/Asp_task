using System;
using Asp_Task.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Asp_Task.Services
{
    public class CookieProvider : ICookieProvider
    {
        private const string USER_KEY = "UserCookie";
        private const string VALIDATION_COUNT_KEY = "ValidationCountCookie";

        private readonly IHttpContextAccessor _context;

        public CookieProvider(IHttpContextAccessor context)
        {
            _context = context;
        }

        public User GetUser()
        {
            return _context.HttpContext.Request.Cookies.TryGetValue(USER_KEY, out var userJson)
                ? DeserializeUser(userJson)
                : new User();
        }

        public void SaveUser(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var userJson = SerializeUser(user);
            _context.HttpContext.Response.Cookies.Append(USER_KEY, userJson);
        }

        public void IncrementValidationCounter()
        {
            var oldCount = 0;
            if (_context.HttpContext.Request.Cookies.TryGetValue(VALIDATION_COUNT_KEY, out var countString) &&
                int.TryParse(countString, out var count))
                oldCount = count;

            oldCount++;
            _context.HttpContext.Response.Cookies.Append(VALIDATION_COUNT_KEY, oldCount.ToString());
        }

        public int GetValidationCount()
        {
            if (_context.HttpContext.Request.Cookies.TryGetValue(VALIDATION_COUNT_KEY, out var countString) &&
                int.TryParse(countString, out var count))
                return count;

            return 0;
        }

        private static string SerializeUser(User user)
        {
            if(user == null)
                throw new ArgumentNullException(nameof(user));

            try
            {
                return JsonConvert.SerializeObject(user);
            }
            catch (JsonException)
            {
                return "";
            }
        }

        private static User DeserializeUser(string userJson)
        {
            if (string.IsNullOrWhiteSpace(userJson))
                throw new ArgumentException(nameof(userJson));

            try
            {
                return JsonConvert.DeserializeObject<User>(userJson);
            }
            catch (JsonException)
            {
                return new User();
            }
        }
    }
}
