using Assets.Scripts.Services;
using ParseUnitySampleCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Menus
{
    public class LoginState : MonoBehaviour
    {
        public Button loginButton;
        public Button signupButton;
        public InputField usernameInp;
        public InputField passwordInp;

        private MenusController menus;
        private UserService userService;
        private bool isLoading;

        void Awake()
        {
            menus = GetComponentInParent<MenusController>();
            userService = FindObjectOfType<UserService>();
            loginButton.onClick.AddListener(Login);
            signupButton.onClick.AddListener(Signup);
        }

        private void Signup()
        {
            menus.States.SetState("Signup State");
        }

        private void Login()
        {
            isLoading = true;
            userService.Login(usernameInp.text, passwordInp.text)
                .Then(OnLoggedIn, OnLoginError);                
        }

        private void OnLoginError(Exception e)
        {
            isLoading = false;
            menus.ErrorPopup.Open("Error logging in!");
        }

        private void OnLoggedIn(GameUser user)
        {
            isLoading = false;        
            menus.States.SetState("Logged In State");
        }

        void Update()
        {
            signupButton.interactable = !isLoading;
            loginButton.interactable = !isLoading;
            usernameInp.interactable = !isLoading;
            passwordInp.interactable = !isLoading;
        }
    }
}
