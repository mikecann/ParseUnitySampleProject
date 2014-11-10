using Assets.Libraries.UnityHelpers.Scripts.View;
using Parse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Menus
{
    public class MenusController : MonoBehaviour
    {
        public ViewStateController States { get; private set; }
        public ErrorPopup ErrorPopup { get; private set; }

        void Awake()
        {
            States = GetComponent<ViewStateController>();
            ErrorPopup = GetComponentsInChildren<ErrorPopup>(true)[0];
        }

        void Start()
        {
            if (ParseUser.CurrentUser == null)
                States.SetState("Login State");
            else
                States.SetState("Logged In State");
        }
    }
}
