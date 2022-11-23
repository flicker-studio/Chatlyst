using AVG.Runtime.Configuration;
using UnityEngine;

namespace AVG.Runtime
{
    [CreateAssetMenu(fileName = "View Config", menuName = "AVG/Configuration/View", order = 1)]
    public class ViewConfig : Config

    {
        public Texture renderTexture;
    }
}