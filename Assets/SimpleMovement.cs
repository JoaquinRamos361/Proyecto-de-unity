using UnityEngine;

public class SimpleMovement : MonoBehaviour
{
    public CharacterController controller;

    [Header("Movimiento")]
    public float speed = 5f;
    public float runSpeed = 9f;
    public float gravity = -20f;
    public float jumpHeight = 2f;

    Vector3 velocity;

    [Header("Armas")]
    public Transform hand;          // Empty donde se equipa el arma
    public Armas armaActual;        // Arma equipada (componente)

    void Update()
    {
        Movimiento();
        GestionArmas();
    }

    // ---------------- Movimiento ----------------
    void Movimiento()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : speed;
        controller.Move(move * currentSpeed * Time.deltaTime);

        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    // ---------------- Gestión de armas ----------------
    void GestionArmas()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Click detectado");

            if (armaActual != null)
            {
                Debug.Log("Arma equipada: " + armaActual.gameObject.name);
                armaActual.Shoot();
            }
            else
            {
                Debug.Log("No hay arma equipada");
            }
        }
    }

    // ---------------- Pick up de armas ----------------
    private void OnTriggerEnter(Collider other)
    {
        WeaponPickup pickup = other.GetComponent<WeaponPickup>();

        if (pickup != null)
        {
            Debug.Log("Pickup detectado: " + pickup.gameObject.name);
            EquiparArma(pickup);
        }
    }

    void EquiparArma(WeaponPickup pickup)
    {
        // Si ya había un arma equipada, la destruimos
        if (armaActual != null|| pickup==null)
        {
            Debug.Log("Destruyendo arma anterior: " + armaActual.gameObject.name);
            Destroy(armaActual.gameObject);
        }

        // Instanciar el arma equipada
        Armas nuevaArma = Instantiate(
            pickup.weaponPrefab,
            hand.position,
            hand.rotation
        );

        nuevaArma.transform.SetParent(hand);
        nuevaArma.transform.localPosition = Vector3.zero;
        nuevaArma.transform.localRotation = Quaternion.Euler(0f, 0f, 260f);

        armaActual = nuevaArma;

        Debug.Log("Nueva arma equipada: " + armaActual.gameObject.name);

        // Destruir el pickup del suelo
        Destroy(pickup.gameObject);
    }
}
