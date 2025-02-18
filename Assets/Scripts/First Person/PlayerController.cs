using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

[RequireComponent(typeof(CharacterController))]

public class PlayerController : MonoBehaviour
{

    #region Atributos

    // Variable que representa el movimiento del jugador con WASD.
    private Vector2 input;

    // El controller del personaje que proporciona Unity
    private CharacterController characterController;

    // Variable que representa la direcci�n del jugador
    private Vector3 direction;

    // Valor que representa la velocidad target o m�xima que el jugador puede alcanzar.
    [SerializeField] private float speed;

    [SerializeField] private float rotationSpeed = 500f;
    private Camera mainCamera;


    private float gravity = -9.81f;

    [SerializeField] private float gravityMultiplier = 3.0f;

    // Variable que controla la velocidad de la ca�da del jugador
    private float velocity;

    // Variable que controla c�mo de alto podemos saltar
    [SerializeField] private float jumpPower;

    private int numberOfJumps;
    [SerializeField] private int maxNumberOfJumps;

    #endregion

    #region M�todos definidos por Unity
    /* Cada vez que el objeto est� activo en la escena (Es decir, este c�digo se ejecuta una �nica vez en una escena en la que el objeto
       al que se le adjunta este script est� activo). */
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        mainCamera = Camera.main;
    }

    private void Update()
    {
        
        ApplyRotation();
        ApplyGravity();
        ApplyMovement();

    }

    #endregion


    #region M�todos propios

    private void ApplyGravity()
    {

        if (IsGrounded() && velocity < 0.0f)
        {
            velocity = -1.0f;
        }
        else
        {
            velocity += gravity * gravityMultiplier * Time.deltaTime;
        }


        direction.y = velocity;
    }

    private void ApplyRotation()
    {
        // Esto lo ponemos para que al girar el jugador se quede en la rotaci�n actual
        if (input.sqrMagnitude == 0) return;

        /* L�gica matem�tica encargada de que la rotaci�n del jugador al moverse sea efectiva */
        direction = Quaternion.Euler(0.0f, mainCamera.transform.eulerAngles.y, 0.0f) * new Vector3(input.x, 0.0f, input.y);
        var targetRotation = Quaternion.LookRotation(direction, Vector3.up);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void ApplyMovement()
    {
        // Ejecuta el m�todo Move que est� asociado en el inspector del objeto.
        // Lo multiplicamos por Time.deltaTime para que la velocidad a la que se mueva no est� relacionada con el framerate.
        characterController.Move(direction * speed * Time.deltaTime);
    }

    #endregion


    #region Acciones del jugador

    // La l�gica para que el personaje avance.
    public void Move(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
        direction = new Vector3(input.x, 0.0f, input.y);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (!context.started) return;

        if (!IsGrounded() && numberOfJumps >= maxNumberOfJumps) return;

        if (numberOfJumps == 0) StartCoroutine(WaitForLanding());

        numberOfJumps++;

        velocity = jumpPower / numberOfJumps;
    }

    #endregion

    #region M�todos privados

    private IEnumerator WaitForLanding()
    {
        yield return new WaitUntil(() => !IsGrounded());
        yield return new WaitUntil(IsGrounded);
        // La l�nea de arriba es equivalente a:
        // while (!IsGrounded()) yield return null

        numberOfJumps = 0;
    }


    private bool IsGrounded() => characterController.isGrounded;

    #endregion
}