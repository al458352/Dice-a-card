using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int puntuacionTotal = 0;
    public DadoController dadoSeleccionado = null;

    [Header("Objetos UI")]
    public SpriteRenderer renderX;
    public SpriteRenderer renderDecenas;
    public SpriteRenderer renderUnidades;

    [Header("sprites numeros")]
    public Sprite[] spritesNumeros;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        Invoke("InicioRetrasado", 1f);
    }
    private void InicioRetrasado()
    {
        ActualizarMarcadorVisual();
    }
    public void AñadirPuntuacion(int cantidad)
    {
        puntuacionTotal += cantidad;

        if (puntuacionTotal > 99)
        {
            puntuacionTotal = 99;
        }

        Debug.Log("Puntuación total acumulada: " + puntuacionTotal);



        Invoke("InicioRetrasado", 1f);
    }

    private void ActualizarMarcadorVisual()
    {
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

        if (renderDecenas != null) renderDecenas.sprite = spritesNumeros[decenas];
        if (renderUnidades != null) renderUnidades.sprite = spritesNumeros[unidades];

        if (puntuacionTotal < 10)
        {
            if (renderDecenas != null) renderDecenas.enabled = false;
        }
        else
        {
            if (renderDecenas != null) renderDecenas.enabled = true;
        }
    }
}