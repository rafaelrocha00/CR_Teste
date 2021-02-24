using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace SistemaDeAudio
{
    public class C_Audio : MonoBehaviour
    {
        [SerializeField] List<AudioSource> audioSources;
        public static C_Audio instancia;

        private void Awake()
        {
            if (instancia != null && instancia != this)
            {
                Destroy(gameObject);
            }
            else
            {
                instancia = this;
            }
        }

        public void TocarSom(AudioClip som)
        {
            for (int i = 0; i < audioSources.Count; i++)
            {
                if (!audioSources[i].isPlaying)
                {
                    audioSources[i].clip = som;
                    audioSources[i].Play();
                    break;
                }
            }
        }
    }
}

