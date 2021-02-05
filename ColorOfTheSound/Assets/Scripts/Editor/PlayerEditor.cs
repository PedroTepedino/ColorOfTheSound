using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Player)), CanEditMultipleObjects]
public class PlayerEditor : Editor
{
    protected void OnSceneGUI()
    {
        Player player = (Player)target ;
        
        EditorGUI.BeginChangeCheck();

        var position = player.transform.position;
        Vector3 newTargetPosition = Handles.PositionHandle(player.RelativePosition + position, Quaternion.identity) - position;
        
        Handles.color = Color.green;
        float newTargetRadius = Handles.RadiusHandle(Quaternion.identity, position + newTargetPosition, player.Radius);
        
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(player, "Change Look At Player");
            
            player.RelativePosition = newTargetPosition;
            player.Radius = newTargetRadius;
        }
    }
}
