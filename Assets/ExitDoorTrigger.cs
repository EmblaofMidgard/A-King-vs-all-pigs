using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDoorTrigger : MonoBehaviour
{
    public void ChangeLevel()
    {
        SceneManager.LoadScene("LivelloFine");
    }
    
    
}
