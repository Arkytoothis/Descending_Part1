using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjectArchitecture;
using UnityEngine;

namespace Descending.Gui
{
    public class MessageHandler : MonoBehaviour
    {
        [SerializeField] private GameMessageEvent onDisplayMessage = null;

        private static MessageHandler _instance = null;

        public static MessageHandler Instance => _instance;

        private void Awake()
        {
            if (_instance == null)
                _instance = this;
            else if(_instance != this)
            {
                Destroy(gameObject);
            }
        }

        public void DisplayMessage(GameMessage message)
        {
            onDisplayMessage.Invoke(message);
        }
    }
}