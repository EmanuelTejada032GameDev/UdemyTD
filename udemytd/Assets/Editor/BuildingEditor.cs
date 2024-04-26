using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(BuildingTypeSO))]
public class BuildingEditor : Editor
{
    private BuildingTypeSO _buildingTypeSO;


    private void OnEnable()
    {
        _buildingTypeSO = target as BuildingTypeSO;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (_buildingTypeSO.sprite == null) return;

        Texture2D sprite = AssetPreview.GetAssetPreview(_buildingTypeSO.sprite);

        GUILayout.Label("", GUILayout.Height(100), GUILayout.Width(100));

        GUI.DrawTexture(GUILayoutUtility.GetLastRect(), sprite);
    }
}
