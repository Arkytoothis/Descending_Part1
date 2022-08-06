using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Descending.Party
{
    public class MinimapController : MonoBehaviour
    {
        [SerializeField] private Transform _followTransform = null;

        private void Update()
        {
            transform.position = _followTransform.position;
        }
    }
}