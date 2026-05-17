using UnityEngine;
using UnityEngine.SceneManagement;

public class Rondas : MonoBehaviour
{
    public static Rondas Instance { get; private set; }

    [Header("Configuración de Escenas")]
    public string escenaBatalla = "Batallas";
    public string escenaVictoria = "Victoria";

    [Header("Gestión de Rondas y Enemigos")]
    public CartaEnemigo[] listaDeEnemigos;
    public int rondaActual = 0;

    [Header("Display de Rondas (Sprites dibujados)")]
    public SpriteRenderer displayRonda;
    public Sprite[] spritesDeRondas;

    private CartaEnemigo EnemigoActual => listaDeEnemigos[rondaActual];

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); 
            return;
        }
    }

    private void Start()
    {
        ActualizarMesa();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += AlCargarEscena;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= AlCargarEscena;
    }

    private void AlCargarEscena(Scene escena, LoadSceneMode modo)
    {
        if (escena.name == "ScenajuegoCartas")
        {
            displayRonda = GameObject.Find("IndicadorDeRonda")?.GetComponent<SpriteRenderer>();

            ActualizarMesa();
        }
    }

    public void EvaluarPuntajeYPasarRonda()
    {
        if (listaDeEnemigos == null || listaDeEnemigos.Length == 0) return;

        int minPermitido = EnemigoActual.puntuacionMinima;
        int maxPermitido = EnemigoActual.puntuacionMaxima;
        int puntosJugador = GameManager.Instance.puntuacionTotal;

        if (puntosJugador < minPermitido || puntosJugador > maxPermitido)
        {
            Debug.Log("¡Puntuación fuera de rango! Entrando en combate...");
            SceneManager.LoadScene(escenaBatalla);
        }
        else
        {
            Debug.Log("¡Ronda superada pacíficamente!");
            AvanzarRonda();
        }
    }

    private void AvanzarRonda()
    {
        rondaActual++;

        if (rondaActual >= listaDeEnemigos.Length)
        {
            SceneManager.LoadScene(escenaVictoria);
        }
        else
        {
            GameManager.Instance.puntuacionTotal = 0;
            ActualizarMesa();
        }
    }

    private void ActualizarMesa()
    {
        for (int i = 0; i < listaDeEnemigos.Length; i++)
        {
            if (listaDeEnemigos[i] != null)
            {
                listaDeEnemigos[i].gameObject.SetActive(i == rondaActual);
            }
        }

        if (displayRonda != null && spritesDeRondas.Length > rondaActual)
        {
            displayRonda.sprite = spritesDeRondas[rondaActual];
        }
    }
}