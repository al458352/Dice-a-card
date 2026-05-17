using UnityEngine;
using System.Collections;

public class SpawnerBalas : MonoBehaviour
{
    [System.Serializable]
    public class DatosDificultadRonda
    {
        public string nombreRonda;
        public int cantidadTotalBalas = 10;
        public float intervaloEntreBalas = 1.0f;
        public float velocidadBaseBalas = 5f;
    }

    [Header("Configuración de Dificultad")]
    public DatosDificultadRonda[] dificultadPorRonda;

    [Header("Referencias de Prefabs")]
    public GameObject prefabBalaInercia;
    public string tagDelJugador = "Player";

    [Header("Configuración del Círculo (Satélites)")]
    [Tooltip("Arrastra aquí todos los GameObjects vacíos que actúan como puntos de disparo.")]
    public Transform[] puntosDeOrigen;

    [Tooltip("Velocidad a la que gira el círculo de spawners alrededor del jugador.")]
    public float velocidadDeGiroDelCirculo = 50f;

    private int balasADisparar;
    private float intervaloActual;
    private float velocidadActual;
    private Transform referenciaJugador;

    private void Start()
    {
        GameObject jugador = GameObject.FindGameObjectWithTag(tagDelJugador);
        if (jugador != null)
        {
            referenciaJugador = jugador.transform;
        }
        else
        {
            Debug.LogError("No se encontró ningún GameObject con el Tag 'Player'.");
        }

        ConfigurarDificultad();

        if (prefabBalaInercia != null && referenciaJugador != null && puntosDeOrigen.Length > 0)
        {
            StartCoroutine(RutinaDisparoCircular());
        }
    }

    private void Update()
    {

        transform.Rotate(Vector3.forward * velocidadDeGiroDelCirculo * Time.deltaTime);

 
        if (referenciaJugador != null)
        {
            transform.position = referenciaJugador.position;
        }
    }

    private void ConfigurarDificultad()
    {
        if (Rondas.Instance != null && dificultadPorRonda != null)
        {
            int indexRonda = Rondas.Instance.rondaActual;

            if (indexRonda < dificultadPorRonda.Length)
            {
                DatosDificultadRonda datosActuales = dificultadPorRonda[indexRonda];
                balasADisparar = datosActuales.cantidadTotalBalas;
                intervaloActual = datosActuales.intervaloEntreBalas;
                velocidadActual = datosActuales.velocidadBaseBalas;
            }
        }
    }

    private IEnumerator RutinaDisparoCircular()
    {
        for (int i = 0; i < balasADisparar; i++)
        {
            int indiceAleatorio = Random.Range(0, puntosDeOrigen.Length);
            Transform puntoDisparoElegido = puntosDeOrigen[indiceAleatorio];


            if (puntoDisparoElegido != null)
            {
                GameObject nuevaBala = Instantiate(prefabBalaInercia, puntoDisparoElegido.position, Quaternion.identity);

                BalaInercia scriptBala = nuevaBala.GetComponent<BalaInercia>();
                if (scriptBala != null)
                {
                    scriptBala.objetivo = referenciaJugador;
                    scriptBala.velocidadBase = velocidadActual;
                }
            }

            yield return new WaitForSeconds(intervaloActual);
        }
    }
}