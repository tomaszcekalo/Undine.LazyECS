using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Undine.Core;

namespace Undine.LazyECS
{
    public sealed class LazyEcsSystemMiddlewareSingleton
    {
        private LazyEcsSystemMiddlewareSingleton()
        { }

        private static LazyEcsSystemMiddlewareSingleton _instance;

        public static LazyEcsSystemMiddlewareSingleton GetInstance()
        {
            if (_instance == null)
            {
                _instance = new LazyEcsSystemMiddlewareSingleton();
            }
            return _instance;
        }

        private Dictionary<Type, UnifiedSystem> Systems { get; set; } = new Dictionary<Type, UnifiedSystem>();

        public void AddSystem(UnifiedSystem system, Type type)
        //where A : struct
        {
            this.Systems.Add(type, system);
        }

        internal UnifiedSystem GetSystem(Type type)
        {
            var keys = Systems.Where(x => x.Key.IsAssignableFrom(type));
            return Systems[type];
        }
    }
}