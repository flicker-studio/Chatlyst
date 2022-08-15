using AVG.Runtime.Configuration;
using UnityEngine;

namespace AVG.Runtime.Element.View
{
    [CreateAssetMenu(fileName = "View Config", menuName = "AVG/Configuration/View", order = 1)]
    public class ViewConfig : Config

    {
        public Texture renderTexture;
    }
}