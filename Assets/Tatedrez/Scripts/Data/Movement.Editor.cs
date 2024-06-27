#if UNITY_EDITOR
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace Tatedrez.Data
{
    public partial class Movement
    {
        [SerializeField] private int _range = 1;

        [CustomEditor(typeof(Movement)), CanEditMultipleObjects]
        public class MovementEditor : Editor
        {
            private Movement _movement;
            private Color _originalBackgroundColor;
            private Color _originalContentColor;

            private void OnEnable()
            {
                _movement = (Movement)target;
            }

            public override void OnInspectorGUI()
            {   
                _originalBackgroundColor = GUI.backgroundColor;
                _originalContentColor = GUI.contentColor;
                
                EditorGUILayout.Space();
                DrawGrid();
                EditorGUILayout.Space();
                DrawButtons();

                string info = _movement.IsDirectional ? "Directional movement" : "Definitive movement";
                EditorGUILayout.HelpBox(info, MessageType.Info);

                if (GUI.changed)
                    EditorUtility.SetDirty(_movement);
            }

            private void DrawButtons()
            {
                EditorGUILayout.BeginHorizontal();

                GUILayoutOption[] options = { GUILayout.ExpandWidth(true), GUILayout.Height(32) };
                if (GUILayout.Button(text: "Increase Range", options))
                    _movement._range++;

                if (GUILayout.Button(text: "Decrease Range", options))
                    _movement._range = Mathf.Max(1, _movement._range - 1);

                EditorGUILayout.EndHorizontal();
            }

            private void DrawGrid()
            {
                int range = _movement._range;

                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                GUILayout.BeginVertical();
                GUILayout.FlexibleSpace();

                for (int y = range; y >= -range; y--)
                {
                    GUILayout.BeginHorizontal();
                    
                    for (int x = -range; x <= range; x++) 
                        DrawCell(x, y);
                    
                    GUILayout.EndHorizontal();
                }

                GUILayout.FlexibleSpace();
                GUILayout.EndVertical();
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();

                GUI.backgroundColor = _originalBackgroundColor;
                GUI.contentColor = _originalContentColor;
            }

            private void DrawCell(int x, int y)
            {
                const float cellSize = 32f;
                const float alpha = 0.9f;
                
                Vector2Int position = new(x, y);
                bool isOrigin = position == Vector2Int.zero;

                Vector2Int data = _movement._positions.FirstOrDefault(Match);
                bool isSelected = data != Vector2Int.zero;
                
                Color color = GetColor();
                GUIStyle buttonStyle = GetButtonStyle();
                    
                if (isOrigin)
                    DrawOrigin();
                else
                    DrawButton();

                return;

                bool Match(Vector2Int p) => p == position;

                Color GetColor()
                {
                    Color c = isOrigin
                        ? Color.clear
                        : isSelected 
                            ? Color.green.WithAlpha(alpha) 
                            : Color.white.WithAlpha(alpha);
                    return c;
                }
        
                GUIStyle GetButtonStyle()
                {
                    GUIStyle s = new(GUI.skin.button);
                    s.fontSize = (int)(cellSize * 0.8f);
                    s.fixedWidth = cellSize;
                    s.fixedHeight = cellSize;
                    return s;
                }

                void DrawOrigin()
                {
                    GUI.backgroundColor = _originalBackgroundColor;
                    GUI.contentColor = _originalContentColor;
                    
                    GUI.enabled = false;
                    string pawn = EditorGUIUtility.isProSkin ? "♙" : "♟";
                    GUILayout.Button(pawn, buttonStyle);
                    GUI.enabled = true;
                }

                void DrawButton()
                {
                    GUI.backgroundColor = color;
                    GUI.contentColor = GUI.contentColor.WithAlpha(alpha * 0.2f);
                    
                    string arrow = GetArrow();
                    if (!GUILayout.Button(arrow, buttonStyle))
                        return;
                        
                    if (isSelected)
                        _movement._positions.Remove(data);
                    else
                    {
                        Vector2Int newMovement = new(x, y);
                        _movement._positions.Add(newMovement);
                    }
                }
                
                string GetArrow()
                {
                    if (position == Vector2Int.zero || !_movement.IsDirectional)
                        return string.Empty;
                    
                    if (position.y == 0)
                        return position.x > 0 ? "→" : "←";
                    
                    if (position.x == 0)
                        return position.y > 0 ? "↑" : "↓";
                    
                    return position.x > 0 
                        ? position.y > 0 ? "↗" : "↘" 
                        : position.y > 0 ? "↖" : "↙";
                }
            }
        }
    }
}
#endif
