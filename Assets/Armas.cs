using UnityEngine;

public class Armas : MonoBehaviour
{
    [SerializeField] protected GameObject particle;
    [SerializeField] protected Transform muzzle;
    protected int damage = 10;

    private Collider col;
    private Rigidbody rb;

    void Awake()
    {
        col = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
    }

    public virtual void Shoot()
    {
        // sobrescrito por hijas
    }

    protected void DealDamage(GameObject target)
    {
        if (target.CompareTag("enemy"))
        {
            target.GetComponent<Enemy>()?.GetDamage(damage);
        }
    }

    // 🔥 CUANDO EL JUGADOR COLISIONA
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SimpleMovement player = other.GetComponent<SimpleMovement>();
            if (player != null)
            {
                player.EquipWeapon(this);
            }
        }
    }

    // 🔹 EQUIPAR
    public void Equip(Transform hand)
    {
        transform.SetParent(hand);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        if (rb != null) rb.isKinematic = true;
        if (col != null) col.enabled = false;
    }

    // 🔹 SOLTAR
    public void Drop()
    {
        transform.SetParent(null);

        if (rb != null) rb.isKinematic = false;
        if (col != null) col.enabled = true;
    }
}

