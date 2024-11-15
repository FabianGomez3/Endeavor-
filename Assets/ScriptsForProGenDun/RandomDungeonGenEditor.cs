// using System.Collections;
// using System.Collections.Generic;
// using UnityEditor;
// using UnityEngine;
// using UnityEngine.EventSystems;

// [CustomEditor(typeof(AbstactDungeonGen), true)]
// public class RandomDungeonGenEditor : Editor
// {
//     AbstactDungeonGen generator;

//     private void Awake()
//     {
//         generator = (AbstactDungeonGen) target;
//     }

//     public override void OnInspectorGUI()
//     {
//         base.OnInspectorGUI();
//         if(GUILayout.Button("Create Dungeon"))
//         {
//             generator.GenerateDungeon();
//         }
//     }
// }
