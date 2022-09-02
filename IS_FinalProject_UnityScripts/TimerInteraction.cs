using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TimerInteraction : MonoBehaviour
{
    public int Duration;
    public float remainingDuration;
    [SerializeField] private Image uiFill;

    public int state;
    // Start is called before the first frame update
    void Start()
    {
        state = -1;
	uiFill.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (state==-1){uiFill.enabled = false;}
	else{uiFill.enabled = true;}
    }

    public void Begin(float time)
    {
        state = 0;
        remainingDuration = time;
        StartCoroutine(UpdateTimer());
    }

    private IEnumerator UpdateTimer()
    {
        while (remainingDuration >= 0 && state==0)
        {
            uiFill.fillAmount = Mathf.InverseLerp(0, Duration, remainingDuration);
            remainingDuration-=0.05f;
            yield return new WaitForSeconds(0.05f);
        }
        OnEnd();
    }

    private void OnEnd()
    {
        state = 1;
    }
}
