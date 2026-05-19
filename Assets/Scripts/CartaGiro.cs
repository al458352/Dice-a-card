using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class CartaGiro : MonoBehaviour
{
    [Header("Configuración de Sprites")]
    public Sprite spriteFrente;  
    public Sprite spriteReverso; 

    [Header("Configuración del Giro")]
    public float duracionGiro = 0.4f;    
    public bool empiezaBocaAbajo = true;  

    private SpriteRenderer spriteRenderer;
    private bool estaGirando = false;
    private bool estaBocaArriba = true;
    private Vector3 escalaOriginal;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        escalaOriginal = transform.localScale;

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


    private void OnMouseDown()
    {
        VoltearCarta();
    }

    public void VoltearCarta()
    {
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

        while (tiempo < mitadDelTiempo)
        {
            tiempo += Time.deltaTime;
            float progreso = tiempo / mitadDelTiempo;

            float nuevaX = Mathf.Lerp(escalaOriginal.x, 0f, progreso);
            transform.localScale = new Vector3(nuevaX, escalaOriginal.y, escalaOriginal.z);

            yield return null;
        }

        estaBocaArriba = !estaBocaArriba;
        spriteRenderer.sprite = estaBocaArriba ? spriteFrente : spriteReverso;

        tiempo = 0f;
        while (tiempo < mitadDelTiempo)
        {
            tiempo += Time.deltaTime;
            float progreso = tiempo / mitadDelTiempo;

            float nuevaX = Mathf.Lerp(0f, escalaOriginal.x, progreso);
            transform.localScale = new Vector3(nuevaX, escalaOriginal.y, escalaOriginal.z);

            yield return null;
        }

        transform.localScale = escalaOriginal;
        estaGirando = false;
    }
}