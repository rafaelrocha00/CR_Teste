using System;
using System.Collections;
using UnityEngine;
using SistemaDeEngrenagens;

namespace Efeitos
{
    public class Trigger_Balao : MonoBehaviour
    {
        [SerializeField] GameObject paiBaloes;
        [SerializeField] float tempoTotal = 2f;

        void Start()
        {
            C_Engrenagem.instancia.Ivenceu += AtivarBaloes;
        }

        void AtivarBaloes()
        {
            paiBaloes.SetActive(true);
            StartCoroutine(Esperar(tempoTotal, DesativarBaloes));
        }

        public void DesativarBaloes()
        {
            paiBaloes.SetActive(false);
        }

        IEnumerator Esperar(float tempo, Action metodoAChamar)
        {
            yield return new WaitForSeconds(tempo);
            metodoAChamar?.Invoke();
        }
    }

}
