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
        /// 
        /// </summary>
        /// <param name="nodeRect"></param>
        /// <param name="typeName"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static NodeView CreatNewNodeView(Rect nodeRect, string typeName)
        {
            object instance = NodeViewAssembly.CreateInstance(typeName);
            if (instance is not NodeView newNodeViewInstance) throw new Exception("New-Instantiation failed!");
            newNodeViewInstance.BuildNewInstance(nodeRect);
            return newNodeViewInstance;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nodeData"></param>
        /// <param name="typeName"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static NodeView RebuildOldNodeView(BasicNode nodeData, string typeName)
        {
            object instance = NodeViewAssembly.CreateInstance(typeName);
            if (instance is not NodeView reNodeViewInstance) throw new Exception("Re-instantiation failed!");
            reNodeViewInstance.RebuildInstance(nodeData);
            return reNodeViewInstance;
        }
    }
}
