using System;
using System.Collections.Generic;
using System.Text;
using LazyECS.Component;
using LazyECS;

namespace Undine.LazyECS
{
    public class UndineEntityManager : EntityManager
    {
        private readonly UndineEcsManager ecsManager;
        private readonly Stack<(Entity e, IComponentData c, Type t)> addEventComponentInformation = new Stack<(Entity, IComponentData, Type)>();
        private readonly Stack<(Entity e, Type t)> removeEventComponentInformation = new Stack<(Entity, Type)>();

        public UndineEntityManager(UndineEcsManager undineEcsManager)
            : base(null)
        {
            this.ecsManager = undineEcsManager;
        }

        public void AddComponent(Entity entity, IComponentData componentData)
        {
            addEventComponentInformation.Push((entity, componentData, componentData.GetType()));
        }

        public void RemoveComponent(Entity entity, Type type)
        {
            removeEventComponentInformation.Push((entity, type));
        }

        public void RemoveComponent<T>(Entity entity)
        {
            RemoveComponent(entity, typeof(T));
        }

        public void ExecuteEvents()
        {
            if (addEventComponentInformation.Count + removeEventComponentInformation.Count == 0)
            {
                return;
            }

            var components = new HashSet<Type>();

            while (addEventComponentInformation.Count > 0)
            {
                var eventInfo = addEventComponentInformation.Pop();
                ecsManager.EntityContainer.AddComponent(eventInfo.e, eventInfo.c);
                ecsManager.BindComponentTo(eventInfo.e, eventInfo.c, false);
                components.Add(eventInfo.t);
            }

            while (removeEventComponentInformation.Count > 0)
            {
                var eventInfo = removeEventComponentInformation.Pop();
                ecsManager.EntityContainer.RemoveComponent(eventInfo.e, eventInfo.t);
                ecsManager.UnbindComponent(eventInfo.e, eventInfo.t, false);
                components.Add(eventInfo.t);
            }

            foreach (var component in components)
                ecsManager.ReattachComponents(component);
        }
    }
}