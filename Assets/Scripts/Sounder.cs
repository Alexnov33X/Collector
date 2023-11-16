using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounder: MonoBehaviour
{
   
  //[SerializeField] private List<string> names;
  [SerializeField] private List<AudioClip> soundClips;
  [SerializeField] private List<AudioClip> musicClips;
  private Dictionary<string, AudioClip> sounds;
  private Dictionary<string, AudioClip> musics;
  [SerializeField] private AudioSource soundSource, musicSource;
  public void PlaySound(string name)
   {
      soundSource.clip = sounds[name];
      soundSource.Play();
   }

   public void LoadSounds()
   {
      sounds = new Dictionary<string, AudioClip>();
      musics = new Dictionary<string, AudioClip>();
      foreach (var sound in soundClips)
      {
         sounds.Add(sound.name, sound);
      }
      foreach (var music in musicClips)
      {
         musics.Add(music.name, music);
      }
  }

   public void ButtonClick()
   {
      PlaySound("button_click");
   }

   public void PlayMusic(string name)
   {
      musicSource.clip = musics[name];
      musicSource.Play();
   }

   public void StopMusic()
   {
      musicSource.Stop();
   }
}
