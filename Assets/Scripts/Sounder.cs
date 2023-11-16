using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounder: MonoBehaviour
{
   
   [SerializeField] private List<string> names;
   [SerializeField] private List<AudioClip> audioClips;
   private static Dictionary<string, AudioClip> sounds;
   [SerializeField] private AudioSource audioSource;

   public void PlaySound(string name)
   {
      audioSource.clip = sounds[name];
      audioSource.Play();
   }

   public void LoadSounds()
   {
      sounds = new Dictionary<string, AudioClip>();
      for(int i = 0; i < audioClips.Count; i++)
      {
         sounds.Add(names[i], audioClips[i]);
      } 
   }

}
