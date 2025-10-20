using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlJugador : MonoBehaviour
{
    //movimiento personaje o jugador
    public float velocidad = 5f;//velocidad del jugador movimiento
    public float gravedad = -9.8f;//gravedad del jugador 
    private CharacterController controller;//controlador del juego registro de movimineto
    private Vector3 velocidadVertical;// que tan rapido cae el jugador en movimiento

    //Variables de la vista
    public Transform camara;//registra que camara funcionan como vision o ojos del jugador
    public float sensibilidadMouse = 200f;//se refiere a qué tan rápido se mueve el cursor en la pantalla en relación con el movimiento físico del ratón
    private float rotacionXVertical = 0f;//indica a cuantos grados ve o voltea a diferentes direcciones el jugador

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();

        //esta linea bloquea el puntero del mouse en los limites de la pantalla
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        ManejadorVista();
        ManejadorMovimiento();
    }
    //Nuevo metodo manejador de vista
    void ManejadorVista()
    {
        //leer el input del mouse
        float mouseX = Input.GetAxis("Mouse X") * sensibilidadMouse * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensibilidadMouse * Time.deltaTime;

        //construir la rotacion horizontal
        transform.Rotate(Vector3.up * mouseX);

        //registro de la rotacion vertical
        rotacionXVertical -= mouseY;

        //limitar la rotacion vertical
        Mathf.Clamp(rotacionXVertical, -90f, 90f);//limito tu vision noventa grados

        //aplicar la rotacion
        camara.localRotation = Quaternion.Euler(rotacionXVertical, 0, 0);
    }
    //Nuevo metodo manejador movimiento
    void ManejadorMovimiento()
    {
        //leer el input de movimiento (registra el movimiento WASD O LAS FLECHAS DE DIRECCION)
        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");

        //crear el vector de movimiento
        //se almnacena de forma local el registro de direccion de movimiento
        Vector3 direccion = transform.right * inputX + transform.forward * inputZ;

        //mover el character contrtoller
        controller.Move(direccion * velocidad * Time.deltaTime);

        //aplicar la gravedad
        //registro si estoy en el piso para un futuro comportamiento de salto
        if(controller.isGrounded && velocidadVertical.y < 0)
        {
            velocidadVertical.y = -2f;//una pequeña fuerza hacia abajo para mantenerlo pegado al piso
        }

        //aplicamos la aceleracion de la gravedad
        velocidadVertical.y += gravedad * Time.deltaTime;

        //movemos el controlador hacia abajo
        controller.Move(velocidadVertical * Time.deltaTime);
    }
}