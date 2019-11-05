using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ViTiet.UnityExtension.Random
{
    /// <summary>
    /// A collection of Unity Random methods extension
    /// </summary>
    public static class RandomExtended
    {
        /// <summary>
        /// Returns a random point within cube/rectangle
        /// </summary>
        /// <param name="size">
        /// x = width
        /// y = height
        /// z = depth
        /// </param>
        /// <returns></returns>
        public static Vector3 RandomInCube(Transform center, Vector3 size)
        {
            Vector3[] points = new Vector3[6];
            Vector3 randomPoint;

            // x axis extreme points
            points[0] = center.right * size.x / 2 + center.position;
            points[1] = center.right * size.x / 2 * -1 + center.position;
            // y axis extreme points
            points[2] = center.up * size.y / 2 + center.position;
            points[3] = center.up * size.y / 2 * -1 + center.position;
            // z axis extreme points
            points[4] = center.forward * size.z / 2 + center.position;
            points[5] = center.forward * size.z / 2 * -1 + center.position;

            // random x
            float x = UnityEngine.Random.Range(points[0].x, points[1].x);
            // random y
            float y = UnityEngine.Random.Range(points[2].y, points[3].y);
            // random z
            float z = UnityEngine.Random.Range(points[4].z, points[5].z);

            return randomPoint = new Vector3(x, y, z);
        }
    }
}
