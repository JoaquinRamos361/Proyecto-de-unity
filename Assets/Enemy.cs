using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int life = 50;
    public float speed = 3f;

    private GameObject player;

    void Update()
    {
        FindClosestPlayer();
        MoveToPlayer();
    }

    void FindClosestPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        float closestDistance = Mathf.Infinity;
        player = null;

        foreach (GameObject p in players)
        {
            SimpleMovement sm = p.GetComponent<SimpleMovement>();
            if (sm == null) continue;

            // Si tu jugador tiene variable dead, descomenta esto
            // if (sm.dead) continue;

            float distance = Vector3.Distance(p.transform.position, transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                player = p;
            }
        }
    }

    void MoveToPlayer()
    {
        if (player == null) return;

        Vector3 direction = (player.transform.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        // Opcional: mirar al jugador
        transform.LookAt(player.transform);
    }

    public void GetDamage(int damage)
    {
        life -= damage;
        if (life <= 0)
            Destroy(gameObject);
    }
}

