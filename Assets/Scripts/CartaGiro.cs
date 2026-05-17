using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class CartaGiro : MonoBehaviour
{
    [Header("Configuración de Sprites")]
    public Sprite spriteFrente;  // Arrastra aquí la cara con los dibujos/stats
    public Sprite spriteReverso; // Arrastra aquí la parte trasera de la carta

    [Header("Configuración del Giro")]
    public float duracionGiro = 0.4f;      // Tiempo total que tarda en darse la vuelta
    public bool empiezaBocaAbajo = true;   // Si está en true, se ocultará al darle al Play

    private SpriteRenderer spriteRenderer;
    private bool estaGirando = false;
    private bool estaBocaArriba = true;
    private Vector3 escalaOriginal;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        escalaOriginal = transform.localScale;

        // Configuramos el estado inicial de la carta al arrancar el juego
        if (empiezaBocaAbajo)
        {
            spriteRenderer.sprite = spriteReverso;
            estaBocaArriba = false;
        }
        else
        {
            spriteRenderer.sprite = spriteFrente;
            estaBocaArriba = true;
        }
    }

    // --- ESTO ES SOLO PARA PROBARLO HACIENDO CLIC ---
    // (Necesitas que la carta tenga un BoxCollider2D para que detecte el clic)
    private void OnMouseDown()
    {
        VoltearCarta();
    }
    // ------------------------------------------------

    public void VoltearCarta()
    {
        // Evitamos que se buguee si le hacemos spam de clics
        if (!estaGirando)
        {
            StartCoroutine(RutinaGiro());
        }
    }

    private IEnumerator RutinaGiro()
    {
        estaGirando = true;
        float tiempo = 0f;
        float mitadDelTiempo = duracionGiro / 2f;

        // 1. APLASTAMOS LA CARTA (Efecto de girar hacia el centro)
        while (tiempo < mitadDelTiempo)
        {
            tiempo += Time.deltaTime;
            float progreso = tiempo / mitadDelTiempo;

            // Lerp va de la escala X original hasta 0
            float nuevaX = Mathf.Lerp(escalaOriginal.x, 0f, progreso);
            transform.localScale = new Vector3(nuevaX, escalaOriginal.y, escalaOriginal.z);

            yield return null;
        }

        // 2. CAMBIAMOS LA IMAGEN JUSTO CUANDO NO SE VE (Escala X = 0)
        estaBocaArriba = !estaBocaArriba;
        spriteRenderer.sprite = estaBocaArriba ? spriteFrente : spriteReverso;

        // 3. EXPANDIMOS LA CARTA (Termina el giro)
        tiempo = 0f;
        while (tiempo < mitadDelTiempo)
        {
            tiempo += Time.deltaTime;
            float progreso = tiempo / mitadDelTiempo;

            // Lerp va de 0 hasta la escala X original
            float nuevaX = Mathf.Lerp(0f, escalaOriginal.x, progreso);
            transform.localScale = new Vector3(nuevaX, escalaOriginal.y, escalaOriginal.z);

            yield return null;
        }

        // Nos aseguramos de que quede exactamente en su tamaño original por si hay decimales sueltos
        transform.localScale = escalaOriginal;
        estaGirando = false;
    }
}