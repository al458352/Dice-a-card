using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int puntuacionTotal = 0;
    public DadoController dadoSeleccionado = null;

    [Header("Sistema de Vida")]
    public float vidaJugador = 100f;
    public Slider barraDeVida;

    [Header("Objetos UI")]
    public SpriteRenderer renderX;
    public SpriteRenderer renderDecenas;
    public SpriteRenderer renderUnidades;

    [Header("sprites numeros")]
    public Sprite[] spritesNumeros;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Instance.renderX = this.renderX;
            Instance.renderDecenas = this.renderDecenas;
            Instance.renderUnidades = this.renderUnidades;
            Instance.spritesNumeros = this.spritesNumeros;

            Destroy(gameObject);
            return;
        }
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

            puntuacionTotal = 0;

            barraDeVida = GameObject.Find("BarraVida")?.GetComponent<Slider>();
            ActualizarBarraVisual();

            ActualizarMarcadorVisual();
        }
    }

    private void Start()
    {
        Invoke("InicioRetrasado", 1f);
    }

    private void InicioRetrasado()
    {
        ActualizarMarcadorVisual();
        ActualizarBarraVisual();
    }

    public void AñadirPuntuacion(int cantidad)
    {
        puntuacionTotal += cantidad;

        if (puntuacionTotal > 99)
        {
            puntuacionTotal = 99;
        }

        Debug.Log("Puntuación total acumulada: " + puntuacionTotal);

        ActualizarMarcadorVisual();
    }

    private void ActualizarMarcadorVisual()
    {
        if (renderX == null) renderX = GameObject.Find("x")?.GetComponent<SpriteRenderer>();
        if (renderDecenas == null) renderDecenas = GameObject.Find("Decenas")?.GetComponent<SpriteRenderer>();
        if (renderUnidades == null) renderUnidades = GameObject.Find("Unidades")?.GetComponent<SpriteRenderer>();

        if (puntuacionTotal == 0)
        {
            if (renderX != null) renderX.enabled = false;
            if (renderDecenas != null) renderDecenas.enabled = false;
            if (renderUnidades != null) renderUnidades.enabled = false;
            return;
        }
        else
        {
            if (renderX != null) renderX.enabled = true;
            if (renderUnidades != null) renderUnidades.enabled = true;
        }

        int decenas = puntuacionTotal / 10;
        int unidades = puntuacionTotal % 10;

        if (spritesNumeros != null && spritesNumeros.Length > 0)
        {
            if (renderDecenas != null && decenas < spritesNumeros.Length)
                renderDecenas.sprite = spritesNumeros[decenas];

            if (renderUnidades != null && unidades < spritesNumeros.Length)
                renderUnidades.sprite = spritesNumeros[unidades];
        }

        if (puntuacionTotal < 10)
        {
            if (renderDecenas != null) renderDecenas.enabled = false;
        }
        else
        {
            if (renderDecenas != null) renderDecenas.enabled = true;
        }
    }

    public void RecibirDaño(int cantidadDaño)
    {
        vidaJugador -= cantidadDaño;

        if (vidaJugador <= 0)
        {
            vidaJugador = 0;
            ActualizarBarraVisual();
            Debug.LogWarning("escena de derrota");
            SceneManager.LoadScene("HasPerdido");
            return;
        }

        ActualizarBarraVisual();
        Debug.Log("Daño recibido: " + cantidadDaño + ". Vida actual: " + vidaJugador);
    }

    private void ActualizarBarraVisual()
    {
        if (barraDeVida != null)
        {
            barraDeVida.value = vidaJugador / 100f;
        }
    }
}