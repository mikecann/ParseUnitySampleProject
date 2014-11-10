using Assets.Scripts.Services;
using Parse;
using ParseUnitySampleCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Menus
{
    public class LoggedInState : MonoBehaviour
    {
        public Button logoutButton;
        public Text title;

        private UserService userService;
        private MenusController menus;
        private GameUser user;

        void Awake()
        {
            menus = GetComponentInParent<MenusController>();
            userService = FindObjectOfType<UserService>();
            logoutButton.onClick.AddListener(Logout);
        }

        void OnEnable()
        {
            user = (GameUser)ParseUser.CurrentUser;
            title.text = "Welcome " + user.PlayerName;
        }

        private void Logout()
        {
            userService.Logout();
            menus.States.SetState("Login State");
        }
    }
}
