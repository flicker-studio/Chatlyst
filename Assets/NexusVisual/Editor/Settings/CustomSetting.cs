using UnityEngine;

namespace NexusVisual.Editor
{
    [CreateAssetMenu(fileName = "CustomSetting", menuName = "Assets/CustomSetting")]
    public class CustomSetting : ScriptableObject
    {
        [SerializeField] public NodeSetting nodeSetting;
    }
}