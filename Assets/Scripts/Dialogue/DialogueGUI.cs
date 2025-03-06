using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueGUI : MonoBehaviour
{
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

    [Space]
    public GameObject choiceButtonPrefab;
    public List<NPC> npcs;

    Dictionary<NPC.ID, NPC> npcDict = new Dictionary<NPC.ID, NPC>();
    List<Button> choiceButtons = new List<Button>();

    float guiAlpha;
    bool guiEnabled;
    NPC.ID currentNPC;

    const float AlphaSpeed = 1f;

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
    }

    public static void Disable()
    {
        instance.guiEnabled = false;
    }

    public static void Speak(NPC.ID npc, string text)
    {
        Enable(npc);
        // Turn on speech bubble
        instance.speechContainer.gameObject.SetActive(true);
        instance.spokenLineText.text = text;
    }

    public static void Speak(NPC.ID npc, NPC.DialogueType line)
    {
        Speak(npc, instance.npcDict[npc].dialogue.Find((dialogue) => dialogue.whenAsked == line).respondWith);
    }

    public static void Speak(NPC.ID npc, EvidenceObject.Type evidence)
    {
        Speak(npc, instance.npcDict[npc].evidenceResponses.Find((ev) => ev.whenShown == evidence).respondWith);
    }

    public static void ClearChoices()
    {
        for (int i = instance.choiceButtons.Count - 1; i >= 0; i--)
            Destroy(instance.choiceButtons[i].gameObject);
        instance.choiceButtons.Clear();
    }

    public static void AddChoice(string text, Action callback)
    {
        // Spawn button, set callback when clicked, set text
        Button button = Instantiate(instance.choiceButtonPrefab, instance.choiceContainer).GetComponent<Button>();
        button.onClick.AddListener(() => callback?.Invoke());
        button.transform.GetChild(0).GetComponent<TMPro.TMP_Text>().text = text;
        instance.choiceButtons.Add(button);
    }

    public static void AddGoodbyeChoice()
    {
        AddChoice("\"Goodbye\"", () => Speak(CurrentNPC.id, NPC.DialogueType.Farewell));
    }

    private void Update()
    {
        // Fade alpha and disable object if alpha is 0 (maybe needed for raycasts? not sure)
        guiAlpha = Mathf.MoveTowards(guiAlpha, guiEnabled ? 1f : 0f, Time.deltaTime * AlphaSpeed);
        entireGUI.gameObject.SetActive(guiAlpha > 0f);
        entireGUI.alpha = guiAlpha;
    }
}
