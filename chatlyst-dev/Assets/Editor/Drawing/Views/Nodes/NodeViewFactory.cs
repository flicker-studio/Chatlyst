using System;
using System.Reflection;
using Chatlyst.Runtime.Data;
using UnityEngine;

namespace Chatlyst.Editor
{
    public static class NodeViewFactory
    {
        private static readonly Assembly NodeViewAssembly = typeof(NodeView).Assembly;

        /// <summary>
        ///     Create a new node view.
        /// </summary>
        /// <param name="nodeRect">The rect of view</param>
        /// <param name="typeName">View Type</param>
        /// <returns>Instanced view</returns>
        /// <exception cref="Exception">Input type is not a node view.</exception>
        public static NodeView CreatNewNodeView(Rect nodeRect, string typeName)
        {
            object instance = NodeViewAssembly.CreateInstance(typeName);
            if (instance is not NodeView newNodeViewInstance) throw new Exception("New-Instantiation failed!");
            newNodeViewInstance.BuildNewInstance(nodeRect);
            return newNodeViewInstance;
        }

        /// <summary>
        ///     Create a node view stored in file.
        /// </summary>
        /// <param name="nodeData">Data in file</param>
        /// <param name="typeName">View type</param>
        /// <returns>Instanced view</returns>
        /// <exception cref="Exception">Input type is not a node view.</exception>
        public static NodeView RebuildOldNodeView(BasicNode nodeData, string typeName)
        {
            object instance = NodeViewAssembly.CreateInstance(typeName);
            if (instance is not NodeView reNodeViewInstance) throw new Exception("Re-instantiation failed!");
            reNodeViewInstance.RebuildInstance(nodeData);
            return reNodeViewInstance;
        }
    }
}
