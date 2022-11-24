using UnityEngine;

namespace NexusVisual.Runtime
{
    //TODO:inheritor and override ElementManager class(maybe need a abstract class)
    public class ViewManager : ElementManager<ViewElement>
    {
        public ViewManager()
        {
            Debug.Log("Creat");
        }
    }
}