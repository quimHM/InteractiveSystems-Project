using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Timer : MonoBehaviour
{
    public int Duration;
    public int burnedDuration;
    public int remainingDuration;
    private int remainingDurationBurned;
    [SerializeField] private Image uiFill;
    [SerializeField] private Text uiText;

    public int state;
    // Start is called before the first frame update
    void Start()
    {
        //Begin(Duration);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Begin(int time)
    {
        state = 0;
        remainingDuration = time;
        StartCoroutine(UpdateTimer());
    }

    private IEnumerator UpdateTimer()
    {
        while (remainingDuration >= 0)
        {
            uiText.text = $"{ remainingDuration }";
            uiFill.fillAmount = Mathf.InverseLerp(0, Duration, remainingDuration);
            remainingDuration--;
            yield return new WaitForSeconds(1f);
        }
        OnEnd();
    }

    private void OnEnd()
    {
        SoundManager.Instance.PlayTimerReady();
        state = 1;
        Debug.Log("Food completed");
        remainingDurationBurned = burnedDuration;
        uiFill.color = new Color32(255, 0, 0, 255);
        StartCoroutine(UpdateTimerBurning());
    }

    private IEnumerator UpdateTimerBurning()
    {
        while (remainingDurationBurned >= 0)
        {
            uiText.text = $"{ remainingDurationBurned }";
            uiFill.fillAmount = Mathf.InverseLerp(0, burnedDuration, remainingDurationBurned);
            remainingDurationBurned--;
            yield return new WaitForSeconds(1f);
        }
        OnEndBurned();
    }

    private void OnEndBurned()
    {
        state = 2;
        // Alert player
        Debug.Log("burned!");
        // Show extintor

    }
}
