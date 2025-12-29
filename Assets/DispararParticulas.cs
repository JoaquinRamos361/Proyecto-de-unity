using UnityEngine;

public class DispararParticulas : MonoBehaviour
{
    public ParticleSystem particulaPrefab;
    public Camera cam;
    public LayerMask hitLayers;

    void Start()
    {
        if (cam == null)
            cam = Camera.main; // 🔥 se asigna sola
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, 165f, hitLayers))
            {
                Instantiate(particulaPrefab, hit.point, Quaternion.identity);

                Enemy enemy = hit.collider.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.GetDamage(10);
                }
            }
        }
    }
}


