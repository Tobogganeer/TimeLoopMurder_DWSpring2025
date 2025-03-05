using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public UnityEngine.UI.Image portrait;
    public TMPro.TMP_Text spokenLineText;

    [Space]
    public List<NPC> npcs;

    Dictionary<NPC.ID, NPC> npcDict = new Dictionary<NPC.ID, NPC>();

    float guiAlpha;
    bool guiEnabled;

    const float AlphaSpeed = 1f;

    public static bool Enabled => instance.guiEnabled;

    public static void Enable(NPC.ID npc)
    {
        instance.guiEnabled = true;
        // Disable speech bubble
        instance.speechContainer.gameObject.SetActive(false);
        // Show character
        instance.portrait.sprite = instance.npcDict[npc].headshot;
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

    private void Update()
    {
        // Fade alpha and disable object if alpha is 0 (maybe needed for raycasts? not sure)
        guiAlpha = Mathf.MoveTowards(guiAlpha, guiEnabled ? 1f : 0f, Time.deltaTime * AlphaSpeed);
        entireGUI.gameObject.SetActive(guiAlpha > 0f);
        entireGUI.alpha = guiAlpha;
    }
}
