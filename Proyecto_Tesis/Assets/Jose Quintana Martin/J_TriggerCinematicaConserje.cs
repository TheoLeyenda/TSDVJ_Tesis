using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class J_TriggerCinematicaConserje : MonoBehaviour
{
    CameraManager cameraManager;
    float timer;
    bool startTimer = false;

    public float cinematicLength = 2.5f;
    public UnityEvent onTriggerEnterAction;
    public UnityEvent onCinematicEnd;
    

    // Start is called before the first frame update
    void Start()
    {
        cameraManager = FindObjectOfType<CameraManager>();
    }

    private void Update()
    {
        if (startTimer)
        {
            timer += Time.deltaTime;

            if (timer >= cinematicLength)
            {
                onCinematicEnd.Invoke();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        onTriggerEnterAction.Invoke();
        startTimer = true;
    }
}
