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

    void OnEnable()
{
    life = 100;
}


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




}
