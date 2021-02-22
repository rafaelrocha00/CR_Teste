using UnityEngine;
using TMPro;

namespace SistemaDeEngrenagens
{
    public class TextoAlteravel : MonoBehaviour
    {
        [SerializeField] bool resertar;
        [SerializeField][TextArea(0,2)] string textoAoVencer;
        string textoOriginal;
        [SerializeField] TextMeshProUGUI caixaDeTexto;

        private void Start()
        {
            C_Engrenagem.instancia.Ivenceu += MudarTexto;
            if (resertar)
            {
                textoOriginal = caixaDeTexto.text;
                C_Engrenagem.instancia.Iresertou += ResertarTexto;
            }
        }

        public void MudarTexto()
        {
            caixaDeTexto.text = textoAoVencer;
        }

        public void ResertarTexto()
        {
            caixaDeTexto.text = textoOriginal;
        }
    }
}

