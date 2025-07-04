using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] PlayerController playerController;
    
    private void Awake()
    {
        cam = GameObject.FindWithTag("Cam2").GetComponent<Camera>();
        playerController = gameObject.GetComponent<PlayerController>();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {
        if(cam.gameObject.activeSelf){
            playerController.enabled = false;

        }else{
            playerController.enabled = true;
        }
    }
    

     
}
