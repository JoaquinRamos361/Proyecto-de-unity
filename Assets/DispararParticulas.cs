using UnityEngine;

public class DispararParticulas : MonoBehaviour
{
    public ParticleSystem particulas;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Click izquierdo
        {
            if (particulas != null)
            {
                particulas.Play();
            }
        }
    }
}
