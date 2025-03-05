using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShooterKeycards : MonoBehaviour
{
    public bool logUIDs;

    public static KeycardCallback Jeff = new KeycardCallback(14873);
    public static KeycardCallback SpawnFlock { get; private set; } = new KeycardCallback(1042621551);
    public static KeycardCallback SpawnBombs { get; private set; } = new KeycardCallback(1043563799);
    public static KeycardCallback DestructiveExplode { get; private set; } = new KeycardCallback(1042658978);
    public static KeycardCallback PushExplode { get; private set; } = new KeycardCallback(1042620548);
    public static KeycardCallback CursorPush { get; private set; } = new KeycardCallback(1043524195);
    public static KeycardCallback CursorDestroy { get; private set; } = new KeycardCallback(1042649453);

    static Dictionary<uint, KeycardCallback> All = new Dictionary<uint, KeycardCallback>()
    {
        { Jeff.UID, Jeff },
        { SpawnFlock.UID, SpawnFlock },
        { SpawnBombs.UID, SpawnBombs },
        { DestructiveExplode.UID, DestructiveExplode },
        { PushExplode.UID, PushExplode },
        { CursorPush.UID, CursorPush },
        { CursorDestroy.UID, CursorDestroy }
    };

    KeycardReader reader;

    void Start()
    {
        reader = new KeycardReader((uid) => OnKeycardRead(uid));
        Jeff.OnRead += () => Debug.Log("HEHEHEHAW");
    }

    private void Update()
    {
        reader.Tick();

        // Try to connect every 100 frames if not connected
        if (!reader.Connected && Time.frameCount % 100 == 0)
            reader.TryConnect();
    }

    void OnKeycardRead(uint uid)
    {
        if (logUIDs)
            Debug.Log(uid);

        // Call appropriate callbacks here
        if (All.ContainsKey(uid))
            All[uid].Invoke();
        else
            Debug.Log("No callback for card " + uid);
    }
}

public class KeycardCallback
{
    public readonly uint UID;
    public event Action OnRead;

    public KeycardCallback(uint uid)
    {
        UID = uid;
    }

    public void Invoke() => OnRead?.Invoke();
}
