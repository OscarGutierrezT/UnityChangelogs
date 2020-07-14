/*Copyright (c) 2020 Ezphera Tech SAS Colombia*/
using System;
using System.Collections.Generic;
using UnityEngine;
namespace Ezphera.Changelogs
{
    [CreateAssetMenu(fileName = "Changelog", menuName = "Ezphera Tools/Changelog", order = 1)]
    public class EzChangelog : ScriptableObject
    {
        public Texture2D icon;
        public float iconMaxWidth = 128f;
        public string title;
        public List<Changes> changes;
        [Serializable]
        public class Changes 
        {
            public string version;
            public string description;
            public DateTime date;
            public List<string> knowIssues;
            public List<string> fixes;
        }
    }
}