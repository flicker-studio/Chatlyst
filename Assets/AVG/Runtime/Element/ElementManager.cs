using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace AVG.Runtime.Element
{
    /// <summary>
    /// Base element manager template
    /// </summary>
    /// <typeparam name="TElement">element type</typeparam>
    public class ElementManager<TElement> : IElementManager<TElement>
        where TElement : IElement
    {
        public event Action<string> OnElementAdd;
        public event Action<string> OnElementRemove;
        
        private Dictionary<string, TElement> _managedElementsList;
        private readonly Dictionary<string, TaskCompletionSource<TElement>> _addTasksCache;

        public Task InitializeAsync()
        {
            //TODO:init action
            return Task.CompletedTask;
        }

        public bool HaveElement(string elementId) =>
            !string.IsNullOrEmpty(elementId) && _managedElementsList.ContainsKey(elementId);

        public async Task<TElement> AddElementAsync(string elementId)
        {
            //element already exists in list
            if (HaveElement(elementId))
            {
                Debug.LogWarning($"'{elementId}' is already exists.");
                return GetElement(elementId);
            }

            //element already exists in Cache
            if (_addTasksCache.ContainsKey(elementId))
                return await _addTasksCache[elementId].Task;

            _addTasksCache[elementId] = new TaskCompletionSource<TElement>();

            var newElement = await ConstructElementAsync(elementId);
            _managedElementsList.Add(elementId, newElement);

            _addTasksCache[elementId].TrySetResult(newElement);

            _addTasksCache.Remove(elementId);
            OnElementAdd?.Invoke(elementId);
            return newElement;
        }

        async Task<IElement> IElementManager.AddElementAsync(string elementId) =>
            await AddElementAsync(elementId);

        public TElement GetElement(string elementId)
        {
            if (!HaveElement(elementId))
                throw new Exception($"Can't find '{elementId}' element.");

            return _managedElementsList[elementId];
        }

        IElement IElementManager.GetElement(string elementId) =>
            GetElement(elementId);

        public IReadOnlyCollection<TElement> ReturnElements() =>
            _managedElementsList?.Values;

        IReadOnlyCollection<IElement> IElementManager.ReturnElements() =>
            ReturnElements().Cast<IElement>().ToArray();

        public void RemoveElement(string elementId)
        {
            if (!HaveElement(elementId)) return;

            var element = GetElement(elementId);
            _managedElementsList.Remove(element.Id);
            //GC code:not using
            //(element as IDisposable)?.Dispose();

            OnElementRemove?.Invoke(elementId);
        }

        public void ClearElement()
        {
            if (_managedElementsList.Count == 0) return;
            TElement[] managedElements = ReturnElements().ToArray();

            foreach (var element in managedElements)
                RemoveElement(element.Id);

            _managedElementsList.Clear();
        }

        public void Destroy()
        {
            ClearElement();
        }

        private async Task<TElement> ConstructElementAsync(string elementId)
        {
            //TODO:turn config to element
            var newElement = default(TElement);
            await newElement?.InitializeAsync();
            return newElement;
        }
    }
}