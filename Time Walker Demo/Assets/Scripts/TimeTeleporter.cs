using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TimeTeleporter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Timer;
    [SerializeField] private GameObject PresentPoint;
    [SerializeField] private GameObject PastPoint;
    [SerializeField] private UnityEvent StartEffect;
    [SerializeField] private UnityEvent StopEffect;
    private Vector3 length;
    private bool TimeNow = true;
    private bool TimeOut = false;

    private void Start() {
        length = PastPoint.transform.position - PresentPoint.transform.position;
    }

    private void Update() {
        Teleport();
    }

    private void Teleport()
    {
        if(Input.GetKeyDown(KeyCode.F) && TimeNow == true && TimeOut == false) 
        {
            transform.position += length;
            TimeNow = false;
            TimeOut = true;
            StartEffect.Invoke();
            Debug.Log(TimeOut);
            StartCoroutine(Waiting(5));
        }
        else if(Input.GetKeyDown(KeyCode.F) && TimeNow == false && TimeOut == false)
        {
            transform.position -= length;
            TimeNow = true;
            TimeOut = true;
            StopEffect.Invoke();
            StartCoroutine(Waiting(5));
        }
    }

    IEnumerator Waiting(int waitTime)
    {
        Debug.Log("ddddd");
        int i = 0;
        while (i < waitTime)
        {
            Timer.text = $"{waitTime - i}";
            Debug.Log(i);
            yield return new WaitForSeconds(1);
            i++;
        }
        Timer.text = "Ready";
        TimeOut = false;
    }
}
