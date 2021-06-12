using UnityEngine;

public class ColorLightRayEmitter : Updateable
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

    public static System.Action<GameObject> OnFaildHitRayEmmiter;

    private RaycastHit hit;
    //private ListenerColorLightRay listenerColorRay;

    protected override void OnEnable()
    {
        base.OnEnable();
        if (enableUse)
        {
            //Debug.Log("Rayo lanzado desde: " + spawnRay.transform.position);
            ThrowRayCheck();
        }
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        if (!enableUse)
        {
            ChangeColorLight(Color.white);
        }
        else
        {
            if (OnFaildHitRayEmmiter != null)
            {
                if(hit.collider != null)
                    OnFaildHitRayEmmiter(hit.collider.gameObject);
            }
        }
    }

    protected override void Start()
    {
        base.Start();
        MyUpdate.AddListener(UpdateColorLightRayEmitter);
        UM.UpdatesInGame.Add(MyUpdate);
    }

    //Esto hay que pasarlo al inputManager
    void UpdateColorLightRayEmitter()
    {
        if (enableUse)
        {
            if (Input.GetKeyDown(changeNextColorButton))
            {
                ChangeNextColor();
            }
            if (Input.GetKeyDown(changePrevColorButton))
            {
                ChangePrevColor();
            }
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
            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(spawnRay.position, spawnRay.transform.forward, out hit, rangeRay, mask))
            {
                ListenerColorLightRay listenerColorRay = hit.collider.GetComponent<ListenerColorLightRay>();
                if (listenerColorRay != null)
                {
                    //Debug.Log("ENTRE");
                    listenerColorRay.CheckIsCorrectColor(currentColor);
                }
                else
                {
                    if (OnFaildHitRayEmmiter != null)
                        OnFaildHitRayEmmiter(hit.collider.gameObject);
                }
            }
            else
            {
                if (OnFaildHitRayEmmiter != null)
                    OnFaildHitRayEmmiter(hit.collider.gameObject);
            }
            //Debug.DrawRay(spawnRay.position, spawnRay.transform.forward, Color.red, 1000);
        }
    }

    public Color GetCurrentColor() { return currentColor; }

    public void SetEnableUse(bool value) => enableUse = value;
}
