using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public static class SpeakerManager
{
    public static Dictionary<ESpeaker, GameObject> speakerDict;

    static SpeakerManager()
    {
        speakerDict = new Dictionary<ESpeaker, GameObject>();
    }

    public static void Register(ESpeaker speaker, GameObject speakerGO)
    {
        if (speakerDict.ContainsKey(speaker))
        {
            Debug.LogError($"�Ѿ�����{speaker}���͵�{speakerGO.name}");
        }
        else speakerDict.Add(speaker, speakerGO);
    }

    public static GameObject Get(ESpeaker speaker)
    {
        if (!speakerDict.ContainsKey(speaker))
        {
            Debug.LogError($"������{speaker}���͵Ķ�Ӧ����");
            return null;
        }
        else return speakerDict[speaker];
    }
}
