using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public GameObject Panel;

    public Text CountDownText;
    int _currentTime = 2;

	// Use this for initialization
	void Start () {

        CountDownText.text = _currentTime.ToString();
        StartCoroutine(CountDown());
	}
	
    IEnumerator CountDown()
    {
        while (_currentTime > 0)
        {
            yield return new WaitForSeconds(1);
            _currentTime -= 1;
            CountDownText.text = _currentTime.ToString();
            if (_currentTime == 0)
            {
                Panel.transform.FindChild("WinnerText").GetComponent<Text>().text = "Player Wins";
                Panel.SetActive(true);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Panel.transform.FindChild("WinnerText").GetComponent<Text>().text = "Carpet Wins";
            Panel.SetActive(true);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene("Balls");
    }
}
