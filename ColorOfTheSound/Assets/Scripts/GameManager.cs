using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance = null;
    public static GameManager Instance => _instance;


    public InputController Controller { get; private set; }
    
    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);

            Controller = new InputController();
        }
    }

    private void OnEnable()
    {
        Controller.EnableGameplayInput();
    }
}
