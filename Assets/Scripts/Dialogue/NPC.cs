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
        Farewell,
        WhatAreYouDoingTonight
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

public static class NPCIDExtensions
{
    public static string GetName(this NPC.ID id) => id switch
    {
        NPC.ID.Dad => "Jeremias",
        NPC.ID.Mom => "Zephyer",
        NPC.ID.Butler => "Yorick",
        NPC.ID.Investor => "Lysander",
        NPC.ID.Mistress => "Kensington",
        _ => throw new System.NotImplementedException(),
    };

    /*
    DialogueGUI.AddChoice("Accuse Lysander", () => Accuse(NPC.ID.Investor), false);
    DialogueGUI.AddChoice("Accuse Kensington", () => Accuse(NPC.ID.Mistress), false);
    DialogueGUI.AddChoice("Accuse Yorick", () => Accuse(NPC.ID.Butler), false);
    DialogueGUI.AddChoice("Accuse Zephyer", () => Accuse(NPC.ID.Mom), false);
    */
}
