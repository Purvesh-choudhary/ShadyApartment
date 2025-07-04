using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DimensionSwap : MonoBehaviour
{

    [SerializeField] GameObject cam_Real_Realm;
    [SerializeField] Image shadow,real;
    [SerializeField] KeyCode swapKey = KeyCode.RightShift;
    [SerializeField] bool isReal = true;

    // Start is called before the first frame update
    void Start()
    {
        cam_Real_Realm = GameObject.FindWithTag("Cam2");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(swapKey)){
            swapWorld();
        }
    }

    void swapWorld(){
        isReal = !isReal;
        if(isReal){
            cam_Real_Realm.SetActive(true);
            real.enabled = true;
            shadow.enabled = false;
        }else{
            cam_Real_Realm.SetActive(false);
            real.enabled = false;
            shadow.enabled = true;
        }
    }
}
