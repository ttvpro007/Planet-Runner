using System;
using UnityEngine;
using ViTiet.Generic;
using ViTiet.UnityExtension.Math;
using Plane = ViTiet.UnityExtension.Math.Plane;

namespace ViTiet.UnityExtension.Gizmos
{
    /// <summary>
    /// Extends Unity Gizmos
    /// </summary>
    public static class GizmosExtended
    {
        /// <summary>
        /// Draws a 2D rectangle
        /// </summary>
        public static void DrawWireRectangle2D(Transform center, float width, float height, Color color)
        {
            Vector3[] points = new Vector3[4];

            points[0] = new Vector3(center.position.x - width / 2, center.position.y, center.position.z + height / 2);
            points[1] = new Vector3(center.position.x + width / 2, center.position.y, center.position.z + height / 2);
            points[2] = new Vector3(center.position.x + width / 2, center.position.y, center.position.z - height / 2);
            points[3] = new Vector3(center.position.x - width / 2, center.position.y, center.position.z - height / 2);

            //points = GetRotatePointsXAxis(center, points);
            points = VectorExtended.GetRotatePointsYAxis(center, points);
            //points = GetRotatePointsZAxis(center, points);

            for (int i = 0; i < 4; i++)
            {
                UnityEngine.Gizmos.color = color;
                UnityEngine.Gizmos.DrawLine(points[i], points[LoopLogic.GetNextLoopIndex(i, 0, 4)]);
            }
        }

        /// <summary>
        /// Draws a 3D rectangle box
        /// </summary>
        public static void DrawWireRectangle3D(Transform center, float width, float height, float depth, Color color)
        {
            UnityEngine.Gizmos.color = color;
            Vector3[] points = new Vector3[8];

            // Loop from 1 to 4
            // Left plane  [1,4,8,5]
            // Right plane [2,3,7,6]
            // x[1,4] = x[5,8] = center x - width / 2
            // x[2,3] = x[6,7] = center x + width / 2
            // Top plane    [1,5,6,2]
            // Bottom plane [3,4,8,7]
            // y[1,2] = y[5,6] = center y + depth / 2
            // y[3,4] = y[7,8] = center y - depth / 2
            // Rear plane  [1,2,3,4]
            // Front plane [5,6,7,8]
            // z[1,2,3,4] + heigth = z[5,6,7,8]
            for (int i = 0; i < 4; i++)
            {
                if (i == 0 || i == 3)
                     points[i].x = center.position.x - width / 2;
                else points[i].x = center.position.x + width / 2;

                if (i == 0 || i == 1)
                     points[i].y = center.position.y + depth / 2;
                else points[i].y = center.position.y - depth / 2;

                points[i].z = center.position.z - height / 2;

                points[i + 4].x = points[i].x;
                points[i + 4].y = points[i].y;
                points[i + 4].z = points[i].z + height;
            }

            points = VectorExtended.GetRotatePointsYAxis(center, points);

            // draw rear rect
            for (int i = 0; i < 4; i++)
            {
                UnityEngine.Gizmos.DrawLine(points[i], points[LoopLogic.GetNextLoopIndex(i, 0, 4)]);
            }

            // draw front rect
            for (int i = 4; i < 8; i++)
            {
                UnityEngine.Gizmos.DrawLine(points[i], points[LoopLogic.GetNextLoopIndex(i, 4, 8)]);
            }

            // connect rear & front rect
            for (int i = 0; i < 4; i++)
            {
                UnityEngine.Gizmos.DrawLine(points[i], points[i + 4]);
            }
        }

