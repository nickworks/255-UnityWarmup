using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powers_Scoring : MonoBehaviour
{
    public enum Value { score, seconds }

    public UnityEngine.UI.Text ui;
    public string header;
    public Value value;
    public Powers_Game gameManager;

    // Update is called once per frame
    void Update()
    {
        if (value == Value.score) ui.text = header + gameManager.time;
        else if (value == Value.seconds) ui.text = header + gameManager.seconds + " SECONDS!";
    }
}
