using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Player)), CanEditMultipleObjects]
public class PlayerEditor : Editor
{
    protected void OnSceneGUI()
    {
        Player player = (Player)target ;
        
        if (!player.ViewGizmos) return; 
        
        EditorGUI.BeginChangeCheck();

        var transform = player.transform;
        var position = transform.position;
        var right = transform.right;

        Handles.color = Color.magenta;
        Vector3 newAttackerDistance = Handles.Slider(position + (right * player.AttackDistance), right, 1f, Handles.ArrowHandleCap, 0.1f );
        float newTargetRadius = Handles.RadiusHandle(Quaternion.identity, position + (right * player.AttackDistance), player.BasicAttackRadius);

        Handles.color = Color.yellow;
        float newStunAttackRadius = Handles.RadiusHandle(Quaternion.identity, position, player.StunAttackRadius);

        
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(player, "Change Look At Player");
            
            player.AttackDistance = (newAttackerDistance - position).magnitude;
            player.BasicAttackRadius = newTargetRadius;

            player.StunAttackRadius = newStunAttackRadius;
        }
    }
}