        /// <summary>
        /// Draws a cube/rectangle box
        /// </summary>
        public static void DrawWireCube(Transform center, Vector3 size, Color color)
        {
            UnityEngine.Gizmos.color = color;
            Vector3[] scaledDirectionalVectors = new Vector3[5];
            Vector3[] cornerDirectionalVectors = new Vector3[4];
            Vector3[] points = new Vector3[8];

            // x axis scaled directional vectors (right/left)
            scaledDirectionalVectors[0] = center.right * size.x / 2;
            scaledDirectionalVectors[1] = center.right * size.x / 2 * -1;
            // y axis scaled directional vectors (up/down)
            scaledDirectionalVectors[2] = center.up * size.z / 2;
            scaledDirectionalVectors[3] = center.up * size.z / 2 * -1;
            // z axis scaled forward directional vector (forward)
            scaledDirectionalVectors[4] = center.forward * size.y / 2;

            // generate corner directional vectors for front plane
            // left up foward vector
            cornerDirectionalVectors[0] = scaledDirectionalVectors[1] + scaledDirectionalVectors[2] + scaledDirectionalVectors[4];
            // right up forward vector
            cornerDirectionalVectors[1] = scaledDirectionalVectors[0] + scaledDirectionalVectors[2] + scaledDirectionalVectors[4];
            // right down forward vector
            cornerDirectionalVectors[2] = scaledDirectionalVectors[0] + scaledDirectionalVectors[3] + scaledDirectionalVectors[4];
            // left down forward vector
            cornerDirectionalVectors[3] = scaledDirectionalVectors[1] + scaledDirectionalVectors[3] + scaledDirectionalVectors[4];
            
            // generate front plane points
            // left up foward point
            points[4] = VectorExtended.GetDestinationPoint(cornerDirectionalVectors[0], center.position);
            // right up forward point
            points[5] = VectorExtended.GetDestinationPoint(cornerDirectionalVectors[1], center.position);
            // right down forward point
            points[6] = VectorExtended.GetDestinationPoint(cornerDirectionalVectors[2], center.position);
            // left down forward point
            points[7] = VectorExtended.GetDestinationPoint(cornerDirectionalVectors[3], center.position);

            // generate rear plane points
            for (int i = 0; i < 4; i++)
            {
                if (i < 2)
                {
                    points[i] = VectorExtended.GetDestinationPoint(cornerDirectionalVectors[i + 2] * -1, center.position);
                }
                else
                {
                    points[i] = VectorExtended.GetDestinationPoint(cornerDirectionalVectors[i - 2] * -1, center.position);
                }
            }

            // draw rear rect
            for (int i = 0; i < 4; i++)
            {
                UnityEngine.Gizmos.DrawLine(points[i], points[LoopLogic.GetNextLoopIndex(i, 0, 4)]);
            }

            // draw front rect
            for (int i = 4; i < 8; i++)
            {
                UnityEngine.Gizmos.DrawLine(points[i], points[LoopLogic.GetNextLoopIndex(i, 4, 8)]);
            }

            // connect rear & front rect
            for (int i = 0; i < 4; i++)
            {
                UnityEngine.Gizmos.DrawLine(points[i], points[i + 4]);
            }
        }

        /// <summary>
        /// Draws a 2D ellipse
        /// </summary>
        public static void DrawWireEllipse2D(Transform center, float diameterA, float diameterB, int segments, Plane plane, Color color)
        {
            float segmentAngle = 2 * Mathf.PI / segments;
            float pointA = 0;
            float pointB = 0;
            Vector3[] points = new Vector3[segments];
            Vector3 currentPoint = Vector3.zero;
            Vector3 nextPoint = Vector3.zero;
            
            for (int i = 0; i < points.Length; i++)
            {
                pointA = diameterA / 2 * Mathf.Cos(segmentAngle * i);
                pointB = diameterB / 2 * Mathf.Sin(segmentAngle * i);
                //currentPoint = GetPoint(center, pointA, pointB, plane);
                points[i] = VectorExtended.GetPoint(center.position, pointA, pointB, plane);

                //pointA = diameterA / 2 * Mathf.Cos(segmentAngle * LoopLogic.GetNextLoopIndex(i, 0, segments));
                //pointB = diameterB / 2 * Mathf.Sin(segmentAngle * LoopLogic.GetNextLoopIndex(i, 0, segments));
                //nextPoint = GetPoint(center, pointA, pointB, plane);

                //UnityEngine.Gizmos.color = color;
                //UnityEngine.Gizmos.DrawLine(currentPoint, nextPoint);
            }

            points = VectorExtended.GetRotatePointsYAxis(center, points);

            for (int i = 0; i < points.Length; i++)
            {
                UnityEngine.Gizmos.color = color;
                UnityEngine.Gizmos.DrawLine(points[i], points[LoopLogic.GetNextLoopIndex(i, 0, points.Length)]);
            }
        }

        /// <summary>
        /// Draws a 3D ellipse
        /// </summary>
        public static void DrawWireEllipse3D(Transform center, Vector3 diameter, int segments, Color color)
        {
            for (int i = 0; i < 3; i++)
            {
                // Draw XY
                DrawWireEllipse2D(center, diameter.x, diameter.y, segments, Plane.XY, color);
                // Draw XZ
                DrawWireEllipse2D(center, diameter.x, diameter.z, segments, Plane.XZ, color);
                // Draw YZ
                DrawWireEllipse2D(center, diameter.y, diameter.z, segments, Plane.YZ, color);
            }
        }

        /// <summary>
        /// Draws a symetrical shape based on the number of segments
        /// </summary>
        public static void DrawWireSymetricalShape2D(Transform center, float diameter, int segments, Plane plane, Color color)
        {
            if (segments <= 3) return;

            DrawWireEllipse2D(center, diameter, diameter, segments, plane, color);
        }
    }
}