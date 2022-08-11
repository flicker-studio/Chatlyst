using System;
using AVG.Runtime.Controller;

namespace AVG.Runtime.Element
{
    /// <summary>
    /// element management interface：basic
    /// </summary>
    public interface IElementManager : IBasicService
    {
        event Action<string> OnElementAdd;
        event Action<string> OnElementRemove;

        bool HaveElement(string elementId);
        IElement GetElement(string elementId);

        void RemoveElement(string elementId);
        void ClearElement();
    }

    /// <summary>
    ///  element management interface：generics
    /// </summary>
    /// <typeparam name="TElement"> specific element type</typeparam>
    public interface IElementManager<out TElement> : IElementManager
        where TElement : IElement
    {
        new TElement GetElement(string elementId);
    }
}