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
                newSegment.tag = "Road";
                newSegment.layer = 12;
                newSegment.transform.rotation.SetEulerRotation(new Vector3(0, newSegment.transform.rotation.eulerAngles.y, 0));
                newSegment.transform.position = new Vector3(newSegment.transform.position.x, 0, newSegment.transform.position.z);
                //var bc = newSegment.AddComponent<BoxCollider>();
                //bc.isTrigger = true;
                //bc.center = new Vector3(25, 0, 0);
                //bc.size = new Vector3(50, 5, 55);
                RoadEditor.RefreshRoadPoints(newSegment.transform.parent.GetComponent<Road>());
                Selection.activeGameObject = newSegment;
            }
        }
    }
}
