using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.VFX;

[Serializable]
public class Audio
{
    public AudioSource audioSource;
}
public class SFX : MonoBehaviour
{
    public AudioSource audioSource;

    public Audio audio;

    public VisualEffect VisualEffect;

    private void Awake()
    {
        
    }
}
