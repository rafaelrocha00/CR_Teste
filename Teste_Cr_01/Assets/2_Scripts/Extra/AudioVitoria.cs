using UnityEngine;
using SistemaDeEngrenagens;

namespace SistemaDeAudio
{
    public class AudioVitoria : MonoBehaviour
    {
        [SerializeField] AudioClip som;

        private void Start()
        {
            C_Engrenagem.instancia.Ivenceu += TocarAudio;
        }

        public void TocarAudio()
        {
            C_Audio.instancia.TocarSom(som);
        }
    }
}
