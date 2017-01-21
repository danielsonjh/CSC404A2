using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public Text countDownText;
    float currentTime = 60f;

	// Use this for initialization
	void Start () {

        countDownText.text = currentTime.ToString();
        StartCoroutine(countDown());
	}
	
    IEnumerator countDown()
    {
        while (currentTime > 0)
        {
            yield return new WaitForSeconds(1);
            currentTime -= 1;
            countDownText.text = currentTime.ToString();
        }
    }
}
