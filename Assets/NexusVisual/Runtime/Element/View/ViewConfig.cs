using NexusVisual.Runtime.Configuration;
using UnityEngine;

namespace NexusVisual.Runtime
{
    [CreateAssetMenu(fileName = "View Config", menuName = "AVG/Configuration/View", order = 1)]
    public class ViewConfig : Config

    {
        public Texture renderTexture;
    }
}