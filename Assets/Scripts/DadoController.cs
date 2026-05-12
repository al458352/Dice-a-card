using UnityEngine;

public class DadoController : MonoBehaviour
{
    private Vector3 posicionOriginal;
    public float elevacionY = 0.5f; // Cuánto subirá el dado
    public bool estaSeleccionado = false;

    [Header("Configuración del Dado")]
    // ¡Aquí está el truco! Creamos una variable para las caras.
    public int carasDelDado = 6;

    void Start()
    {
        posicionOriginal = transform.position;
    }

    void OnMouseDown()
    {
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
        // Random.Range en números enteros SIEMPRE excluye el último número.
        // Si queremos que salga del 1 al 6, hay que poner (1, 7).
        // Por eso sumamos + 1 a nuestras carasDelDado.
        int resultado = Random.Range(1, carasDelDado + 1);
        return resultado;
    }
}