using MarioMonkeMadness;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LibSM64
{
    static internal class Utils
    {
        private static Interop.SM64Surface[] SurfaceArray;

        private static Mesh unityCubeMesh = null;

        static void TransformAndGetSurfaces(List<Interop.SM64Surface> outSurfaces, Mesh mesh, SM64SurfaceType surfaceType, SM64TerrainType terrainType, Func<Vector3, Vector3> transformFunc)
        {
            var tris = mesh.GetTriangles(0);
            var vertices = mesh.vertices.Select(transformFunc).ToArray();

            for (int i = 0; i < tris.Length; i += 3)
            {
                outSurfaces.Add(new Interop.SM64Surface
                {
                    force = 0,
                    type = (short)surfaceType,
                    terrain = (ushort)terrainType,
                    v0x = (short)(RefCache.Scale * -vertices[tris[i]].x),
                    v0y = (short)(RefCache.Scale * vertices[tris[i]].y),
                    v0z = (short)(RefCache.Scale * vertices[tris[i]].z),
                    v1x = (short)(RefCache.Scale * -vertices[tris[i + 2]].x),
                    v1y = (short)(RefCache.Scale * vertices[tris[i + 2]].y),
                    v1z = (short)(RefCache.Scale * vertices[tris[i + 2]].z),
                    v2x = (short)(RefCache.Scale * -vertices[tris[i + 1]].x),
                    v2y = (short)(RefCache.Scale * vertices[tris[i + 1]].y),
                    v2z = (short)(RefCache.Scale * vertices[tris[i + 1]].z)
                });
            }
        }

        static public Interop.SM64Surface[] GetSurfacesForMesh(Vector3 scale, Mesh mesh, SM64SurfaceType surfaceType, SM64TerrainType terrainType)
        {
            var surfaces = new List<Interop.SM64Surface>();
            TransformAndGetSurfaces(surfaces, mesh, surfaceType, terrainType, x => Vector3.Scale(scale, x));
            return surfaces.ToArray();
        }

        static public Interop.SM64Surface[] GetAllStaticSurfaces()
        {
            if (RefCache.TerrainUpdated || SurfaceArray == null)
            {
                List<Interop.SM64Surface> surfaceList = new();

                foreach (var obj in RefCache.TerrainList)
                {
                    if (!obj.enabled) continue;

                    Mesh objMesh;
                    Vector3 meshScale = Vector3.one;
                    if (obj.GetComponent<MeshCollider>() != null)
                    {
                        objMesh = obj.GetComponent<MeshCollider>().sharedMesh;
                    }
                    else if (obj.GetComponent<BoxCollider>() != null)
                    {
                        if (unityCubeMesh == null)
                            unityCubeMesh = Resources.GetBuiltinResource<Mesh>("Cube.fbx");
                        objMesh = unityCubeMesh;
                        meshScale = obj.GetComponent<BoxCollider>().size;
                    }
                    else if (obj.GetComponent<MeshFilter>() != null)
                    {
                        objMesh = obj.GetComponent<MeshFilter>().sharedMesh;
                    }
                    else
                    {
                        continue;
                    }

                    if (objMesh != null)
                        TransformAndGetSurfaces(surfaceList, objMesh, obj.SurfaceType, obj.TerrainType, x => obj.transform.TransformPoint(Vector3.Scale(meshScale, x)));
                }

                SurfaceArray = surfaceList.ToArray();
                RefCache.TerrainUpdated = false;
            }

            return SurfaceArray;
        }
    }
}