using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] float LevelLoadDelay = 1f;
    [SerializeField] float LevelExitSlowDown = 0.2f;

    private void Start()
    {
        //Time.timeScale = 1;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent("Player"))
        {
            StartCoroutine(WaitThenLoad());
        }
    }

    IEnumerator WaitThenLoad()
    {
        Time.timeScale = LevelExitSlowDown;
        yield return new WaitForSecondsRealtime(LevelLoadDelay);
        Time.timeScale = 1f;

        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
