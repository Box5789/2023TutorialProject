using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class Firework : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip audioOnFire;
    public AudioClip audioOnExplosion;
    public AudioClip audioOnExplosion2;
    

    public VisualEffect VisualEffect;

    private bool Play;

    private void OnEnable()
    {
        if(Play==false)
            StartCoroutine(Firework1());
    }
    IEnumerator Firework1()
    {
        //var random=new Unity.Mathematics.Random();
        Play = true;
        while (Play)
        {
            yield return new WaitForSeconds(1/VisualEffect.GetFloat("Rate"));
            VisualEffect.SendEvent("OnPlay");
            audioSource.PlayOneShot(audioOnFire,Random.Range(0.1f,1));
            yield return new WaitForSeconds(VisualEffect.GetFloat("Life"));
            if(!VisualEffect.GetBool("MuteSound"))
                audioSource.PlayOneShot(audioOnExplosion,Random.Range(0.1f,1));
            yield return new WaitForSeconds(VisualEffect.GetFloat("Life 1"));
            if(VisualEffect.GetInt("Amount 1")!=0)
                audioSource.PlayOneShot(audioOnExplosion2,Random.Range(0.1f,1));

            
        }
    }

    //1차 폭발 색상 설정 코드
    public void SetGradiant(Gradient gradient)
    {
        VisualEffect.SetGradient("Gradiant",gradient);
    }
    
    //2차 폭발 색상 설정 코드
    public void SetGradiant1(Gradient gradient)
    {
        VisualEffect.SetGradient("Gradiant 1",gradient);
    }
        
}
