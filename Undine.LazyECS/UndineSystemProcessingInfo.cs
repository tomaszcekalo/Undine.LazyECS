using System;
using System.Collections.Generic;
using System.Text;
using LazyECS.Models;
using LazyECS.System;
using LazyECS;

namespace Undine.LazyECS
{
    public class UndineSystemProcessingInfo
    {
        public LazyEcsSystem SystemProcessing { get; set; }

        public List<ComponentInfo> NeededComponents { get; set; } = new List<ComponentInfo>();

        public List<Entity> AttachedEntity { get; set; } = new List<Entity>();
    }
}