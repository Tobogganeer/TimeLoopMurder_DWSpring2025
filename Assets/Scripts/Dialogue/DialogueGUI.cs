using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueGUI : MonoBehaviour
{
    // This code is atrocious
    // Sorry to whoever has to deal with it in the future (me)

    private static DialogueGUI instance;
    private void Awake()
    {
        instance = this;

        npcDict.Clear();
        foreach (NPC npc in npcs)
            npcDict.Add(npc.id, npc);
    }

    public CanvasGroup entireGUI;
    public CanvasGroup speechContainer;
    public Image portrait;
    public TMPro.TMP_Text spokenLineText;
    public Transform choiceContainer;
    public TMPro.TMP_Text characterNameText;

    [Space]
    public GameObject choiceButtonPrefab;
    public List<NPC> npcs;

    Dictionary<NPC.ID, NPC> npcDict = new Dictionary<NPC.ID, NPC>();
    List<Button> choiceButtons = new List<Button>();
    // Hacky way to track and make sure we don't add a choice twice
    List<EvidenceObject.Type> buttonEvidenceTypes = new List<EvidenceObject.Type>();

    float guiAlpha;
    bool guiEnabled;
    NPC.ID currentNPC;

    const float AlphaSpeed = 1.3f;

    public static bool Enabled => instance.guiEnabled;
    public static NPC CurrentNPC => instance.npcDict[instance.currentNPC];
    public static bool HasChoices => instance.choiceButtons.Count > 0;

    public static void Enable(NPC.ID npc)
    {
        instance.guiEnabled = true;
        // Disable speech bubble
        instance.speechContainer.gameObject.SetActive(false);
        ClearChoices();
        // Show character
        instance.portrait.sprite = instance.npcDict[npc].headshot;
        instance.currentNPC = npc;
        instance.characterNameText.text = npc.GetName();
    }

    public static void Disable()
    {
        instance.guiEnabled = false;
    }

    public static void Speak(NPC.ID npc, string text, bool addDefaultChoices = true)
    {
        Enable(npc);
        // Turn on speech bubble
        instance.speechContainer.gameObject.SetActive(true);
        instance.spokenLineText.text = text;
        if (addDefaultChoices)
            AddDefaultChoices();
    }

    public static void Speak(NPC.ID npc, NPC.DialogueType line, bool addDefaultChoices = true)
    {
        Speak(npc, instance.npcDict[npc].dialogue.Find((dialogue) => dialogue.whenAsked == line).respondWith, addDefaultChoices);
    }

    public static void Speak(NPC.ID npc, EvidenceObject.Type evidence, bool addDefaultChoices = true)
    {
        // TODO: make this not terrible
        var line = instance.npcDict[npc].evidenceResponses.Find((ev) => ev.whenShown == evidence);
        if (line != null)
            Speak(npc, line.respondWith, addDefaultChoices);
        else
            Speak(npc, "(No response yet)", addDefaultChoices);

        // TODO: Remove after demo
        if (npc == NPC.ID.Butler && evidence == EvidenceObject.Type.Footprints)
        {
            PopUp.Show("This is incriminating evidence. Using photos to question the 4 NPCs is how you'll solve the first puzzle.", 10f);
        }
    }

    public static void ClearChoices()
    {
        for (int i = instance.choiceButtons.Count - 1; i >= 0; i--)
            Destroy(instance.choiceButtons[i].gameObject);
        instance.choiceButtons.Clear();
        instance.buttonEvidenceTypes.Clear();
    }

    public static void AddChoice(string text, Action callback, bool takesAnAction = true, EvidenceObject.Type type = EvidenceObject.Type.None)
    {
        // Spawn button, set callback when clicked, set text
        Button button = Instantiate(instance.choiceButtonPrefab, instance.choiceContainer).GetComponent<Button>();
        button.onClick.AddListener(() => callback?.Invoke());
        if (takesAnAction)
            button.onClick.AddListener(() => Loop.TakeAction());

        button.transform.GetChild(0).GetComponent<TMPro.TMP_Text>().text = text;
        instance.choiceButtons.Add(button);
        instance.buttonEvidenceTypes.Add(type);
    }

    public static bool HasEvidenceButton(EvidenceObject.Type type) => instance.buttonEvidenceTypes.Contains(type);

    public static void AddBackChoice()
    {
        AddChoice("Back", () =>
        {
            ClearChoices();
            AddDefaultChoices();
        }, false);
    }

    public static void AddDefaultChoices()
    {
        AddChoice("\"Goodbye.\"", () => Speak(CurrentNPC.id, NPC.DialogueType.Farewell, false), false);
        AddChoice("\"How do you know my dad?\"", () => Speak(CurrentNPC.id, NPC.DialogueType.HowDoYouKnowMyDad));
        AddChoice("\"What are you doing?\"", () => Speak(CurrentNPC.id, NPC.DialogueType.WhatAreYouDoing));
        AddChoice("\"Why are you here?\"", () => Speak(CurrentNPC.id, NPC.DialogueType.WhyAreYouHere));
    }

    private void Update()
    {
        // Fade alpha and disable object if alpha is 0 (maybe needed for raycasts? not sure)
        guiAlpha = Mathf.MoveTowards(guiAlpha, guiEnabled ? 1f : 0f, Time.deltaTime * AlphaSpeed);
        entireGUI.gameObject.SetActive(guiAlpha > 0f);
        entireGUI.alpha = guiAlpha;
    }
}
