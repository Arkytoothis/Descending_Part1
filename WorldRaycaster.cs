using System;
using System.Collections;
using System.Collections.Generic;
using Descending.Abilities;
using Descending.Characters;
using Descending.Core;
using Descending.Enemies;
using Descending.Gui;
using Descending.Interactables;
using Descending.Player;
using ScriptableObjectArchitecture;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Descending.Party
{
    public enum RaycastModes { World, Target, Number, None }
    
    public class WorldRaycaster : MonoBehaviour
    {
        [SerializeField] private LayerMask _interactableMask = new LayerMask();
        [SerializeField] private LayerMask _enemyMask = new LayerMask();
        [SerializeField] private Image _crosshair = null;
        [SerializeField] private float _interactableDistance = 10f;
        [SerializeField] private float _enemyDistance = 10f;
        [SerializeField] private List<Texture2D> _cursorTextures = null;
        [SerializeField] private List<Sprite> _crosshairSprites = null;
        [SerializeField] private AttackController _attackController = null;
        [SerializeField] private PartyManager _partyManager = null;

        [SerializeField] private BoolEvent onSetCanClickHeroWidgets = null;
        
        private bool _raycastEnabled = true;
        private Camera _camera = null;
        private RaycastModes _mode = RaycastModes.None;
        private Ability _currentAbility = null;
        private HeroWidget _currentHeroWidget = null;
        private Vector3 _projectileDestination;
        
        private void Start()
        {
            _camera = Camera.main;
            _mode = RaycastModes.World;
        }

        private void Update()
        {
            if (_raycastEnabled == false) return;

            if (_mode == RaycastModes.World)
            {
                if (Input.GetMouseButtonUp(1))
                {
                    ShootProjectile();
                }
                
                if (RaycastForInteractable(GetRay()) == true) return;
                if (RaycastForEnemy(GetRay()) == true) return;

                Cursor.SetCursor(_cursorTextures[(int) CursorTypes.Gui], Vector2.zero, CursorMode.Auto);
                _crosshair.sprite = _crosshairSprites[(int) CrosshairTypes.Default];
            }
            else if(_mode == RaycastModes.Target)
            {
                Cursor.SetCursor(_cursorTextures[(int) CursorTypes.Ability_Invalid], Vector2.zero, CursorMode.Auto);

                if (Input.GetMouseButtonUp(1))
                {
                    SetWorldMode();
                    return;
                }
                
                if (RaycastForHeroWidget(GetRay()) == true) return;
                if (RaycastForEnemyTarget(GetRay()) == true) return;
            }

            if (EventSystem.current.IsPointerOverGameObject() == true)
            {
                Cursor.SetCursor(_cursorTextures[(int) CursorTypes.Gui], Vector2.zero, CursorMode.Auto);
                return;
            }
            
            if (Utilities.IsMouseInWindow() == false)
            {
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                return;
            }
        }

        bool RaycastForInteractable(Ray ray)
        {
            if (Physics.Raycast(ray, out RaycastHit hit, _interactableDistance, _interactableMask))
            {
                Interactable interactable = hit.collider.gameObject.GetComponentInParent<Interactable>();

                if (interactable != null)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        interactable.Interact();
                    }

                    Cursor.SetCursor(_cursorTextures[(int) CursorTypes.Interact], Vector2.zero, CursorMode.Auto);
                    _crosshair.sprite = _crosshairSprites[(int) CrosshairTypes.Interact];
                    return true;
                }
            }

            return false;
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

                    if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.LeftControl)) && _attackController.CanAttack == true)
                    {
                        _attackController.ProcessLeftClick(hit.point, enemy);
                    }

                    return true;
                }
            }

            return false;
        }

        bool RaycastForEnemyTarget(Ray ray)
        {
            if (_currentAbility == null) return false;
            
            if (_currentAbility.Definition.Details.TargetType == TargetTypes.Enemy && Physics.Raycast(ray, out RaycastHit hit, 500f))
            {
                Enemy enemy = hit.collider.gameObject.GetComponent<Enemy>();

                if (enemy != null)
                {
                    Cursor.SetCursor(_cursorTextures[(int) CursorTypes.Ability_Valid], Vector2.zero, CursorMode.Auto);

                    if (Input.GetMouseButtonDown(0))
                    {
                        AbilityProcessor.ProcessAbility(_partyManager.GetCurrentHero(), new List<GameEntity>() { enemy }, _currentAbility, _attackController.ProjectileSpawnPoint);
                        SetWorldMode();
                    }

                    return true;
                }
            }

            return false;
        }
        
        bool RaycastForHeroWidget(Ray ray)
        {
            if (_currentAbility == null) return false;
            
            if (_currentAbility.Definition.Details.TargetType == TargetTypes.Friend && _currentHeroWidget != null)
            {
                Cursor.SetCursor(_cursorTextures[(int) CursorTypes.Ability_Valid], Vector2.zero, CursorMode.Auto);

                if (Input.GetMouseButtonUp(0))
                {
                    AbilityProcessor.ProcessAbility(_partyManager.GetCurrentHero(), new List<GameEntity>() { _currentHeroWidget.Hero }, _currentAbility, _attackController.ProjectileSpawnPoint);
                    SetWorldMode();
                }
                
                return true;
            }

            return false;
        }

        private Ray GetRay()
        {
            return _camera.ScreenPointToRay(Input.mousePosition);
        }

        public void SetCrosshairActive(bool active)
        {
            _crosshair.enabled = active;
        }

        public void SetAbility(Ability ability)
        {
            _currentAbility = ability;
            _mode = RaycastModes.Target;
            onSetCanClickHeroWidgets.Invoke(false);
        }

        private void SetWorldMode()
        {
            _currentAbility = null;
            _mode = RaycastModes.World;
            onSetCanClickHeroWidgets.Invoke(true);
        }

        public void OnSetCurrentHeroWidget(HeroWidget heroWidget)
        {
            _currentHeroWidget = heroWidget;
        }


        [SerializeField] private GameObject _arrowPrefab = null;
        [SerializeField] private Transform _firePoint = null;
        [SerializeField] private float _projectileSpeed = 50f;
        
        private void ShootProjectile()
        {
            Ray ray = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
                _projectileDestination = hit.point;
            else
            {
                _projectileDestination = ray.GetPoint(500f);
            }
            
            SpawnArrow(hit.point);
        }

        private void SpawnArrow(Vector3 hitPoint)
        {
            GameObject clone = Instantiate(_arrowPrefab, _firePoint.position, _firePoint.transform.rotation);
            clone.GetComponent<Rigidbody>().velocity = (_projectileDestination - _firePoint.position).normalized * _projectileSpeed;
        }
    }
}   