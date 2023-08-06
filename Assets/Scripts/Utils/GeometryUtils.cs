using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public static class GeometryUtils
    {
        public static bool IsPointInsidePolygon(Vector2 point, List<Vector2> polygon)
        {
            var j = polygon.Count - 1;
    
            var isInside = false;
    
            for (var i = 0; i < polygon.Count; j = i++)
            {
                isInside ^= polygon[i].y > point.y ^ polygon[j].y > point.y && point.x <
                    (polygon[j].x - polygon[i].x) * (point.y - polygon[i].y) /
                    (polygon[j].y - polygon[i].y) + polygon[i].x;
            }
    
            return isInside;
        }
        
        public static Vector2 GetClosestBorderPointTo(Vector2 point, List<Vector2> polygon)
        {
            var bestDistance = float.MaxValue;
            var bestPoint = Vector2.zero;
    
            for (var i = 0; i < polygon.Count; i++)
            {
                var currentPoint = polygon[i];
                var nextPoint = i + 1 < polygon.Count ? polygon[i + 1] : polygon[0];
    
                var closestPoint = GetClosestPointInSegment(point, currentPoint, nextPoint);
                var distance = Vector2.Distance(point, closestPoint);
    
                if (distance < bestDistance)
                {
                    bestDistance = distance;
                    bestPoint = closestPoint;
                }
            }
    
            return bestPoint;
        }
        
        private static Vector2 GetClosestPointInSegment(Vector2 point, Vector2 p0, Vector2 p1)
        {
            var v = p1 - p0;
            var w = point - p0;
    
            var dot1 = Vector2.Dot(w, v);
    
            if (dot1 <= 0)
                return p0;
    
            var dot2 = Vector2.Dot(v, v);
    
            if (dot2 <= dot1)
                return p1;
    
            var b = dot1 / dot2;
    
            var pb = p0 + b * v;
    
            return pb;
        }
    }
}