using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
   // public TESTHighscoreManager TESThighscoreManager;
    public HighscoreManager highscoreManager;
    public GameObject highscoreEntries;
    public GameObject highscoreEntryUIPrefab;

    // Start is called before the first frame update
    private void Start()
    {
        ShowHighscores();
    }

    private void ShowHighscores()
    {
        for (var i = highscoreEntries.transform.childCount - 1; i >= 0; i--)
        {
            var child = highscoreEntries.transform.GetChild(i);
            Destroy(child.gameObject);
        }

        //      var highscores<highscoreEntry> = TESTHighscoreManager.List();

        //      foreach(var highscore < 0)
        //       {
        //           var highscoreEntry = Instantiate(highscoreEntryUIPrefab, highscoreEntries.transform);
        //           var textMeshPro = highscoreEntry.GetComponent<TextMeshProUGUI>();
        //           textMeshPro.text = $"{highscore.Score} - {highscore.Name}";
        //
        // 10 - Carina
        // 5 - Flo
        // 1 Manu
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("AngelAndDeath");
    }

    public void StartOptions()
    {
        SceneManager.LoadScene("Options");
    }

    public void StartHighscore()
    {
        SceneManager.LoadScene("Highscore");
    }

    public void StartTest()
    {
        SceneManager.LoadScene("TESTPlane");
    }

    public void CloseGame()
    {
        if (Application.isEditor)
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#endif
        }
        else
        {
            Application.Quit();
        }
    }
}
