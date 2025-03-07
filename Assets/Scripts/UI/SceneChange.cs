using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;
using JetBrains.Annotations;

public class SceneChange : MonoBehaviour
{

    
   public void changeScene( string sceneName)
    {
        
     //Debug.Log(sceneName);   
        SceneManager.LoadScene(sceneName);
    }
}
