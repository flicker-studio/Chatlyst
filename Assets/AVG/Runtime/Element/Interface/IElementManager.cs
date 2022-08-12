using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        Task<IElement> AddElementAsync(string elementId);

        IElement GetElement(string elementId);

        IReadOnlyCollection<IElement> ReturnElements();

        void RemoveElement(string elementId);
        void ClearElement();
    }

    /// <summary>
    ///  element management interface：generics
    /// </summary>
    /// <typeparam name="TElement"> specific element type</typeparam>
    public interface IElementManager<TElement> : IElementManager
        where TElement : IElement
    {
        new Task<TElement> AddElementAsync(string elementId);
        new TElement GetElement(string elementId);
        new IReadOnlyCollection<TElement> ReturnElements();
    }
}