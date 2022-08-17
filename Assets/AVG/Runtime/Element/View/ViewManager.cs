using UnityEngine;

namespace AVG.Runtime.Element.View
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