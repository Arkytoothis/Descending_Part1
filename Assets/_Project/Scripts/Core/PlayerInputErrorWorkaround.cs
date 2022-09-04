using UnityEngine;
using UnityEngine.InputSystem;

namespace Descending.Core
{
    public class PlayerInputErrorWorkaround : MonoBehaviour
    {
        private PlayerInput Input;

        private void Start()
        {
            Input = GetComponent<PlayerInput>();
        }

        private void OnDisable()
        {
            Input.actions = null;
        }
    }
}