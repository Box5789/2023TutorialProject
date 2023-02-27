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
public class Firework : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip audioOnFire;
    public AudioClip audioOnExplosion;
    public AudioClip audioOnExplosion2;
    

    public VisualEffect VisualEffect;

    private void Awake()
    {
        StartCoroutine(Firework1());
    }

    IEnumerator Firework1()
    {
        while (true)
        {
            yield return new WaitForSeconds(1/VisualEffect.GetFloat("Rate"));
            VisualEffect.SendEvent("OnPlay");
            audioSource.PlayOneShot(audioOnFire);
            yield return new WaitForSeconds(VisualEffect.GetFloat("Life"));
            if(!VisualEffect.GetBool("MuteSound"))
                audioSource.PlayOneShot(audioOnExplosion);
            yield return new WaitForSeconds(VisualEffect.GetFloat("Life 1"));
            if(VisualEffect.GetInt("Amount 1")!=0)
                audioSource.PlayOneShot(audioOnExplosion2);
        }
    }
        
}
