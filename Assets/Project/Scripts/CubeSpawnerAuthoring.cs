using UnityEngine;
using Unity.Entities;

public struct CubeSpawner : IComponentData
{
    public Entity Cube;
}

[DisallowMultipleComponent]
public class CubeSpawnerAuthoring : MonoBehaviour
{
    public GameObject Cube;

    class Baker : Baker<CubeSpawnerAuthoring>
    {
        public override void Bake(CubeSpawnerAuthoring authoring)
        {
            CubeSpawner component = default;
            component.Cube = GetEntity(authoring.Cube, TransformUsageFlags.Dynamic);
            var entity = GetEntity(TransformUsageFlags.None);
            AddComponent(entity, component);
        }
    }
}
