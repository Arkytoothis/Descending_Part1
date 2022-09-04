using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Descending.Enemies
{
    public class TargetController : MonoBehaviour
    {
        [SerializeField] private GameObject _target = null;

        public GameObject Target => _target;

        public void SetTarget(GameObject target)
        {
            _target = target;
        }
    }
}