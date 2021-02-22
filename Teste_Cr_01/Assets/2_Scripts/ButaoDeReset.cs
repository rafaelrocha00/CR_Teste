using UnityEngine;
using UnityEngine.UI;

namespace SistemaDeEngrenagens
{
    [RequireComponent(typeof(Button))]
    public class ButaoDeReset : MonoBehaviour
    {
        // Já que manager é uma singleton, eu posso não ter sempre a referencia dele no inspetor.

        void Start()
        {
            GetComponent<Button>().onClick.AddListener(Clicar);
        }

        public void Clicar()
        {
            if (C_Engrenagem.instancia != null)
            {
                C_Engrenagem.instancia.ResertarEngrenagens();
            }
        }


    }
}

