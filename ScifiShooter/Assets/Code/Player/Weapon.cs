using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum CharacterStance
{
    singlehand,
    multihanded,
    Shoulder

};

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Inventory/Weapons")]
public class Weapon : ScriptableObject
{
    //data
    public float fireRate;
    public int clipSize, ammo;
    public GameObject ammoType;
    public int damage;
    
    //graphics
    public Sprite SpriteSheet;
    public Transform fireFrom, placement;
    public CharacterStance characterStance;
    public AudioClip fireSound;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void UpdateItem()
    {

    }

    public void FireWeapon(PlayerShooting parent)
    {
        GameObject tempB = Instantiate(ammoType, parent.G_FireFrom.transform.position, parent.transform.rotation) as GameObject;
        tempB.GetComponent<Ammo>().damage = damage;

        //spawn a shot at the end of the gun

    }
}
    