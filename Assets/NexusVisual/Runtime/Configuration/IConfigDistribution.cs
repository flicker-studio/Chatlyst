using System;

namespace NexusVisual.Runtime.Configuration
{
    public interface IConfigDistribution
    {
        /// <summary>
        /// return a service config 
        /// </summary>
        /// <param name="type">service type</param>
        /// <returns>a instanced type config</returns>
        public Config GetConfig(Type type);
    }
}