using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using LazyECS;
using LazyECS.Component;
using LazyECS.Models;
using LazyECS.System;
using LazyECS.Tools;

namespace Undine.LazyECS
{
    public class UndineEcsManager : ECSManager
    {
        public Dictionary<Type, LazyEcsSystem> RegisteredSystemInstances { get; } = new Dictionary<Type, LazyEcsSystem>();
        public HashSet<Type> RegisteredSystemsProcessing { get; } = new HashSet<Type>();
        private UndineEntitySystemsController entitySystemsController;
        public new UndineEntityManager EntityManager { get; set; }

        public IComponentAssignCreator ComponentAssignCreator { get; }

        public UndineEcsManager(IComponentAssignCreator componentAssignCreator)
            : base(componentAssignCreator)
        {
            this.ComponentAssignCreator = componentAssignCreator;
        }

        public (LazyEcsSystem, List<ComponentInfo>) GetSystemInstance(Type type)
        {
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance)
                .Where(CheckField);

            var assignInfo = new List<ComponentInfo>();
            foreach (var fieldInfo in fields)
            {
                var expressionAssign = ComponentAssignCreator.GetExpression(type, fieldInfo);
                assignInfo.Add(
                    new ComponentInfo(fieldInfo.FieldType, expressionAssign));
            }

            if (RegisteredSystemInstances.ContainsKey(type))
            {
                return (RegisteredSystemInstances[type], assignInfo);
            }
            LazyEcsSystem instance = Activator.CreateInstance(type) as LazyEcsSystem;
            return (instance, assignInfo);
        }

        public new UndineEntitySystemsController Init()
        {
            EntityManager = new UndineEntityManager(this);

            var systemInfos = new Dictionary<Type, UndineSystemProcessingInfo>();

            foreach (var type in RegisteredSystemsProcessing)
            {
                var (instance, assignInfo) = GetSystemInstance(type);
                systemInfos.Add(type,
                    new UndineSystemProcessingInfo { SystemProcessing = instance, NeededComponents = assignInfo });
            }

            entitySystemsController = new UndineEntitySystemsController(systemInfos, EntityManager);
            return entitySystemsController;
        }

        public new void BindComponentTo(Entity entity, IComponentData componentData, bool isAttach = true)
        {
            var componentType = componentData.GetType();
            var processing = entitySystemsController.SystemInfos.Values.Where(e =>
                e.NeededComponents.Any(x => x.TypeComponent.GetElementType() == componentType));
            foreach (var processingInfo in processing)
            {
                if (processingInfo.NeededComponents.All(e =>
                    EntityContainer.HasComponent(entity, e.TypeComponent.GetElementType())))
                {
                    processingInfo.AttachedEntity.Add(entity);
                    if (isAttach)
                        AttachComponents(processingInfo, processingInfo.AttachedEntity);
                }
            }
        }

        public new void ReattachComponents(Type type)
        {
            var processing = entitySystemsController.SystemInfos.Values.Where(e =>
                e.NeededComponents.Any(x => x.TypeComponent.GetElementType() == type));
            foreach (var processingInfo in processing)
                AttachComponents(processingInfo, processingInfo.AttachedEntity);
        }

        public new void Register<T>()
            where T : ISystemProcessing
        {
            RegisteredSystemsProcessing.Add(typeof(T));
        }

        public void Register<T>(T system, bool skipProcessing = true)
            where T : LazyEcsSystem
        {
            Register<T>();
            system.SkipProcessing = skipProcessing;
            RegisteredSystemInstances.Add(system.GetType(), system);
        }

        private bool CheckField(FieldInfo fieldInfo)
        {
            var attribute = fieldInfo.GetCustomAttribute<InjectComponentAttribute>();
            return attribute != null;
        }

        public void AttachComponents(UndineSystemProcessingInfo processingInfo, List<Entity> entities)
        {
            foreach (var field in processingInfo.NeededComponents)
            {
                var components = Array.CreateInstance(field.TypeComponent.GetElementType(), entities.Count);
                for (var i = 0; i < entities.Count; i++)
                    components.SetValue(
                        EntityContainer.GetComponent(entities[i], field.TypeComponent.GetElementType()), i);
                field.AttachToSystem(processingInfo.SystemProcessing, components as IComponentData[]);
            }
        }
    }
}