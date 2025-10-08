using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlBola : MonoBehaviour
{
    //variables úblicas hacen referencia a la camara y al score
    public CameraFollow cameraFollow;
    public ScoreManager scoreManager;
    // variables que controlan al objeto bola
    public Rigidbody rb;
    //variables para apuntar
    public float velocidadDeApuntado = 5f;
    public float limiteIzquierdo = -2f;
    public float limiteDerecho = 2f;

    public float fuerzaDeLanzamiento = 1000f;
    //se mejoro el control de flujo de la bola con un bool
    private bool haSidoLanzada = false;

    // Start is called before the first frame update
    void Start()
    {
        // PISTA: Obtener el componente Rigidbody de esta bola
         rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    { //Expresion: muestra que ha sido lanzada si es falso puede disparar
        if (haSidoLanzada ==false)
        {//se invoca el metodo de apuntar
            Apuntar();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //se cambia el valor cuando ya se lanzo la bola
                //se utiliza o invoca el metodo lanzar
                Lanzar();
            }
        }
    }

    //1. Metodo o identificador Apuntar (leer un inputhorizotal de tipo axis te permite registrar
    //entradas con las teclas A y D, y flecha izquierda y flecha derecha
    void Apuntar()
    {
        float inputHorizontal = Input.GetAxis("Horizontal");

        //2. Mover la bola hacia los lados
        transform.Translate(Vector3.right * inputHorizontal * velocidadDeApuntado * Time.deltaTime);

        //3. Delimitar el movimiento de la bola
        //trasnform position me permite saber cual es la posicion actual de la bola dentro de la escena
        Vector3 posicionActual = transform.position;

        posicionActual.x = Mathf.Clamp(posicionActual.x, limiteIzquierdo, limiteDerecho);

        transform.position = posicionActual;
    }

    //Nuevo metodo o identificador (void, verbo, llaves)
    void Lanzar()
    {
        haSidoLanzada = true;

        if (rb != null)
        {
            rb.AddForce(Vector3.forward * fuerzaDeLanzamiento);
        }
        else
        {
            Debug.LogWarning("rb no esta asignado" + gameObject.name);
        }

        //Iniciar seguimiento de la camara si existe
        if (cameraFollow != null) cameraFollow.IniciarSeguimiento();
    }
    private void OnCollisionEnter(Collision collision)
    {
        //Si colisiona con un pino (cuando quiero que algo suceda al contactar con otro objeto)
        if (collision.gameObject.CompareTag("Pin"))
        {
            //Detener seguimiento de camara si no es null
            if (cameraFollow != null)
            {
                cameraFollow.DetenerSeguimiento();
            }
            //Calcular puntaje tras un pequeño retraso
            if (scoreManager != null)
            {
                Invoke("CalcularPuntaje", 0f);
            }
        }
    }

    void CalcularPuntaje()
    {
        //llamar al ScoreManager para ctualizar puntos
        scoreManager.CalcularPuntaje();
    }
} //BIEN BENIDO A LA ENTRADA AL INFIERNO