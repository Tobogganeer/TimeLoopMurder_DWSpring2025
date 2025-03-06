using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/NPC")]
public class NPC : ScriptableObject
{
    // Note: THIS CODE IS TERRIBLE RIGHT NOW
    // ... it should work for the prototype

    public ID id;
    public Sprite headshot;

    public List<InspectorDialogueLine> dialogue;
    public List<InspectorEvidenceLine> evidenceResponses;

    public enum DialogueType
    {
        Greeting,
        WhyAreYouHere,
        WhatAreYouDoing,
        HowDoYouKnowMyDad,
        Farewell
    }

    [System.Serializable]
    public class InspectorDialogueLine
    {
        public DialogueType whenAsked;
        [TextArea(1, 3)]
        public string respondWith;
    }

    [System.Serializable]
    public class InspectorEvidenceLine
    {
        public EvidenceObject.Type whenShown;
        [TextArea(1, 3)]
        public string respondWith;
    }

    public enum ID
    {
        Dad,
        Mom,
        Butler,
        Investor,
        Mistress
    }
}
