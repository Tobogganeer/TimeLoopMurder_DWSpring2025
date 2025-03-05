using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/NPC")]
public class NPC : ScriptableObject
{
    public ID id;

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
