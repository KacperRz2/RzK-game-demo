using Unity.NetCode;
using Unity.Rendering;

[GhostComponentVariation(typeof(URPMaterialPropertyBaseColor), "URP Base Color Network Variant")]
[GhostComponent]
public struct URPMaterialPropertyBaseColorVariant
{
    [GhostField]
    public Unity.Mathematics.float4 Value;
}
