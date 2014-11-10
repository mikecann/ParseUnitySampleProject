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
    public class SignupState : MonoBehaviour
    {
        public Button signupButton;
        public Button backButton;
        public InputField usernameInp;
        public InputField passwordInp;
        public InputField playernameInp;

        private MenusController menus;
        private UserService userService;
        private bool isLoading;

        void Awake()
        {
            menus = GetComponentInParent<MenusController>();
            userService = FindObjectOfType<UserService>();
            signupButton.onClick.AddListener(Signup);
            backButton.onClick.AddListener(Back);
        }

        private void Signup()
        {
            isLoading = true;

            // Signup
            userService.Signup(usernameInp.text, passwordInp.text, playernameInp.text)

                // Then Login
                .Then(t => userService.Login(usernameInp.text, passwordInp.text))

                // Then we are done
                .Then(OnLoggedIn, OnError);                     
        }

        private void OnError(Exception e)
        {
            isLoading = false;
            Debug.LogException(e);
            menus.ErrorPopup.Open("Error signing up!");
        }

        private void OnLoggedIn(GameUser user)
        {
            isLoading = false;
            menus.States.SetState("Logged In State");
        }

        private void Back()
        {
            menus.States.SetState("Login State");
        }

        void Update()
        {
            signupButton.interactable = !isLoading;
            backButton.interactable = !isLoading;
            usernameInp.interactable = !isLoading;
            passwordInp.interactable = !isLoading;
            playernameInp.interactable = !isLoading;
        }
    }
}
