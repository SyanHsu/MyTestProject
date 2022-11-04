using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speaker : MonoBehaviour
{
    public ESpeaker speaker;

    private void Awake()
    {
        SpeakerManager.Register(speaker, gameObject);
    }
}
