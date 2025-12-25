using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsController : MonoBehaviour
{[Header("Armas del jugador")]
    public GameObject pistol;
    public GameObject rifle;
    public GameObject shotgun;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        // Detectar arma por TAG
        if (other.CompareTag("Pistol"))
        {
            ActivateWeapon(pistol);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Rifle"))
        {
            ActivateWeapon(rifle);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Shotgun"))
        {
            ActivateWeapon(shotgun);
            Destroy(other.gameObject);
        }
    }
    void ActivateWeapon(GameObject weaponToActivate)
    {
        // Desactivar todas
        pistol.SetActive(false);
        rifle.SetActive(false);
        shotgun.SetActive(false);

        // Activar la seleccionada
        weaponToActivate.SetActive(true);
    }
}
