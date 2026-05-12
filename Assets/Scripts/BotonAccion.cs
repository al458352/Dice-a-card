using UnityEngine;

public class BotonAccion : MonoBehaviour
{
    // Esto crea un menú desplegable en el Inspector de Unity
    public enum TipoAccion { Tirar, Pasar }

    [Header("¿Qué hace este botón?")]
    public TipoAccion tipoDeBoton;

    void OnMouseDown()
    {
        // 1. Primero comprobamos si el jugador ha seleccionado un dado.
        // Si no hay dado seleccionado, no hacemos nada.
        if (GameManager.Instance.dadoSeleccionado == null)
        {
            Debug.Log("¡Debes seleccionar un dado primero!");
            return; // El 'return' hace que el código se detenga aquí
        }

        // 2. Lógica para el botón de TIRAR (Throw)
        if (tipoDeBoton == TipoAccion.Tirar)
        {
            // Ejecutamos la función TirarDado del dado seleccionado
            int resultadoTirada = GameManager.Instance.dadoSeleccionado.TirarDado();

            // Mostramos el resultado en la consola para comprobar que funciona
            Debug.Log("¡Has tirado el dado y has sacado un " + resultadoTirada + "!");

            // Le enviamos ese número al GameManager para que actualice las imágenes en pantalla
            GameManager.Instance.AñadirPuntuacion(resultadoTirada);

            // Devolvemos el dado a su posición original abajo
            GameManager.Instance.dadoSeleccionado.Deseleccionar();
        }

        // 3. Lógica para el botón de PASAR (Pass)
        else if (tipoDeBoton == TipoAccion.Pasar)
        {
            Debug.Log("Has pasado el turno. (Aquí irá la lógica del enemigo más adelante)");

            // Simplemente deseleccionamos el dado por limpieza
            GameManager.Instance.dadoSeleccionado.Deseleccionar();
        }
    }
}