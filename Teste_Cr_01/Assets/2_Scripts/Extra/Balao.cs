using UnityEngine;

namespace Efeitos
{
    public class Balao : MonoBehaviour
    {
        [SerializeField] float velocidadeLinear = 100f;
        Vector3 posicaoInicial;

        private void Awake()
        {
            posicaoInicial = transform.position;
        }

        private void Update()
        {
            Mover();
        }

        private void OnEnable()
        {
            Resertar();
        }

        public void Resertar()
        {
            transform.position = posicaoInicial;
        }

        public void Mover()
        {
            Vector3 velocidadeAdicionada = new Vector3(0, velocidadeLinear, 0);
            transform.position += velocidadeAdicionada * Time.deltaTime;
        }
    }
}

