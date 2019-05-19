using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour {
    public static GameController Gc;
    public bool pause = false;
    public float timer;
    public int m=0;
    
    public GameObject winnerPannel,gameplayPannel;
    public TextMeshProUGUI timerText;

    public AudioSource death, checkPoint,booster;

	// Use this for initialization
	void Start () {
        Gc = this;
        winnerPannel.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer >= 60)
        {
            m++;
            timer = 0;
        }
	}


    public void PlayAudio(string type)
    {
        switch (type) {
            case "Death":
                death.Play();
                break;
            case "CheckPoint":
                checkPoint.Play();
                break;
            
        }
    }

    public void LevelCleared()
    {
        pause = true;
        gameplayPannel.SetActive(false);
        winnerPannel.SetActive(true);
        timerText.SetText(m + "m : " + (int)timer+" s");
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadScene(int num)
    {
        SceneManager.LoadScene(num);
    }
}
