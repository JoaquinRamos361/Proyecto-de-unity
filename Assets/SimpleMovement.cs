using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class SimpleMovement : MonoBehaviour
{
    public CharacterController controller;

    [Header("Movimiento")]
    public float speed = 5f;
    public float runSpeed = 9f;
    public float gravity = -20f;
    public float jumpHeight = 2f;

    Vector3 velocity;

    [Header("Vida")]
    public int life = 100;

    [Header("Armas")]
    public Transform hand;
    public Armas armaActual;
    public Armas pistola;
    public Armas rifle;
    public Armas escopeta;
    [Header("UI")]
public GameObject crosshair;



    void OnEnable()
{
    life = 100;
}

        void Start()
{
    Cursor.lockState = CursorLockMode.Locked;
    Cursor.visible = false;
}

    

    void Update()
    {
        Movimiento();
        GestionArmas();
        CambiarConTeclas();

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
            velocity.y = -2f;

        if (Input.GetButtonDown("Jump") && controller.isGrounded)
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    // ---------------- Gestión de armas ----------------
    void GestionArmas()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (armaActual != null)
                armaActual.Shoot();
        }
    }

    // ---------------- DAÑO ----------------
    public void TakeDamage(int damage)
    {
        life -= damage;
        Debug.Log("Vida del jugador: " + life);

        if (life <= 0)
            Die();
    }

  void Die()
{
    Debug.Log("Jugador muerto");
    SceneManager.LoadScene("Menu");
}


IEnumerator DeathRoutine()
{
    Destroy(gameObject);          // destruye el Player
    yield return null;            // espera 1 frame
    SceneManager.LoadScene("Menu");
}
void GoToMenu()
{
    SceneManager.LoadScene("Menu");
}

public void EquipWeapon(Armas nuevaArma)
{
    if (nuevaArma is Pistola)
        pistola = nuevaArma;
    else if (nuevaArma is Rifle)
        rifle = nuevaArma;
    else if (nuevaArma is Escopeta)
        escopeta = nuevaArma;

    nuevaArma.Equip(hand);
    nuevaArma.gameObject.SetActive(false);

    // 👉 ACTIVAR LA MIRA
    if (crosshair != null && !crosshair.activeSelf)
        crosshair.SetActive(true);
}



void CambiarArma(Armas nuevaArma)
{
    if (armaActual != null)
        armaActual.gameObject.SetActive(false);

    armaActual = nuevaArma;
    armaActual.gameObject.SetActive(true);
}

void CambiarConTeclas()
{
    if (Input.GetKeyDown(KeyCode.Alpha1) && pistola != null)
        CambiarArma(pistola);

    if (Input.GetKeyDown(KeyCode.Alpha2) && rifle != null)
        CambiarArma(rifle);

    if (Input.GetKeyDown(KeyCode.Alpha3) && escopeta != null)
        CambiarArma(escopeta);
}




}
