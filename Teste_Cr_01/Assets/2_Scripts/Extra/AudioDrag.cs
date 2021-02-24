using UnityEngine;
using UnityEngine.EventSystems;


namespace SistemaDeAudio
{
    public class AudioDrag : MonoBehaviour, IEndDragHandler
    {
        [SerializeField] AudioClip somAoClicar;
        public void OnEndDrag(PointerEventData eventData)
        {
            TocarSom();
        }

        public void TocarSom()
        {
            C_Audio.instancia.TocarSom(somAoClicar);
        }
    }
}
