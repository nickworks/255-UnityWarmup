using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Powers_Restart : MonoBehaviour
{
    private void Start()
    {
        gameObject.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(OnClick);
    }


    // Update is called once per frame
    void OnClick()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
