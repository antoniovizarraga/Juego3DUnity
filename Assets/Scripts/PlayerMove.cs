using JetBrains.Annotations;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class PlayerMove : MonoBehaviour
{

    // Variables relacionadas con el movimiento de John
    private Rigidbody rb;

    private float horizontal;

    private float vertical;

    // La velocidad a la que se mueve John.
    [SerializeField]
    private float speed;

    // Valor que comprueba si John est� en el suelo.
    private bool isGrounded;


    private int puntos;

    // Referencia a la c�mara principal
    [SerializeField]
    private Camera mainCamera;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main; // Obtenemos la c�mara principal
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        // Calculamos la direcci�n del movimiento en funci�n de la c�mara
        Vector3 cameraForward = mainCamera.transform.forward;
        Vector3 cameraRight = mainCamera.transform.right;

        // Ignoramos el componente Y de la c�mara (para evitar que el personaje se mueva hacia arriba o abajo)
        cameraForward.y = 0;
        cameraRight.y = 0;

        // Normalizamos las direcciones para evitar que el movimiento sea m�s r�pido en diagonal
        cameraForward = cameraForward.normalized;
        cameraRight = cameraRight.normalized;

        // Creamos el movimiento en funci�n de las entradas del usuario y la c�mara
        Vector3 movement = (cameraRight * horizontal + cameraForward * vertical).normalized;

        rb.AddForce(movement * speed);

        


        // Jump when space is pressed
        if (Input.GetKeyDown("space") && isGrounded)
        {
            rb.AddForce(Vector3.up * 25, ForceMode.Impulse);
        }

    }

    void OnCollisionEnter(Collision toque)
    {
        if (toque.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Moneda"))
        {
            puntos++;
        }
    }
    void OnCollisionExit(Collision toque)
    {
        isGrounded = false;
    }
}
