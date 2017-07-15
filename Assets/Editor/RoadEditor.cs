using System;
using System.Linq;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace Assets
{
    [CustomEditor(typeof(Road))]
    class RoadEditor : Editor
    {

        public static void RefreshRoadPoints(Road road)
        {
            road.roadPoints = new List<Transform>();
            for (int i = 0; i < road.transform.childCount; ++i)
            {
                road.roadPoints.Add(road.transform.GetChild(i));
            }
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            
            if (GUILayout.Button("Refresh Segment List"))
            {
                RefreshRoadPoints((Road)target);
            }
        }
    }
}
