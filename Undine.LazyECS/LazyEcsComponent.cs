using LazyECS.Component;
using System;
using System.Collections.Generic;
using System.Text;

namespace Undine.LazyECS
{
    public class LazyEcsComponent<T> : IComponentData where T : struct
    {
        public T Value;
    }
}