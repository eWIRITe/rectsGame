using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject Loose_screen;

    public TMP_Text timer_text;
    public TMP_Text score_text;

    public float start_timer;
    private float _timer;

    public static int _score = 0;
    public static bool playing = true;

    [Space, Header("raycast")]
    public LayerMask mouse_raycastLayer;

    private void Start()
    {
        _timer = start_timer;
    }
    private void FixedUpdate()
    {
        if(_timer <= 0)
        {
            Loose();
        }
        else
        {
            _timer -= Time.deltaTime;
        }

        update_timerText();
        update_scoreText();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        else if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, new Vector3(0, 0, 1), 1000.0f, mouse_raycastLayer);

            Debug.DrawRay(mousePosition, new Vector3(0, 0, 1));

            if (hit.collider != null)
            {
                if (hit.collider.gameObject.tag == "Mouse")
                {
                    hit.collider.gameObject.GetComponent<Mouse>().Die();
                }
                else
                {
                    Debug.Log("if(hit.collider.CompareTag('Mouse'))" + hit.collider.gameObject.tag);
                }
            }
            else
            {
                Debug.Log(" if (hit.collider != null)");
            }
        }
    }

    public void Loose()
    {
        playing = false;

        Loose_screen.SetActive(true);
    }

    public void PlayAgain()
    {
        playing = true;
        _score = 0;

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    private void update_timerText()
    {
        timer_text.text = "left: " + Math.Round(_timer).ToString();
    }
    private void update_scoreText()
    {
        score_text.text = "score: " + _score.ToString();
    }
}