using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class debugTextItems : MonoBehaviour
{
    public GameObject Player;

    Text debugText;
    GameManager GM;


    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        debugText = this.GetComponent<Text>();
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        debugText.text = ("player facing :" + Player.GetComponent<PlayerController>().A_G_CharacterPawn.forward +"\nmovementVect:(" + new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"))+") | AimVect(" + new Vector2(Input.GetAxis("HorizontalRStick"),Input.GetAxis("VerticalRStick"))+")\n current fire input: " + Input.GetAxis("Fire1") );
        debugText.text += ("\nPlayer health: " + Player.GetComponent<PlayerController>().GS_Health);

    }
}
