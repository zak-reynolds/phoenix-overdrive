using System;
using System.Linq;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace Assets
{
    [CustomEditor(typeof(RoadSegment))]
    class RoadSegmentEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            RoadSegment road = (RoadSegment)target;
            if (GUILayout.Button("Add Segment"))
            {
                var newSegment = Editor.Instantiate(road.gameObject);
                newSegment.transform.position += newSegment.transform.forward * 50;
                newSegment.transform.parent = road.transform.parent;
                newSegment.name = "RoadSegment";
                RoadEditor.RefreshRoadPoints(newSegment.transform.parent.GetComponent<Road>());
                Selection.activeGameObject = newSegment;
            }
        }
    }
}
