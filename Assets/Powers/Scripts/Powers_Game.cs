using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powers_Game : MonoBehaviour
{

    public float playerHealth = 100;
    public UnityEngine.UI.Image healthBar;
    public UnityEngine.UI.Text score;
    public GameObject obstacle;
    public GameObject realtimeUI;
    public GameObject gameOverUI;
    public Powers_PlayerLook playerLook;

    [HideInInspector]
    public int time = 0;
    [HideInInspector]
    public float seconds = 0;
    [HideInInspector]
    public bool gameOver = false;

    private float healthBarFullWidth;

    private int obstTimer;
    [HideInInspector]
    public int healTimer;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;

        healthBarFullWidth = healthBar.rectTransform.sizeDelta.x;
        obstTimer = Mathf.RoundToInt(Random.value * 120) + 120;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver)
        {
            realtimeUI.SetActive(true);
            gameOverUI.SetActive(false);

            Mathf.Clamp(obstTimer, 0, int.MaxValue);

            //if timer is 0, spawn new obstacle and reset timer.
            if (obstTimer == 0)
            {
                Instantiate(obstacle);
                Powers_Obst obstVars = obstacle.GetComponent<Powers_Obst>();
                obstVars.gameManager = this;

                int directionRandomizer = Mathf.RoundToInt(Random.value * 3);

                if (directionRandomizer == 0) obstVars.direction = Powers_Obst.Direction.Forward;
                else if (directionRandomizer == 1) obstVars.direction = Powers_Obst.Direction.Left;
                else if (directionRandomizer == 2) obstVars.direction = Powers_Obst.Direction.Backward;
                else if (directionRandomizer == 3) obstVars.direction = Powers_Obst.Direction.Right;

                obstTimer = Mathf.RoundToInt(Random.value * 40) + 80;
            }

            //clamp health
            playerHealth = Mathf.Clamp(playerHealth, 0, 100);
            //if health 0, set game over to ui
            if (playerHealth == 0) gameOver = true;

            //set ui
            healthBar.rectTransform.sizeDelta = new Vector2(healthBarFullWidth * (playerHealth / 100), healthBar.rectTransform.sizeDelta.y);
            score.text = time.ToString();
        }
        else
        {
            realtimeUI.SetActive(false);
            gameOverUI.SetActive(true);
            playerLook.cursorLock = false;
            playerLook.mouseSensitivity = 0;

            Time.timeScale = 0;
        }
    }

    private void FixedUpdate()
    {
        if(!gameOver)
        {
            obstTimer--;
            healTimer--;
            if (healTimer == 0) playerHealth += 0.01f;
            time++;
            seconds = time / 60;
        }
    }
}
