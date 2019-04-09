using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    #region GetSets

    public Transform A_G_CharacterPawn
    {
        get { return A_characterPawn; }
    }

    public Rigidbody GS_RB_Player
    {
        get { return RB_player; }
        set { RB_player = value; }
    }
    public int S_healthMax
    {
        set { maxHealth = value; }
    }

    public int GS_Health
    {
        get { return health; }
        set { health = value; }
    }
    public PlayerInventory GS_Inventory
    {
        get {return playerInventory;}
        set {playerInventory = value;}
    }
    #endregion

    GameManager GM;
    public GameObject playerUIElements;

    int health;
    int maxHealth = 15;
    Slider HealthBar;

    
    

    public float movementSpeed = 1;
    Rigidbody RB_player;
    Transform A_characterPawn;
    PlayerShooting playerShooting;
    PlayerInventory playerInventory;
    FieldOfView playerVision;

   

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        RB_player = gameObject.GetComponent<Rigidbody>();
        A_characterPawn = this.transform.Find("CharacterParts");
        GM = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        playerUIElements = GameObject.FindGameObjectWithTag("PlayerUI");
        HealthBar = playerUIElements.transform.Find("HealthBar").GetComponent<Slider>();
        HealthBar.maxValue = maxHealth;
        playerShooting = this.transform.GetChild(1).GetChild(1).GetComponent<PlayerShooting>();


    }

    private void Update()
    {
        HealthBar.value = health;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!GameManager.instance.Locked)
        {
            MovementControl();
            PlayerAim();
        }
    }

    private void PlayerAim()
    {
        if (Input.GetAxis("HorizontalRStick") > 0.1f || Input.GetAxis("VerticalRStick") > 0.1f || Input.GetAxis("HorizontalRStick") < -0.1f || Input.GetAxis("VerticalRStick") < -0.1f)
        {
            Vector3 u_LookDirection = new Vector3(Input.GetAxis("HorizontalRStick"), 0f, -Input.GetAxis("VerticalRStick"));

            u_LookDirection = Vector3.Normalize(u_LookDirection);

            A_characterPawn.LookAt(A_characterPawn.position + u_LookDirection);
        }
    }

    private void MovementControl()
    {
        float u_moveHorizontal = Input.GetAxis("Horizontal");
        float u_moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(u_moveHorizontal, 0.0f, u_moveVertical);

        if (Input.GetButton("Run"))
        {
            movement = movement.normalized * (movementSpeed * 2.5f) * Time.deltaTime;
        }
        else
        {
            movement = movement.normalized * movementSpeed * Time.deltaTime;
        }
         RB_player.MovePosition(transform.position + movement);
    }

    public void DamagePlayer(int damage, string dmgType)
    {
        if(dmgType == "" || dmgType == null)
        {
            health -= damage;
        }

    }
}
public class PlayerInventory
{
    ScriptableObject[] inventoryItems;
    Dictionary<int[,], ScriptableObject> dic_inventoryItems;
    PlayerController _PC;

    public PlayerInventory(PlayerController playerController)
    {
        _PC = playerController;

    }

    public void AddItem (ScriptableObject newItem)
    {
        GameManager.TriggerEvent("itemPickup");
    }
    public void RemoveItem(string nameofItem)
    {

    }
    public void UseItem()
    {

    }
    
}
