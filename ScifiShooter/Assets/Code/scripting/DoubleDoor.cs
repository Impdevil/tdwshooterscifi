using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleDoor : MonoBehaviour

{
    public Vector3 lClosed, lOpen, rClosed, rOpen;
    public bool toggleDoor, locked;
    public float speed;

    Transform lDoor, rDoor;



    // Start is called before the first frame update
    void Start()
    {
        lDoor = this.transform.Find("DoorModelL");
        rDoor = this.transform.Find("DoorModelR");

    }




    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !locked)
        {
            StopCoroutine("MoveDoor");
            StartCoroutine(MoveDoor(lOpen, lDoor));
            StartCoroutine(MoveDoor(rOpen, rDoor));
            //door.transform.position = Vector3.MoveTowards(final, start, Time.deltaTime * speed);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && !toggleDoor && !locked)
        {
            StopCoroutine("MoveDoor");
            StartCoroutine(MoveDoor(lClosed, lDoor));
            StartCoroutine(MoveDoor(rClosed, rDoor));
            //door.transform.position = Vector3.MoveTowards(start, final, Time.deltaTime * speed);
        }
    }
    IEnumerator MoveDoor(Vector3 moveToo, Transform doorSelect)
    {
        Vector3 curr_pos = doorSelect.transform.position;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * speed;
            doorSelect.transform.position = Vector3.Slerp(curr_pos, moveToo, t);
            yield return null;
        }
    }
}


