using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(GAgentDebugInfo))]
[CanEditMultipleObjects]
public class GAgentDebugInfoEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        serializedObject.Update();
        GAgentDebugInfo agent = (GAgentDebugInfo)target;
        GUILayout.Label("Name: " + agent.name);
        GUILayout.Label("Current Action: " + agent.gameObject.GetComponent<GAgent>().CurrentAction);
        GUILayout.Label("Actions: ");
        foreach (GAction a in agent.gameObject.GetComponent<GAgent>().CurrentActions)
        {
            string pre = "";
            string eff = "";

            foreach (KeyValuePair<GKey, int> p in a.Preconditions)
                pre += p.Key + ", ";
            foreach (KeyValuePair<GKey, int> e in a.Effects)
                eff += e.Key + ", ";

            GUILayout.Label("====  " + a.GetType().Name + "(" + pre + ")(" + eff + ")");
        }
        GUILayout.Label("Goals: ");
        foreach (KeyValuePair<GAgentSubGoal, int> g in agent.gameObject.GetComponent<GAgent>().CurrentGoals)
        {
            GUILayout.Label("---: ");
            foreach (KeyValuePair<GKey, int> sg in g.Key.SubGoals)
                GUILayout.Label("=====  " + sg.Key);
        }
        GUILayout.Label("Beliefs: ");
        foreach (KeyValuePair<GKey, int> sg in agent.gameObject.GetComponent<GAgent>().Beliefs.GetStates())
        {
            GUILayout.Label("=====  " + sg.Key);
        }

        GUILayout.Label("Inventory: ");
        foreach (KeyValuePair<GInventoryKey, GameObject> g in agent.gameObject.GetComponent<GAgent>().Inventory.items)
        {
            GUILayout.Label("====  " + g.Key + " : " + (g.Value ? g.Value.name : "DESTROYED"));
        }


        serializedObject.ApplyModifiedProperties();
    }
}