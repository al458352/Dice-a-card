using UnityEngine;
using UnityEngine.SceneManagement;

public class Rondas : MonoBehaviour
{
    public static Rondas Instance { get; private set; }

    [Header("Escenas")]
    public string escenaBatalla = "batallas";
    public string escenaVictoria = "Victoria";

    [Header("Rondas y Enemigos")]
    public CartaEnemigo[] listaDeEnemigos;
    public int rondaActual = 0;

    [Header("Rondas)")]
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
            Instance.listaDeEnemigos = this.listaDeEnemigos;
            Instance.ActualizarMesa();

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
            if (displayRonda == null)
            {
                displayRonda = GameObject.Find("IndicadorDeRonda")?.GetComponent<SpriteRenderer>();
            }

            if (displayRonda != null) displayRonda.enabled = true;

            ActualizarMesa();
        }
        else if (escena.name == escenaBatalla)
        {
            if (displayRonda == null)
            {
                displayRonda = GetComponent<SpriteRenderer>();
            }

            if (displayRonda != null) displayRonda.enabled = false;
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
            Debug.Log("Entrando en combate");
            SceneManager.LoadScene(escenaBatalla);
        }
        else
        {
            Debug.Log("Ronda superada");
            AvanzarRonda();
        }
    }

    private void AvanzarRonda()
    {
        rondaActual++;

        if (rondaActual >= 5 || rondaActual >= listaDeEnemigos.Length)
        {
            Debug.LogWarning("escena de Victoria");
            SceneManager.LoadScene(escenaVictoria);
        }
        else
        {
            GameManager.Instance.puntuacionTotal = 0;
            ActualizarMesa();
        }
    }

    public void ActualizarMesa()
    {
        for (int i = 0; i < listaDeEnemigos.Length; i++)
        {
            if (listaDeEnemigos[i] != null)
            {
                listaDeEnemigos[i].gameObject.SetActive(i == rondaActual);
            }
        }

        if (displayRonda != null && spritesDeRondas != null && rondaActual < spritesDeRondas.Length)
        {
            displayRonda.sprite = spritesDeRondas[rondaActual];
        }
    }
}