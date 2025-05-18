using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using productions;


#if UNITY_EDITOR
using UnityEditor;
[CustomEditor(typeof(DrawPileManager))]
public class DeckManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        DrawPileManager drawPileManager = (DrawPileManager)target;
        if (GUILayout.Button("Draw Next Card"))
        {
            HandManager handManager = FindObjectOfType<HandManager>();
            if (handManager != null)
            {
                drawPileManager.DrawCard(handManager, CardOwner.Player); // <-- Aquí agregas el CardOwner
            }
        }
    }
}
#endif
