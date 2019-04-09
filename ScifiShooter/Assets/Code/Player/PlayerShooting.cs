using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerShooting : MonoBehaviour
{

    public Transform G_FireFrom
    {
        get { return firefrom; }
    }
    public Weapon curr_weapon;
    public GameManager GM;
    float btwnShots;
    bool fired;
    CharacterStance characterStance;
    Mesh curr_mesh;
    Transform firefrom;
    
    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        firefrom = this.transform.Find("ShotSpawn");
        if (curr_weapon != null)
        {
            if (curr_weapon.name == "DebugWaepon")
            { }
            else
            {
                firefrom.localPosition = curr_weapon.fireFrom.position; 
            }

        }

    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.Locked)
        {
            if (curr_weapon != null)
            {
                if (fired)
                {
                    btwnShots += Time.deltaTime;
                    if (btwnShots > curr_weapon.fireRate)
                    {
                        fired = false;
                        btwnShots = 0;
                    }
                }
                curr_weapon.UpdateItem();
                if (Input.GetButton("Fire1") && !fired)
                {
                    curr_weapon.FireWeapon(this);
                    fired = true;
                }
            }
        }
    }
}
