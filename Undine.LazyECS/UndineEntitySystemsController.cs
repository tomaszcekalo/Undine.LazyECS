using System;
using System.Collections.Generic;
using System.Text;
using LazyECS.Models;

namespace Undine.LazyECS
{
    public class UndineEntitySystemsController
    {
        internal readonly Dictionary<Type, UndineSystemProcessingInfo> SystemInfos;
        private readonly UndineEntityManager entityManager;

        public UndineEntitySystemsController(Dictionary<Type, UndineSystemProcessingInfo> systemInfos, UndineEntityManager entityManager)
        {
            SystemInfos = systemInfos;
            this.entityManager = entityManager;
        }

        public void OnUpdate()
        {
            entityManager.ExecuteEvents();

            foreach (var processing in SystemInfos)
            {
                var processingInfo = processing.Value;
                if (processing.Value.AttachedEntity.Count > 0)
                    processingInfo.SystemProcessing.Execute();
            }
        }
    }
}