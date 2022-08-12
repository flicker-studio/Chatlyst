using System;
using System.Threading.Tasks;

namespace AVG.Runtime.Element.View
{
    public class ViewManager : IViewManager
    {
        public event Action<string> OnElementAdd;
        public event Action<string> OnElementRemove;

        //TODO:link the initialize to [Runtime]
        public Task InitializeAsync()
        {
            return Task.CompletedTask;
        }


        public void Destroy()
        {
            throw new NotImplementedException();
        }


        public bool HaveElement(string elementId)
        {
            throw new NotImplementedException();
        }

        public IViewElement GetElement(string elementId)
        {
            throw new NotImplementedException();
        }

        IElement IElementManager.GetElement(string elementId)
        {
            return GetElement(elementId);
        }

        public void RemoveElement(string elementId)
        {
            throw new NotImplementedException();
        }

        public void ClearElement()
        {
            throw new NotImplementedException();
        }
    }
}