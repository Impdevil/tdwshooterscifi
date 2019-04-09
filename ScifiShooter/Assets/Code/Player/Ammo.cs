using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public float Speed = 10;
    public float damage = 5;
    Rigidbody RB_shot;

    // Start is called before the first frame update
    void Start()
    {
        RB_shot = this.GetComponent<Rigidbody>();
        RB_shot.velocity = transform.forward * Speed;
        Destroy(this.gameObject, 20);
    }
    private void FixedUpdate()
    {
        RaycastHit tipRay;
        Ray projectionRay = new Ray(transform.position, transform.forward);
        if(Physics.Raycast(projectionRay, out tipRay,.2f))
        {
            Debug.Log(tipRay.transform.name + " hit");
            if (tipRay.transform.tag == "NPC")
            { 
                tipRay.transform.GetComponent<EnemyBase>().DamageSelf((int)damage);
            }
            else
            { }

            Destroy(this.gameObject);
        }
    }
}
