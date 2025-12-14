using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escopeta : Pistola
{
    public override void Shoot()
{
    if (muzzle == null)
    {
        Debug.LogError("Muzzle NO asignado en " + gameObject.name);
        return;
    }

    if (particle != null)
    {
        Instantiate(particle, muzzle.position, muzzle.rotation);
    }

    int pellets = 5;
    float spread = 0.2f;

    for (int i = 0; i < pellets; i++)
    {
        Vector3 direction = muzzle.forward + new Vector3(
            Random.Range(-spread, spread),
            Random.Range(-spread, spread),
            Random.Range(-spread, spread)
        );

        RaycastHit hit;
        if (Physics.Raycast(muzzle.position, direction, out hit, 50f))
        {
            DealDamage(hit.collider.gameObject);
        }
    }
}

}
