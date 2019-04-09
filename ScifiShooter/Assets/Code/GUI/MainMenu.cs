using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public int speed;
    GameObject Menu_canvases;
    public Camera cam;
    bool doOnce;
    public EventSystem GrabEventSystem;

    // Start is called before the first frame update
    void Start()
    {

        GrabEventSystem = EventSystem.current;
        Menu_canvases = this.gameObject;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        Debug.Log("load start");
        SceneManager.LoadScene("DEBUG_halls");
    }
    public void GoToOptions()
    {if (!doOnce)
        {
            
            doOnce = true;
            StopCoroutine("RotateCam");
            Debug.Log("GoToOptions");
            Menu_canvases.transform.GetChild(3).gameObject.SetActive(false);
            StopCoroutine("RotateCam");
            StartCoroutine(RotateCam(90));
            GrabEventSystem.SetSelectedGameObject(Menu_canvases.transform.GetChild(2).gameObject);
        }

    }
    public void GoToMainMenu()
    {
        if (!doOnce)
        {
            
            doOnce = true;
            StopCoroutine("RotateCam");
            Debug.Log("GoToMainMenu");
            Menu_canvases.transform.GetChild(3).gameObject.SetActive(true);
            Menu_canvases.transform.GetChild(2).gameObject.SetActive(true);
            StartCoroutine(RotateCam(0));
            GrabEventSystem.SetSelectedGameObject(Menu_canvases.transform.GetChild(1).gameObject);
        }
    }
    public void GoToStartGame()
    {
        if (!doOnce)
        {
            
            doOnce = true;
            StopCoroutine("RotateCam");
            Debug.Log("GoToStartGame");
            Menu_canvases.transform.GetChild(2).gameObject.SetActive(false);
            StartCoroutine(RotateCam(-90));
            GrabEventSystem.SetSelectedGameObject(Menu_canvases.transform.GetChild(3).gameObject);
        }
    }


    public void QuitGame()
    {
        Debug.Log("Kill game");
    }

    IEnumerator RotateCam(float angle )
    {
        Transform StartPos = cam.transform;
        float t = 0;
        Quaternion newRotation = Quaternion.Euler(0, angle, 0);
        
        while (t <1f)
        {
            t += Time.deltaTime * speed;
            cam.transform.rotation = Quaternion.Slerp(StartPos.rotation, newRotation, t);


            yield return null;
        }
        doOnce = false;
        t = 0;
    }
}
