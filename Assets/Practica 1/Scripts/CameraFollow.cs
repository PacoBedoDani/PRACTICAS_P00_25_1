using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //referencia al objetivo
    public Transform objetivo;

    //Offset o separacion entre la camara y el objetivo
    public Vector3 offset = new Vector3(0f, 3f, -6f);

    //Variable para activar o desasctivar el segumiento (queda protegida)
    private bool seguir = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        //solo seguir si esta activado y el objetivo esta correctamente referenciado
        if (seguir && objetivo != null)
        {
            //posicionar camara con offset
            transform.position = objetivo.position + offset;
        }
    }
    //metodo para iniciar seguimiento
    public void IniciarSeguimiento()
    {
        seguir = true;
    }
    //metodo para detener segumiemto
    public void DetenerSeguimiento()
    {
        seguir = false;
    }
}
