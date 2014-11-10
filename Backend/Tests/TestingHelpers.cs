using Parse;
using ParseUnitySampleCommon;
using ParseUnitySampleCommon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseUnitySampleBackend.Tests
{
    public class TestingHelpers
    {
         public static Random random = new Random();

        public static async Task Login()
        {
            if (ParseUser.CurrentUser != null) return;
            var query = from usr in new ParseQuery<GameUser>() where usr.Username == "parse.test@gmail.com" select usr;
            var user = await query.FirstOrDefaultAsync();
            if (user == null)
            {
                var u = new GameUser()
                {
                    Username = "parse.test@gmail.com",
                    Email = "parse.test@gmail.com",
                    Password = "a",
                    PlayerName = "Test User"
                };
                await u.SignUpAsync();
            }
            await ParseUser.LogInAsync("parse.test@gmail.com", "a");
        }

        public static async Task<GameUser> LoginAsANewUser()
        {
            var email = GetRandomUserEmail();
            var u = new GameUser()
            {
                Username = email,
                Email = email,
                Password = "a",
                PlayerName = "Test User"
            };
            await u.SignUpAsync();
            return (GameUser)await ParseUser.LogInAsync(u.Username, "a");
        }

        public static async Task<T> Save<T>(T obj) where T : ParseObject
        {
            await obj.SaveAsync();
            return obj;
        }

        public static async Task<GameUser> LoginAs(GameUser user)
        {
            return (GameUser)await ParseUser.LogInAsync(user.Username, "a");
        }

        public static async Task<GameUser> CreateNewUser()
        {
            var email = GetRandomUserEmail();
            var u = new GameUser()
            {
                Username = email,
                Email = email,
                Password = "a",
                PlayerName = "Test User"
            };
            await u.SignUpAsync();
            return u;
        }

        public static void Logout()
        {
            ParseUser.LogOut();
        }
        
        public static string GetRandomUserEmail()
        {
            return "ParseTestUser_" + random.Next(0, 999999) + "@gmail.com";
        }

        private readonly Random _rng = new Random();
        private const string _chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        private string RandomString(int size)
        {
            char[] buffer = new char[size];

            for (int i = 0; i < size; i++)
            {
                buffer[i] = _chars[_rng.Next(_chars.Length)];
            }
            return new string(buffer);
        }
       
        public static void InitParse()
        {
            ParseClient.Initialize("8NPGAbZDxZkQ4yn1DELxENrbA1907dpIKF1rytEJ", "QQxHLn27wSjUGn67sO6HwTwcA5EqbmXPDHt7WH2g");
            ParseObject.RegisterSubclass<GameUser>();
        }

        public static GameUser User { get { return (GameUser)ParseUser.CurrentUser; } }    
    }
}
