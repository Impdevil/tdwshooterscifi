using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;


public class GameManager : MonoBehaviour
{
    public GameObject player;
    public List<GameObject> enemies;
    public bool Locked;


    Dictionary<string, UnityEvent> EventDictionary;


    private static GameManager gameManager;
    public static GameManager instance
    {
        get
        {
            if (!gameManager)
            {
                gameManager = FindObjectOfType(typeof(GameManager)) as GameManager;

                if (!gameManager)
                { Debug.Log("you fucked up somewhere, there is no game manager"); }
                else
                    gameManager.InitEventSystem();
            }
            return gameManager;
        }
    }

    void InitEventSystem()
    {
        if (EventDictionary == null)
            EventDictionary = new Dictionary<string, UnityEvent>();
    }
    
    public static void StartListening(string eventName, UnityAction listener)
    {
        UnityEvent thisEvent = null;
        if(instance.EventDictionary.TryGetValue(eventName,out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new UnityEvent();
            thisEvent.AddListener(listener);
            instance.EventDictionary.Add(eventName, thisEvent);

        }
    }

    public static void StopListening(string eventName, UnityAction listener)
    {
        if (gameManager == null) return;
        UnityEvent thisEvent = null;
        if(instance.EventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    public static void TriggerEvent(string eventName)
    {
        UnityEvent thisEvent = null;
        if(instance.EventDictionary.TryGetValue(eventName,out thisEvent))
        {
            thisEvent.Invoke();
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        Input.simulateMouseWithTouches = false;
        Locked = false;
        enemies = new List<GameObject>();
        enemies.AddRange(GameObject.FindGameObjectsWithTag("NPC"));
        player = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        if(!Locked)
        {

        }
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene("DEBUG_halls");
    }
}
