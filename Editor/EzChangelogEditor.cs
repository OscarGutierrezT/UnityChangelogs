using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

namespace Ezphera.Changelogs
{
    [CustomEditor(typeof(EzChangelog))]
    [InitializeOnLoad]
    public class EzChangelogEditor : Editor
    {
        static float kSpace = 16f;
        protected override void OnHeaderGUI()
        {
            var changelog = (EzChangelog)target;
            Init();

            var iconWidth = Mathf.Min(EditorGUIUtility.currentViewWidth / 3f - 20f, changelog.iconMaxWidth);

            GUILayout.BeginHorizontal("In BigTitle");
            {
                GUILayout.Label(changelog.icon, GUILayout.Width(iconWidth), GUILayout.Height(iconWidth));
                GUILayout.Label(changelog.title, TitleStyle);
            }
            GUILayout.EndHorizontal();
        }

        public override void OnInspectorGUI()
        {
            var changelog = (EzChangelog)target;
            Init();

            if (changelog.changes.Count > 0)
            {
                var changes = changelog.changes.OrderByDescending((x) => x.version);
                foreach (var change in changes)
                {
                    //if (!string.IsNullOrEmpty(change.version))
                    {
                        GUILayout.Label("Version " + change.version, HeadingStyle);
                    }
                    if (!string.IsNullOrEmpty(change.description))
                    {
                        GUILayout.Label(change.description, BodyStyle);
                    }
                    if (change.knowIssues.Count > 0)
                    {
                        //GUILayout.Space(kSpace / 2);
                        GUILayout.Label(" Know Issues", SubtitleStyle);
                        foreach (string issue in change.knowIssues)
                        {
                            GUILayout.Label(" - " + issue, BodyStyle);
                        }
                    }
                    if (change.fixes.Count > 0)
                    {
                        //GUILayout.Space(kSpace / 2);
                        GUILayout.Label(" Fixes or news", SubtitleStyle);
                        foreach (string fixes in change.fixes)
                        {
                            GUILayout.Label(" - " + fixes, BodyStyle);
                        }
                    }
                    GUILayout.Space(kSpace);
                }
            }
            else 
            {
                GUILayout.Label("Para editar el archivo de clic a los tres punticos arriba al lado del candado y seleccione debug mode, al finalizar cambie nuevamente a modo normal", SubtitleStyle);
            }
        }

        bool m_Initialized;

        GUIStyle LinkStyle { get { return m_LinkStyle; } }
        [SerializeField] GUIStyle m_LinkStyle;

        GUIStyle TitleStyle { get { return m_TitleStyle; } }
        [SerializeField] GUIStyle m_TitleStyle;

        GUIStyle SubtitleStyle { get { return m_SubtitleStyle; } }
        [SerializeField] GUIStyle m_SubtitleStyle;

        GUIStyle DescriptionStyle { get { return m_DescriptionStyle; } }
        [SerializeField] GUIStyle m_DescriptionStyle;

        GUIStyle HeadingStyle { get { return m_HeadingStyle; } }
        [SerializeField] GUIStyle m_HeadingStyle;

        GUIStyle BodyStyle { get { return m_BodyStyle; } }
        [SerializeField] GUIStyle m_BodyStyle;

        void Init()
        {
            if (m_Initialized)
                return;
            m_BodyStyle = new GUIStyle(EditorStyles.label);
            m_BodyStyle.wordWrap = true;
            m_BodyStyle.fontSize = 14;

            m_TitleStyle = new GUIStyle(m_BodyStyle);
            m_TitleStyle.fontSize = 26;

            m_HeadingStyle = new GUIStyle(m_BodyStyle);
            m_HeadingStyle.fontSize = 18;
            m_HeadingStyle.fontStyle = FontStyle.Bold;

            m_SubtitleStyle = new GUIStyle(m_BodyStyle);
            m_SubtitleStyle.fontSize = 14;
            m_SubtitleStyle.fontStyle = FontStyle.Bold;

            m_LinkStyle = new GUIStyle(m_BodyStyle);
            // Match selection color which works nicely for both light and dark skins
            m_LinkStyle.normal.textColor = new Color(0x00 / 255f, 0x78 / 255f, 0xDA / 255f, 1f);
            m_LinkStyle.stretchWidth = false;

            m_Initialized = true;
        }

        bool LinkLabel(GUIContent label, params GUILayoutOption[] options)
        {
            var position = GUILayoutUtility.GetRect(label, LinkStyle, options);

            Handles.BeginGUI();
            Handles.color = LinkStyle.normal.textColor;
            Handles.DrawLine(new Vector3(position.xMin, position.yMax), new Vector3(position.xMax, position.yMax));
            Handles.color = Color.white;
            Handles.EndGUI();

            EditorGUIUtility.AddCursorRect(position, MouseCursor.Link);

            return GUI.Button(position, label, LinkStyle);
        }
}
}