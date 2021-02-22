using UnityEngine;

namespace SistemaDeEngrenagens
{
    public class CaixaDeEngrenagem : MonoBehaviour
    {
        [SerializeField][Tooltip("Valor negativo para direita, positivo para esquerda.")] int direcao = 1;
        [SerializeField] Engrenagem engrenagemAtual;
        [SerializeField] Transform formatoFisico;
        [SerializeField] Sprite aparenciaEngrenagem;
        RectTransform container;

        private void Start()
        {
            container = transform as RectTransform;
            if (engrenagemAtual != null)
            {
                engrenagemAtual.IMovendo += TirarEngrenagem;
            }
        }

        public bool ChecarSeHaEngrenagem()
        {
            return engrenagemAtual != null;
        }

        public void ColocarEngrenagem(Engrenagem novaEngrenagem)
        {
            engrenagemAtual = novaEngrenagem;
            engrenagemAtual.SetarNovoPai(transform, formatoFisico.position);
            if(aparenciaEngrenagem != null)
            {
                engrenagemAtual.SetarAparencia(aparenciaEngrenagem);
            }
            engrenagemAtual.IMovendo += TirarEngrenagem;
            C_Engrenagem.instancia.ChecarVitoria();
      

        }

        public void TirarEngrenagem()
        {
            Debug.Log("tirando engrenagem");
            engrenagemAtual.IMovendo -= TirarEngrenagem;
            engrenagemAtual = null;
        }

        public bool ChecarColisao(RectTransform outro)
        {
            return RectTransformUtility.RectangleContainsScreenPoint(container, Input.mousePosition, null);
        }

        public void GirarEngrenagem()
        {
            engrenagemAtual.IniciarGiro(direcao);
        }

        public void PararEngrenagem()
        {
            engrenagemAtual.PararGiro();
        }

        public void MudarEngrenagemDeLugar(CaixaDeEngrenagem novaCaixa)
        {
            if (engrenagemAtual == null) return;

            novaCaixa.ColocarEngrenagem(engrenagemAtual);
            engrenagemAtual.Resertar();
            TirarEngrenagem();
        }
   
    }
}


