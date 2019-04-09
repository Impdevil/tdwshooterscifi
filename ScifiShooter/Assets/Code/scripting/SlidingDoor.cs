using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingDoor : MonoBehaviour
{
    public Vector3 closed, Open;
    public bool toggleDoor,locked;
    public float speed;
    
    Transform door;



    // Start is called before the first frame update
    void Start()
    {
        door = this.transform.Find("DoorModel");

    }



    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !locked)
        {
            StopCoroutine("MoveDoor");
            StartCoroutine(MoveDoor(Open));
            //door.transform.position = Vector3.MoveTowards(final, start, Time.deltaTime * speed);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player" && !toggleDoor && !locked)
        {
            StopCoroutine("MoveDoor");
            StartCoroutine(MoveDoor(closed));
            //door.transform.position = Vector3.MoveTowards(start, final, Time.deltaTime * speed);
        }
    }
    IEnumerator MoveDoor(Vector3 moveToo)
    {
        Vector3 curr_pos = door.transform.position;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime*speed;
            door.transform.position = Vector3.Slerp(curr_pos, moveToo,t);
            yield return null;
        }
    }
}
