using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
// Start is called 
public static SoundManager Instance;

//public GameObject AlarmPrefab;
//private GameObject alarm;
private bool alarm = false;
private bool alarm_actually_playing = false;
public AudioClip Alarm;

public AudioClip TimerReady;

public AudioClip Boiler;
public AudioClip Fryer;
public AudioClip Oven;

public AudioClip Fridge;

public AudioClip OrderIncoming;
public AudioClip OrderVeryGood;
public AudioClip OrderGood;
public AudioClip OrderBad;

private Vector3 cameraPosition; // before the first frame update

    void Awake()
    {
        Instance = this;
        cameraPosition = Camera.main.transform.position;
    }

    private void PlaySound(AudioClip clip, Vector3 position)
    {
        AudioSource.PlayClipAtPoint(clip, position);
    }

    private void PlaySound(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, cameraPosition);
    }

    public void Update(){
	if(alarm==true && !alarm_actually_playing){
	    	StartCoroutine("PlayAlarm");
	}
    }
    public IEnumerator PlayAlarm()
    {
	alarm_actually_playing = true;
        PlaySound(Alarm);
Debug.Log("e");
	yield return new WaitForSeconds(Alarm.length);
	alarm_actually_playing = false;
    }
    public void StartAlarm()
    {
        //alarm = Instantiate(AlarmPrefab, AlarmPrefab.transform.position, AlarmPrefab.transform.rotation).GetComponent<GameObject>();
	alarm = true;    
    }

    public void StopAlarm()
    {
        alarm = false;
	//Destroy(alarm.gameObject);
    }

    public void PlayTimerReady()
    {
        PlaySound(TimerReady);
    }

    public void PlayBoiler()
    {
        PlaySound(Boiler);
    }

    public void PlayFryer()
    {
        PlaySound(Fryer);
    }

    public void PlayOven()
    {
        PlaySound(Oven);
    }

    public void PlayFridge()
    {
        PlaySound(Fridge);
    }

    public void PlayOrderIncoming()
    {
        PlaySound(OrderIncoming);
    }

    public void PlayOrderVeryGood()
    {
        PlaySound(OrderVeryGood);
    }

    public void PlayOrderGood()
    {
        PlaySound(OrderGood);
    }

    public void PlayOrderBad()
    {
        PlaySound(OrderBad);
    }




}
