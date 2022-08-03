using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] RayCastWeapon RayCastWeapon;
    [SerializeField] PrefabWeapon PrefabWeapon;

    public enum ShootingMode
    {
        Shootgun = 0, Raycast = 1
    }

    private int shootingMode = 1;
    public void SetShootingMode()
    {
        if (shootingMode == 0) { shootingMode = 1; }
        else if (shootingMode == 1) { shootingMode = 0; }

        switch (shootingMode)
        {
            case (int)ShootingMode.Shootgun:
                shootingMode = (int)ShootingMode.Shootgun;

                RayCastWeapon.enabled = false;
                PrefabWeapon.enabled = true;
                break;

            case (int)ShootingMode.Raycast:
                shootingMode = (int)ShootingMode.Raycast;

                RayCastWeapon.enabled = true;
                PrefabWeapon.enabled = false;
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        RayCastWeapon = this.gameObject.GetComponent<RayCastWeapon>();
        PrefabWeapon = gameObject.GetComponent<PrefabWeapon>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Z"))
        {
            Debug.Log("Z");
            SetShootingMode();
        }


        if (Input.GetButtonDown("Fire1"))
        {
            switch (shootingMode)
            {
                case (int)ShootingMode.Raycast:
                    RayCastWeapon.Shoot();
                    break;

                case (int)ShootingMode.Shootgun:
                    PrefabWeapon.Shoot();
                    break;
            }
        }
    }
}
