using System;
using System.Collections.Generic;
using UnityEngine;

namespace SistemaDeEngrenagens
{
    public class C_Engrenagem : MonoBehaviour
    {
        public Action Ivenceu;
        public Action Iresertou;
        bool venceu = false;
        public static C_Engrenagem instancia;
        //Evita engrenagens em baixo de outros objetos de UI;
        public Transform paiEngrenagens;

        [SerializeField] List<CaixaDeEngrenagem> inventario = new List<CaixaDeEngrenagem>();
        [SerializeField] List<CaixaDeEngrenagem> circulos = new List<CaixaDeEngrenagem>();


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

        private void Start()
        {
            Ivenceu += GirarEngrenagens;
        }

        public CaixaDeEngrenagem ChecarColisao(Engrenagem engrenagem)
        {
            CaixaDeEngrenagem retorno = ChecarColisao(engrenagem.transform, inventario);
            if (retorno == null)
            {
                retorno = ChecarColisao(engrenagem.transform, circulos);
            }

            return retorno;
        }

        CaixaDeEngrenagem ChecarColisao(Transform transform, List<CaixaDeEngrenagem> lista)
        {
            CaixaDeEngrenagem retorno = null;
            for (int i = 0; i < lista.Count; i++)
            {
                if (lista[i].ChecarSeHaEngrenagem())
                {
                    continue;
                }
                if (lista[i].ChecarColisao(transform as RectTransform))
                {
                    retorno = lista[i];
                }
            }

            return retorno;
        }

        public bool ChecarVitoria()
        {
            bool venceu = true;
            for (int i = 0; i < circulos.Count; i++)
            {
                if (!circulos[i].ChecarSeHaEngrenagem())
                {
                    venceu = false;
                }
            }
            if (venceu) Ivenceu.Invoke();
            this.venceu = venceu;
            return venceu;
        }

        public void GirarEngrenagens()
        {
            for (int i = 0; i < circulos.Count; i++)
            {
                circulos[i].GirarEngrenagem();
            }
        }

        public void PararEngrenagens()
        {
            if (!venceu) return;

            for (int i = 0; i < circulos.Count; i++)
            {
                circulos[i].PararEngrenagem();
            }

            venceu = false;
        }

        public void ResertarEngrenagens()
        {
            for (int i = 0; i < circulos.Count; i++)
            {
                for (int t = 0; t < inventario.Count; t++)
                {
                    if (!inventario[t].ChecarSeHaEngrenagem())
                    {
                        circulos[i].MudarEngrenagemDeLugar(inventario[t]);
                        break;
                    }
                }
                 
            }

            PararEngrenagens();
            Iresertou?.Invoke();
        }
    }
}

