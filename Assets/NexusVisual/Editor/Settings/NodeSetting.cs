using UnityEngine;
using UnityEngine.UIElements;

namespace NexusVisual.Editor
{
    [CreateAssetMenu(fileName = "NodeSetting", menuName = "Assets/NodeSetting")]
    public class NodeSetting : ScriptableObject
    {
        [SerializeField] public VisualTreeAsset startNode;
        [SerializeField] public VisualTreeAsset dialogueNode;
    }
}