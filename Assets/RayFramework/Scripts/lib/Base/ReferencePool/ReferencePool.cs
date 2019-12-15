using System;
using System.Collections.Generic;
namespace RayFramework
{
    public static partial class ReferencePool
    {
        private static readonly IDictionary<string, ReferenceCollection> s_ReferenceCollections = new Dictionary<string, ReferenceCollection>();
    }
}
