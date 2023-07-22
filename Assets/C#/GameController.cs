using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController _GC;

    public GameObject Loose_screen;

    public TMP_Text timer_text;
    public TMP_Text score_text;

    public float start_timer;
    private float _timer;

    public static int Score;

    public static int _score
    {
        get { return Score; }
        set { 
            Score = value;
            _GC.update_scoreText(value);
        }
    }

    public static bool playing = true;

    [Space, Header("raycast")]
    public LayerMask raycastIgnoreLayer;

    private void Start()
    {
        _GC = this;
        _timer = start_timer;
        _score = 0;
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
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, new Vector3(0, 0, 1), 1000.0f, ~raycastIgnoreLayer);

            Debug.DrawRay(mousePosition, new Vector3(0, 0, 1));

            if (hit.collider != null)
            {
                Debug.Log(hit.collider.gameObject.tag);
                if (hit.collider.gameObject.tag == "Mouse")
                {
                    hit.collider.gameObject.GetComponent<Mouse>().Die();
                }
                else if (hit.collider.gameObject.tag == "AgainButton")
                {
                    playing = true;
                    _score = 0;

                    int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
                    SceneManager.LoadScene(currentSceneIndex);
                }
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
    public void update_scoreText(int value)
    {
        score_text.text = "score: " + value.ToString();
    }
}
