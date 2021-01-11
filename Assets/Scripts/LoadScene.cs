using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [SerializeField] private string _sceneToLoad;
    [SerializeField] private string _exitPoint;
    private PlayerController _player;

    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "player")
        {
            SceneManager.LoadScene(_sceneToLoad, LoadSceneMode.Single);
            _player.startingPoint = _exitPoint;
        }
    }
}
