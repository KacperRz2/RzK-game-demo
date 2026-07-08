using UnityEngine;
using Unity.Entities;
using Unity.NetCode;

public struct CubeInput : IInputComponentData
{
    public int Horizontal;
    public int Vertical;
}

[DisallowMultipleComponent]
public class CubeInputAuthoring : MonoBehaviour
{
    class CubeInputBaking : Baker<CubeInputAuthoring>
    {
        public override void Bake(CubeInputAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);
            AddComponent<CubeInput>(entity);
        }
    }
}

[UpdateInGroup(typeof(GhostInputSystemGroup))]
public partial class SampleCubeInput : SystemBase
{
    private InputSystem_Actions _inputActions;
    protected override void OnCreate()
    {
        RequireForUpdate<NetworkStreamInGame>();
        RequireForUpdate<CubeSpawner>();
        _inputActions = new InputSystem_Actions();
        _inputActions.Player.Enable();
    }
    protected override void OnDestroy()
    {
        _inputActions.Player.Disable();
        _inputActions.Dispose();
    }
    protected override void OnUpdate()
    {
        Vector2 moveInput = _inputActions.Player.Move.ReadValue<Vector2>();

        foreach (var playerInput in SystemAPI.Query<RefRW<CubeInput>>().WithAll<GhostOwnerIsLocal>())
        {
            playerInput.ValueRW.Horizontal = Mathf.RoundToInt(moveInput.x);
            playerInput.ValueRW.Vertical = Mathf.RoundToInt(moveInput.y);
        }
    }
}
