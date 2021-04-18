using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController cc;
    public float Velocidad = 12;

    public float Gravedad = -9.81f;
    public Vector3 velocity;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask floorMask;
    bool isGrounded;




    public Camera camara;
    RaycastHit hit;
    public float distancia = 10f;
    public GameObject Linterna;

    void Inspection()
    {
        bool rayDidHit = Physics.Raycast(
            camara.transform.position, //Punto de origen
            camara.transform.forward, //Direccion de disparo
            out hit, // Variable donde se almacena del objeto colisionado
            distancia // Distancia del rayo
            );
        if (rayDidHit)
        {
            string tagObj = hit.transform.tag;
            if(tagObj == "Power")
            {
                TorchBehavior linterna = Linterna.GetComponent<TorchBehavior>();
                linterna.BatteryRefill();
                BatteryBehavior Bateria = hit.transform.GetComponent<BatteryBehavior>();
                Bateria.Taken();
                Debug.Log("Bateria recargada");
            }
        }
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, floorMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(3 * -2 * Gravedad);
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        cc.Move(move * Velocidad * Time.deltaTime);


        velocity.y += Gravedad * Time.deltaTime;
        cc.Move(velocity * Time.deltaTime);
        if (Input.GetKeyDown("e"))
        {
            Inspection();
        }
    }
}
