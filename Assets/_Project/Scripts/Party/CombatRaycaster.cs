using System;
using System.Collections;
using System.Collections.Generic;
using Descending.Core;
using Descending.Enemies;
using Descending.Interactables;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Descending.Party
{
    public class CombatRaycaster : MonoBehaviour
    {
        [SerializeField] private LayerMask _enemyMask = new LayerMask();
        [SerializeField] private Image _crosshair = null;
        [SerializeField] private float _enemyDistance = 10f;
        [SerializeField] private List<Texture2D> _cursorTextures = null;
        [SerializeField] private List<Sprite> _crosshairSprites = null;

        private bool _raycastEnabled = true;
        private Camera _camera = null;
        
        private void Start()
        {
            _camera = Camera.main;
        }
        
        private void Update()
        {
            if (_raycastEnabled == false) return;
            if (EventSystem.current.IsPointerOverGameObject() == true)
            {
                Cursor.SetCursor(_cursorTextures[(int)CursorTypes.Gui], Vector2.zero, CursorMode.Auto);
                return;
            }
            
            if (Utilities.IsMouseInWindow() == false)
            {
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                return;
            }

            if (RaycastForEnemy(GetRay()) == true) return;

            Cursor.SetCursor(_cursorTextures[(int)CursorTypes.Gui], Vector2.zero, CursorMode.Auto);
            _crosshair.sprite = _crosshairSprites[(int) CrosshairTypes.Default];
        }
        
        bool RaycastForEnemy(Ray ray)
        {
            if (Physics.Raycast(ray, out RaycastHit hit, _enemyDistance, _enemyMask))
            {
                Enemy enemy = hit.collider.gameObject.GetComponent<Enemy>();

                if (enemy != null)
                {
                    Cursor.SetCursor(_cursorTextures[(int) CursorTypes.Enemy], Vector2.zero, CursorMode.Auto);
                    _crosshair.sprite = _crosshairSprites[(int) CrosshairTypes.Enemy];
                    return true;
                }
            }

            return false;
        }

        private Ray GetRay()
        {
            return _camera.ScreenPointToRay(Input.mousePosition);
        }

        public void EnableRaycast()
        {
            _raycastEnabled = true;
        }

        public void DisableRaycast()
        {
            _raycastEnabled = false;
        }

        public void SetCrosshairActive(bool active)
        {
            _crosshair.enabled = active;
        }
    }
}