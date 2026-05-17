using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class DadoController : MonoBehaviour
{
    // --- VARIABLES DE SELECCIÓN ---
    private Vector3 posicionOriginal;
    public float elevacionY = 0.5f;
    public bool estaSeleccionado = false;

    // --- VARIABLES DEL DADO ---
    [Header("Configuración del Dado")]
    public int carasDelDado = 6;

    [Header("Gráficos del Dado")]
    public Sprite[] carasSprites; // Arrastra aquí tus sprites desde el Inspector

    private SpriteRenderer spriteRenderer;
    public bool estaRodando = false; // Bloquea interacciones mientras rueda

    void Start()
    {
        posicionOriginal = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Opcional: Si prefieres cargar los sprites por código en vez de arrastrarlos al Inspector, 
        // descomenta la siguiente línea (asegúrate de que la ruta sea correcta en la carpeta Resources).
        // carasSprites = Resources.LoadAll<Sprite>("dice sides/dadospritesheet24x24");
    }

    // --- LÓGICA DE SELECCIÓN ---
    void OnMouseDown()
    {
        // Si el dado está rodando, ignoramos el clic
        if (estaRodando) return;

        if (GameManager.Instance.dadoSeleccionado != null && GameManager.Instance.dadoSeleccionado != this)
        {
            GameManager.Instance.dadoSeleccionado.Deseleccionar();
        }

        if (!estaSeleccionado)
        {
            Seleccionar();
        }
        else
        {
            Deseleccionar();
        }
    }

    public void Seleccionar()
    {
        estaSeleccionado = true;
        GameManager.Instance.dadoSeleccionado = this;
        transform.position = posicionOriginal + new Vector3(0, elevacionY, 0);
    }

    public void Deseleccionar()
    {
        estaSeleccionado = false;
        if (GameManager.Instance.dadoSeleccionado == this)
        {
            GameManager.Instance.dadoSeleccionado = null;
        }
        transform.position = posicionOriginal;
    }

    // --- LÓGICA DE TIRAR EL DADO ---
    public int TirarDado()
    {
        // Evitamos tirar el dado si ya está rodando
        if (estaRodando) return 0;

        // 1. Calculamos el resultado final al instante
        int resultado = Random.Range(1, carasDelDado + 1);

        // 2. Iniciamos la animación visual de 1 segundo pasándole el resultado
        StartCoroutine(RutinaRodar(resultado));

        // 3. Devolvemos el resultado por si el GameManager lo necesita procesar ya mismo
        return resultado;
    }

    // --- ANIMACIÓN VISUAL ---
    private IEnumerator RutinaRodar(int resultadoFinal)
    {
        estaRodando = true;

        // 20 iteraciones * 0.05 segundos = 1 segundo exacto de duración
        int iteraciones = 20;

        for (int i = 0; i < iteraciones; i++)
        {
            // Muestra una cara aleatoria solo como efecto visual
            if (carasSprites != null && carasSprites.Length > 0)
            {
                int randomIndex = Random.Range(0, carasSprites.Length);
                spriteRenderer.sprite = carasSprites[randomIndex];
            }

            // Espera 0.05 segundos antes de cambiar al siguiente sprite
            yield return new WaitForSeconds(0.05f);
        }

        // Una vez pasa el segundo, aplicamos el sprite del resultado real
        ActualizarSprite(resultadoFinal);

        estaRodando = false;
    }

    private void ActualizarSprite(int valor)
    {
        if (carasSprites != null && carasSprites.Length >= valor)
        {
            // Arrays en C# empiezan en 0, por eso restamos 1 al valor (el valor 1 es el índice 0)
            spriteRenderer.sprite = carasSprites[valor - 1];
        }
        else
        {
            Debug.LogWarning("Faltan sprites en el array carasSprites o el valor sacado es mayor que los sprites disponibles.");
        }
    }
}