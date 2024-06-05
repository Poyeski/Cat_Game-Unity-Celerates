using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BackgroundMusic : MonoBehaviour
{
  private static BackgroundMusic backgroundMusic;
  public AudioSource BGM;

  public void StopBGM()
  {
    BGM.Stop();
  }

public void StartBGM()
{
  BGM.Play();
}
 
  void Start()
  {
    BGM.Play();
  }
}
