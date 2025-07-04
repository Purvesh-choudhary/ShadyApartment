using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;
    public int lvlIndex = 1;
    
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play(int startingindex){
        SceneManager.LoadScene(startingindex);
    }

    public void Credit(){
        
    }

    public void Exit(){
        Application.Quit();
    }

    public void lvlComplete(){
        lvlIndex++;
        SceneManager.LoadScene(lvlIndex);

    }

    // IEnumerator  lvlCompleteCoroutine(){
    //     yield return new WaitForSeconds(5);
    //     SceneManager.LoadScene(sceneName:"Lv"+lvlIndex);
    // }


}
