using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int life = 50;
    public float speed = 3f;
    public float detectionRange = 10f;

    [Header("Patrulla")]
    public float patrolRadius = 5f;

    [Header("Ataque")]
    public int damage = 10;
    public float attackDistance = 1.5f;
    public float attackCooldown = 1f;

    GameObject player;
    Vector3 patrolTarget;
    float lastAttackTime;

    void Start()
    {
        SetNewPatrolPoint();
    }

    void Update()
    {
        DetectPlayer();

        if (player != null)
            MoveToPlayer();
        else
            Patrol();
    }

    // ---------------- DETECTAR PLAYER ----------------
    void DetectPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");


        float closest = Mathf.Infinity;
        player = null;

        foreach (GameObject p in players)
        {
            Vector3 a = transform.position;
            Vector3 b = p.transform.position;
            a.y = b.y = 0;

            float d = Vector3.Distance(a, b);

            if (d <= detectionRange && d < closest)
            {
                closest = d;
                player = p;
            }
        }
    }

    // ---------------- PATRULLA ----------------
    void Patrol()
    {
        Vector3 dir = patrolTarget - transform.position;
        dir.y = 0;

        if (dir.magnitude < 0.5f)
            SetNewPatrolPoint();

        transform.position += dir.normalized * speed * Time.deltaTime;
        Rotate(dir);
    }

    void SetNewPatrolPoint()
    {
        Vector2 rnd = Random.insideUnitCircle * patrolRadius;
        patrolTarget = new Vector3(
            transform.position.x + rnd.x,
            transform.position.y,
            transform.position.z + rnd.y
        );
    }

    // ---------------- PERSEGUIR + ATACAR ----------------
    void MoveToPlayer()
    {
        Vector3 dir = player.transform.position - transform.position;
        dir.y = 0;

        float dist = dir.magnitude;

        if (dist > attackDistance)
        {
            transform.position += dir.normalized * speed * Time.deltaTime;
        }
        else
        {
            Attack();
        }

        Rotate(dir);
    }

    void Attack()
    {
        if (Time.time < lastAttackTime + attackCooldown)
            return;

        SimpleMovement playerLife = player.GetComponent<SimpleMovement>();
        if (playerLife != null)
        {
            playerLife.TakeDamage(damage);
            lastAttackTime = Time.time;
        }
    }

    // ---------------- ROTAR ----------------
    void Rotate(Vector3 dir)
    {
        if (dir != Vector3.zero)
        {
            Quaternion rot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, 10f * Time.deltaTime);
        }
    }

    // ---------------- DAÑO ----------------
    public void GetDamage(int damage)
    {
        life -= damage;
        if (life <= 0)
            Destroy(gameObject);
    }
}
