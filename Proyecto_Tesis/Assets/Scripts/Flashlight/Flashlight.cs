using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : Updateable
{
    // Start is called before the first frame update


    
    [System.Serializable]
    public class RayLight
    {
        [HideInInspector]
        public Transform spawnRay;
        [HideInInspector]
        public float rangeRayLight;

        public void CheckRayLight()
        {
            RaycastHit hit;

            if (Physics.Raycast(spawnRay.transform.position, spawnRay.transform.forward, out hit, rangeRayLight))
            {
                ViewFlashlight viewFlashlight = hit.collider.GetComponent<ViewFlashlight>();

                if (viewFlashlight != null)
                {
                    viewFlashlight.OnViewFlash?.Invoke();
                }
            }
        }
    }
    public enum LightState
    {
        On,
        Off,
    }
    [SerializeField] private Transform parentRayLights;
    [SerializeField] private int countRayLight;
    [SerializeField] private float rangeRayLight;
    [SerializeField] private float distanceBetweenRayLights;
    [SerializeField] private GameObject originalSpawnerObject;
    [SerializeField] private List<GameObject> RayLightsInstanciated;
    [SerializeField] private List<RayLight> raysLight;
    [SerializeField] private Vector3 initialRotation;


    [SerializeField] private bool useUpdateFlashlight = true;
    [SerializeField] private GameObject lightObject;
    [SerializeField] private LightState lightState;

    protected override void Start()
    {
        InitRayCasters();

        base.Start();
        if (useUpdateFlashlight)
        {
            MyUpdate.AddListener(FixedUpdateFlashlight);
            UM.UpdatesInGame.Add(MyUpdate);
        }

    }

    private void InitRayCasters()
    {
        raysLight = new List<RayLight>();

        for (int i = 0; i < countRayLight; i++)
        {
            raysLight.Add(new RayLight());
        }

        Vector3 currentRotation = initialRotation;
        for (int i = 0; i < raysLight.Count; i++)
        {
            GameObject go = Instantiate(originalSpawnerObject, parentRayLights.transform.position, Quaternion.identity, parentRayLights);
            currentRotation = currentRotation + new Vector3(0, distanceBetweenRayLights, 0);
            go.transform.eulerAngles = currentRotation;
            RayLightsInstanciated.Add(go);
        }

        for (int i = 0; i < raysLight.Count; i++)
        {
            raysLight[i].spawnRay = RayLightsInstanciated[i].transform;
            raysLight[i].rangeRayLight = rangeRayLight;
        }
    }

    public void FixedUpdateFlashlight()
    {
        switch (lightState)
        {
            case LightState.On:
                CheckAllRayCastLight();
                if (!lightObject.activeSelf)
                    lightObject.SetActive(true);

                break;
            case LightState.Off:
                if (lightObject.activeSelf)
                    lightObject.SetActive(false);
                break;
        }
    }

    public void OnLight()
    {
        lightState = LightState.On;
    }

    public void ChangeLight()
    {
        switch (lightState)
        {
            case LightState.On:
                lightState = LightState.Off;
                break;
            case LightState.Off:
                lightState = LightState.On;
                break;
        }
    }

    public void CheckAllRayCastLight()
    {
        for (int i = 0; i < raysLight.Count; i++)
        {
            raysLight[i].CheckRayLight();
        }
    }

    public void OffLight()
    {
        lightState = LightState.Off;
    }

    
}
