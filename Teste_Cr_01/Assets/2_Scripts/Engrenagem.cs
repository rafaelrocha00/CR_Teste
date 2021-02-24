using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


namespace SistemaDeEngrenagens
{
    public class Engrenagem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        bool girando;
        [SerializeField] float velocidadeAngular;
        [SerializeField] Sprite visualPadrao;
        Sprite visualAntigo;
        Rect AspectoPadrao;
        Image imagemAssociada;
        Transform paiAtual;
        Vector3 ultimaPosicao;
        public Action IMovendo;
        Quaternion rotacaoOriginal;
        bool manterTamanhoAoVoltar = false;

        private void Start()
        {
            imagemAssociada = GetComponent<Image>();
            AspectoPadrao = imagemAssociada.rectTransform.rect;
            paiAtual = transform.parent;
            rotacaoOriginal = transform.rotation;
        }

        public void SetarNovoPai(Transform novoParente, Vector3 novaPosicaoPadrao)
        {
            paiAtual = novoParente;
            ultimaPosicao = novaPosicaoPadrao;
        }

        public void SetarAparencia(Sprite novoSprite)
        {
            imagemAssociada.sprite = novoSprite;
            if (!manterTamanhoAoVoltar)
            {
                imagemAssociada.SetNativeSize();
            }
        }

        public void SetarAparenciaOriginal()
        {
            visualAntigo = imagemAssociada.sprite;
            imagemAssociada.sprite = visualPadrao;
            // O tamanho padrao do sprite é maior do que o indicado na referencia.
            ResertarTamanhoSprite(imagemAssociada.rectTransform, AspectoPadrao);
        }

        public void ResertarTamanhoSprite(RectTransform rectAtual , Rect tamanhoAntigo)
        {
            rectAtual.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, tamanhoAntigo.width);
            rectAtual.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, tamanhoAntigo.height);
            rectAtual.ForceUpdateRectTransforms();
        }

        public void SetarNovaPosicao()
        {
            transform.SetParent(paiAtual);
            transform.position = ultimaPosicao;
        }

        public void Resertar()
        {
            SetarNovaPosicao();
            SetarAparenciaOriginal();
            PararGiro();
            transform.rotation = rotacaoOriginal;
        }

        public void MoverObjetoComMouse()
        {
            transform.position = Input.mousePosition;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            ultimaPosicao = transform.position;
            transform.rotation = rotacaoOriginal;
            transform.SetParent(C_Engrenagem.instancia.paiEngrenagens);
            C_Engrenagem.instancia.PararEngrenagens();
            SetarAparenciaOriginal();
        }

        public void OnDrag(PointerEventData eventData)
        {
            MoverObjetoComMouse();
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            CaixaDeEngrenagem novaCaixa = C_Engrenagem.instancia.ChecarColisao(this);
            if (novaCaixa != null)
            {
                IMovendo?.Invoke();
                novaCaixa.ColocarEngrenagem(this);
            }
            else
            {
                SetarAparencia(visualAntigo);
            }

            SetarNovaPosicao();
            C_Engrenagem.instancia.ChecarVitoria();
        }

        public void IniciarGiro(int direcao)
        {
            if (!girando)
            {
                StartCoroutine(Girar(direcao));
            }

            girando = true;
        }

        public void PararGiro()
        {
            StopAllCoroutines();
            girando = false;
        }

        IEnumerator Girar(int direcao)
        {
            while (true)
            {
                transform.Rotate(Vector3.forward * velocidadeAngular * Time.deltaTime * direcao);
                yield return null;
            }  
        }

        public void ManterTamanho(bool manter)
        {
            manterTamanhoAoVoltar = manter;
        }
    }
}


