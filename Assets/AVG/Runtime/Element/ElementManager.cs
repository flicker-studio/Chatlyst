using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace AVG.Runtime
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

        private readonly Dictionary<string, TElement> m_ManagedElementsList;
        private readonly Dictionary<string, UniTaskCompletionSource<TElement>> m_AddTasksCache;

        public virtual UniTask InitializeAsync()
        {
            //TODO:init action
            return UniTask.CompletedTask;
        }

        protected ElementManager()
        {
            m_ManagedElementsList = new Dictionary<string, TElement>(StringComparer.Ordinal);
            m_AddTasksCache = new Dictionary<string, UniTaskCompletionSource<TElement>>();
        }

        public bool HaveElement(string elementId) =>
            !string.IsNullOrEmpty(elementId) && m_ManagedElementsList.ContainsKey(elementId);

        public async UniTask<TElement> AddElementAsync(string elementId)
        {
            //element already exists in list
            if (HaveElement(elementId))
            {
                Debug.LogWarning($"'{elementId}' is already exists.");
                return GetElement(elementId);
            }

            //element already exists in Cache
            if (m_AddTasksCache.ContainsKey(elementId))
                return await m_AddTasksCache[elementId].Task;

            m_AddTasksCache[elementId] = new UniTaskCompletionSource<TElement>();

            var newElement = await ConstructElementAsync(elementId);
            m_ManagedElementsList.Add(elementId, newElement);

            m_AddTasksCache[elementId].TrySetResult(newElement);

            m_AddTasksCache.Remove(elementId);
            OnElementAdd?.Invoke(elementId);
            return newElement;
        }

        async UniTask<IElement> IElementManager.AddElementAsync(string elementId) =>
            await AddElementAsync(elementId);

        public TElement GetElement(string elementId)
        {
            if (!HaveElement(elementId))
                throw new Exception($"Can't find '{elementId}' element.");

            return m_ManagedElementsList[elementId];
        }

        IElement IElementManager.GetElement(string elementId) =>
            GetElement(elementId);

        public IReadOnlyCollection<TElement> ReturnElements()
        {
            return m_ManagedElementsList?.Values;
        }


        IReadOnlyCollection<IElement> IElementManager.ReturnElements() =>
            ReturnElements().Cast<IElement>().ToArray();

        public void RemoveElement(string elementId)
        {
            if (!HaveElement(elementId)) return;

            var element = GetElement(elementId);
            m_ManagedElementsList.Remove(element.Id);
            //GC code:not using
            //(element as IDisposable)?.Dispose();

            OnElementRemove?.Invoke(elementId);
        }

        public void ClearElement()
        {
            if (m_ManagedElementsList.Count == 0) return;
            TElement[] managedElements = ReturnElements().ToArray();

            foreach (var element in managedElements)
                RemoveElement(element.Id);

            m_ManagedElementsList.Clear();
        }

        public void Destroy()
        {
            ClearElement();
        }

        private async UniTask<TElement> ConstructElementAsync(string elementId)
        {
            //TODO：managed element
            throw new NotImplementedException();
        }
    }
}