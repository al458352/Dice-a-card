using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class DadoController : MonoBehaviour
{

    private Vector3 posicionOriginal;
    public float elevacionY = 0.5f;
    public bool estaSeleccionado = false;

    [Header("Configuración del Dado")]
    public int carasDelDado = 6;

    [Header("Gráficos del Dado")]
    public Sprite[] carasSprites; 

    private SpriteRenderer spriteRenderer;
    public bool estaRodando = false;

    void Start()
    {
        posicionOriginal = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();

 
    }

    void OnMouseDown()
    {

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

    
    public int TirarDado()
    {
        if (estaRodando) return 0;

        int resultado = Random.Range(1, carasDelDado + 1);

        StartCoroutine(RutinaRodar(resultado));

        return resultado;
    }

    private IEnumerator RutinaRodar(int resultadoFinal)
    {
        estaRodando = true;

        int iteraciones = 20;

        for (int i = 0; i < iteraciones; i++)
        {

            if (carasSprites != null && carasSprites.Length > 0)
            {
                int randomIndex = Random.Range(0, carasSprites.Length);
                spriteRenderer.sprite = carasSprites[randomIndex];
            }

            yield return new WaitForSeconds(0.05f);
        }

        ActualizarSprite(resultadoFinal);

        estaRodando = false;
    }

    private void ActualizarSprite(int valor)
    {
        if (carasSprites != null && carasSprites.Length >= valor)
        {
            spriteRenderer.sprite = carasSprites[valor - 1];
        }
        else
        {
            Debug.LogWarning("Faltan sprites en el array carasSprites o el valor sacado es mayor que los sprites disponibles.");
        }
    }
}