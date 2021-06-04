using UnityEngine;

public class ColorLightRayEmitter : MonoBehaviour
{
    private int currentColorEmitterIndex = -1;
    private Color currentColor;
    [SerializeField] private bool enableUse = false;

    [SerializeField] private KeyCode changeNextColorButton = KeyCode.RightArrow;
    [SerializeField] private KeyCode changePrevColorButton = KeyCode.LeftArrow;

    [SerializeField] private Light myLightModifireColor;

    [SerializeField] private Color[] colorsEmitter;

    [SerializeField] private Transform spawnRay;
    [SerializeField] private float rangeRay;
    [SerializeField] private LayerMask mask = -1;
    void OnEnable()
    {
        if (enableUse)
        {
            //Debug.Log("Rayo lanzado desde: " + spawnRay.transform.position);
            ThrowRayCheck();
        }
    }

    void OnDisable()
    {
        if (!enableUse)
        {
            ChangeColorLight(Color.white);
        }
    }

    void Start(){}

    //Esto hay que pasarlo al inputManager
    void Update()
    {
        if (Input.GetKeyDown(changeNextColorButton))
        {
            ChangeNextColor();
            ThrowRayCheck();
        }

        if (Input.GetKeyDown(changePrevColorButton))
        {
            ChangePrevColor();
            ThrowRayCheck();
        }
    }

    public void ChangePrevColor()
    {
        if (enableUse)
        {
            currentColorEmitterIndex--;
            if (currentColorEmitterIndex < 0)
                currentColorEmitterIndex = colorsEmitter.Length - 1;

            currentColor = colorsEmitter[currentColorEmitterIndex];

            ChangeColorLight(currentColor);
        }
    }

    public void ChangeNextColor()
    {
        if (enableUse)
        {
            currentColorEmitterIndex++;
            if (currentColorEmitterIndex > colorsEmitter.Length - 1)
                currentColorEmitterIndex = 0;

            currentColor = colorsEmitter[currentColorEmitterIndex];

            ChangeColorLight(currentColor);
        }
    }

    public void ChangeColorLight(Color color)
    {
        if (enableUse)
        {
            myLightModifireColor.color = color;
        }
    }

    public void ThrowRayCheck()
    {
        if (enableUse)
        {
            RaycastHit hit;
            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(spawnRay.position, spawnRay.transform.forward, out hit, rangeRay, mask))
            {
                //Debug.Log(hit.transform.name);
                ListenerColorLightRay listenerColorRay = hit.collider.GetComponent<ListenerColorLightRay>();
                if (listenerColorRay != null)
                {
                    listenerColorRay.CheckIsCorrectColor(currentColor);
                }
            }
            //Debug.DrawRay(spawnRay.position, spawnRay.transform.forward, Color.red, 1000);
        }
    }

    public Color GetCurrentColor() { return currentColor; }

    public void SetEnableUse(bool value) => enableUse = value;
}
