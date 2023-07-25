using System.Collections.Generic;
using Chatlyst.Runtime;
using UnityEngine;

namespace Chatlyst.Editor
{
    [CreateAssetMenu(fileName = "CustomSetting", menuName = "Assets/CustomSetting")]
    public class CustomSetting : ScriptableObject
    {
        [SerializeField] public NodeSetting nodeSetting;
        [SerializeField] public List<string> list;
    }
}
