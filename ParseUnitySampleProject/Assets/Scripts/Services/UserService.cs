using Assets.Libraries.Unity_Parse_Helpers.Scripts;
using Parse;
using ParseUnitySampleCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Services
{
    public class UserService : MonoBehaviour
    {
        void Awake()
        {
            if (Loom.Instance == null) Loom.Init();
            ParseObject.RegisterSubclass<GameUser>();
        }

        public Task<GameUser> Login(string email, string password)
        {
            Debug.Log("Logging in..");

            return ParseUser.LogInAsync(email, password)
                .OnMainThread()
                .DebugLog()
                .Then(t => Task.FromResult((GameUser)t.Result));
        }

        public Task Signup(string email, string password, string playerName)
        {
            Debug.Log("Signing up..");

            var user = new GameUser
            {
                Email = email,
                Username = email,
                Password = password,
                PlayerName = playerName
            };

            return user.SignUpAsync()
                .OnMainThread();
        }

        public void Logout()
        {
            ParseUser.LogOut();
        }
    }
}
