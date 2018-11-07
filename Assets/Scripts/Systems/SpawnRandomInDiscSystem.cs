using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Samples.Common
{
    public class SpawnRandomInDiscSystem : ComponentSystem
    {
        struct SpawnRandomInDiscInstance
        {
            public int spawnerIndex;
            public Entity sourceEntity;
            public float3 position;
#pragma warning disable 649
            public float radius;
#pragma warning restore 649
        }

        ComponentGroup m_MainGroup;

        protected override void OnCreateManager()
        {
            m_MainGroup = GetComponentGroup(typeof(SpawnRandomInDisc), typeof(Position));
        }

        protected override void OnUpdate()
        {
            var uniqueTypes = new List<SpawnRandomInDisc>(10);

            EntityManager.GetAllUniqueSharedComponentData(uniqueTypes);

            int spawnInstanceCount = 0;
            for (int sharedIndex = 0; sharedIndex != uniqueTypes.Count; sharedIndex++)
            {
                var spawner = uniqueTypes[sharedIndex];
                m_MainGroup.SetFilter(spawner);
                var entities = m_MainGroup.GetEntityArray();
                spawnInstanceCount += entities.Length;
            }

            if (spawnInstanceCount == 0)
                return;

            var spawnInstances = new NativeArray<SpawnRandomInDiscInstance>(spawnInstanceCount, Allocator.Temp);
            {
                int spawnIndex = 0;
                for (int sharedIndex = 0; sharedIndex != uniqueTypes.Count; sharedIndex++)
                {
                    var spawner = uniqueTypes[sharedIndex];
                    m_MainGroup.SetFilter(spawner);
                    var entities = m_MainGroup.GetEntityArray();
                    var positions = m_MainGroup.GetComponentDataArray<Position>();

                    for (int entityIndex = 0; entityIndex < entities.Length; entityIndex++)
                    {
                        var spawnInstance = new SpawnRandomInDiscInstance();

                        spawnInstance.sourceEntity = entities[entityIndex];
                        spawnInstance.spawnerIndex = sharedIndex;
                        spawnInstance.position = positions[entityIndex].Value;

                        spawnInstances[spawnIndex] = spawnInstance;
                        spawnIndex++;
                    }
                }
            }

            for (int spawnIndex = 0; spawnIndex < spawnInstances.Length; spawnIndex++)
            {
                int spawnerIndex = spawnInstances[spawnIndex].spawnerIndex;
                var spawner = uniqueTypes[spawnerIndex];
                int count = spawner.count;
                var entities = new NativeArray<Entity>(count, Allocator.Temp);
                var prefab = spawner.prefab;
                float radius = spawner.radius;
                float discWidth = spawner.discWidth;
                float discHeight = spawner.discHeight;
                var spawnPositions = new NativeArray<float3>(count, Allocator.Temp);
                float3 center = spawnInstances[spawnIndex].position;
                var sourceEntity = spawnInstances[spawnIndex].sourceEntity;

                RandomPointsInDisc(center, radius, discWidth, discHeight, ref spawnPositions);

                EntityManager.Instantiate(prefab, entities);

                for (int i = 0; i < count; i++)
                {
                    var position = new Position
                    {
                        Value = spawnPositions[i]
                    };
                    EntityManager.SetComponentData(entities[i], position);
                }

                EntityManager.RemoveComponent<SpawnRandomInDisc>(sourceEntity);

                spawnPositions.Dispose();
                entities.Dispose();
            }
            spawnInstances.Dispose();
        }

        static public void RandomPointsInDisc(float3 center, float radius, float width, float height, ref NativeArray<float3> points)
        {
            var radiusSquared = radius * radius;
            var pointsFound = 0;
            var count = points.Length;
            float padding = width;
            while (pointsFound < count)
            {
                float angle = UnityEngine.Random.Range(180, 360) * Mathf.Deg2Rad;
                float paddedRadius = radius + UnityEngine.Random.Range(-padding, padding);
                var p = new float3
                {
                    x = center.x + paddedRadius * Mathf.Sin(angle),
                    y = UnityEngine.Random.Range(-height, height),
                    z = center.z + paddedRadius * Mathf.Cos(angle)
                };

                points[pointsFound] = p;

                pointsFound++;
            }
        }
    }
}
