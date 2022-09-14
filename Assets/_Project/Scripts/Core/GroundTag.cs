using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Descending.Core
{
    public enum GroundTags { None, Sand, Grass, Stone, Mud, Wood, Gravel, Metal, Number}
    public class GroundTag : MonoBehaviour
    {
        public GroundTags Tag = GroundTags.None;
    }
}