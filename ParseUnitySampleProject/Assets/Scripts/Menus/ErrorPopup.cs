using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Menus
{
    public class ErrorPopup : MonoBehaviour
    {
        public Button closeButton;
        public Text errorMessageText;

        private MenusController menus;

        void Awake()
        {
            menus = GetComponentInParent<MenusController>();
            closeButton.onClick.AddListener(Close);
        }

        public void Open(string error)
        {
            errorMessageText.text = error;
            gameObject.SetActive(true);
        }

        private void Close()
        {
            gameObject.SetActive(false);
        }
    }
}
