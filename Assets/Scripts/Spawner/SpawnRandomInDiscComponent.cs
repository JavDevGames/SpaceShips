using System;
using Unity.Entities;
using UnityEngine;

namespace Samples.Common
{
    [Serializable]
    public struct SpawnRandomInDisc : ISharedComponentData
    {
        public GameObject prefab;
        public float radius;
        public int count;
        public float discWidth;
        public float discHeight;
    }

    public class SpawnRandomInDiscComponent : SharedComponentDataWrapper<SpawnRandomInDisc> { }
}
