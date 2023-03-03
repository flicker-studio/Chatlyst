using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace NexusVisual.Editor.Data
{
    [Serializable]
    public struct Dialogue
    {
        [FormerlySerializedAs("Name")] public string name;
        [FormerlySerializedAs("Talk")] public string talk;

        public Dialogue(string src)
        {
            var array = src.Split(":");
            if (array.Length != 2) throw new Exception("Src Error!");
            name = array[0];
            talk = array[1];
        }

        public override bool Equals(object obj) =>
            obj is Dialogue d && d.name == name && d.talk == talk;

        public override int GetHashCode() =>
            HashCode.Combine(name, talk);
    }


    public class DialoguesNode : NexusNode
    {
        [Serializable]
        private class BindHelper : ScriptableObject
        {
            public List<Dialogue> dialogueList = new List<Dialogue>();
        }

        [JsonIgnore]
        private readonly BindHelper _bindProxy;
        [JsonProperty]
        public List<Dialogue> dialogueList
        {
            get => _bindProxy.dialogueList;
            set => _bindProxy.dialogueList = value;
        }
        [JsonIgnore]
        public SerializedProperty getListProperty
        {
            get
            {
                var obj = new SerializedObject(_bindProxy);
                return obj.FindProperty("dialogueList");
            }
        }

        public DialoguesNode()
        {
            _bindProxy = ScriptableObject.CreateInstance<BindHelper>();
        }
    }
}