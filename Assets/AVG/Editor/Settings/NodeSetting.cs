using UnityEngine;
using UnityEngine.UIElements;

namespace AVG.Editor
{
    [CreateAssetMenu(fileName = "NodeSetting", menuName = "Assets/NodeSetting")]
    public class NodeSetting : ScriptableObject
    {
        [SerializeField] public VisualTreeAsset startNode;
    }
}