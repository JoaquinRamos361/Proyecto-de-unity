using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armas : MonoBehaviour
{
    [SerializeField] protected GameObject particle; // Partículas para disparo
    protected int damage = 10; // Daño base
    public GameObject particlePrefab;
    [SerializeField] protected Transform muzzle;



    public virtual void Shoot()
    {
        // Este método será sobreescrito por las armas hijas
    }

    protected void DealDamage(GameObject target)
    {
        if (target.CompareTag("enemy"))
        {
            target.GetComponent<Enemy>().GetDamage(damage);
        }
    }
}
