using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistola : Armas
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

    RaycastHit hit;
    if (Physics.Raycast(muzzle.position, muzzle.forward, out hit, 100f))
    {
        DealDamage(hit.collider.gameObject);
    }
}
}

