using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RuntimeNavMeshBaker : MonoBehaviour
{
    [SerializeField] Bounds _bounds = new Bounds(Vector3.zero, new Vector3(10, 10, 10));
    [SerializeField] NavMeshCollectGeometry _collectGeometry = NavMeshCollectGeometry.RenderMeshes;
    int _includedLayerMask = ~0;
    int _defaultArea = 0;

    [Header(nameof(NavMeshBuildSettings))]

    [SerializeField] int _agentTypeID;// The agent type ID the NavMesh will be baked for.
    [SerializeField] float _agentRadius = 0.5f;// The radius of the agent for baking in world units.
    [SerializeField] float _agentHeight = 2;// The height of the agent for baking in world units.
    [SerializeField] float _agentSlope = 45;// The maximum slope angle which is walkable (angle in degrees).
    [SerializeField] float _agentClimb = 0.4f;// The maximum vertical step size an agent can take.
    [SerializeField] float _minRegionArea = 100;// The approximate minimum area of individual NavMesh regions.

    [SerializeField] bool _overrideVoxelSize = false;// Enables overriding the default voxel size. See Also: voxelSize.
    [SerializeField] float _voxelSize = 0.5f / 3f;// Sets the voxel size in world length units.

    [SerializeField] bool _overrideTileSize = false;// Enables overriding the default tile size. See Also: tileSize.
    [SerializeField] int _tileSize = 10;// Sets the tile size in voxel units.

    NavMeshDataInstance _activeNavmesh;

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 1, 1, 0.1f);
        Gizmos.DrawCube(_bounds.center, _bounds.size);
        Gizmos.DrawWireCube(_bounds.center, _bounds.size);
    }

    void OnEnable()
    {
        var settings = new NavMeshBuildSettings
        {
            agentTypeID = _agentTypeID,
            agentRadius = _agentRadius,
            agentHeight = _agentHeight,
            agentSlope = _agentSlope,
            agentClimb = _agentClimb,
            minRegionArea = _minRegionArea,
            overrideVoxelSize = _overrideVoxelSize,
            voxelSize = _voxelSize,
            overrideTileSize = _overrideTileSize,
            tileSize = _tileSize,
            maxJobWorkers = uint.MaxValue,
            preserveTilesOutsideBounds = false,
        };
        List<NavMeshBuildSource> sources = new List<NavMeshBuildSource>();
        NavMeshBuilder.CollectSources(
            _bounds,
            includedLayerMask: _includedLayerMask,
            _collectGeometry,
            defaultArea: _defaultArea,
            new List<NavMeshBuildMarkup>(),
            sources
        );
        var navMeshData = NavMeshBuilder.BuildNavMeshData(settings, sources, _bounds, Vector3.zero, Quaternion.identity);
        _activeNavmesh = NavMesh.AddNavMeshData(navMeshData);
    }

    void OnDisable()
    {
        NavMesh.RemoveNavMeshData(_activeNavmesh);
    }

}