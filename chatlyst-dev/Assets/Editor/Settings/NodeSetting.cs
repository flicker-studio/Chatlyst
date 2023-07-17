using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

namespace Chatlyst.Editor
{
    [CreateAssetMenu(fileName = "NodeSetting", menuName = "Assets/NodeSetting")]
    public class NodeSetting : ScriptableObject
    {
        [SerializeField] public VisualTreeAsset startNode;
        [SerializeField] public VisualTreeAsset dialogueNode;
        [FormerlySerializedAs("selectNode")] [SerializeField] public VisualTreeAsset choiceNode;
        [SerializeField] public VisualTreeAsset dialogueInspector;
    }
}