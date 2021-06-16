using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawingSpriteForIndex : MonoBehaviour
{
    // Start is called before the first frame update
    enum DirectionMoveToSpawn
    {
        up,
        forward,
        right,
    }

    [SerializeField] private DirectionMoveToSpawn dir = DirectionMoveToSpawn.right;

    [System.Serializable]
    public class DrawingData
    {
        public Sprite sprite;
        public string stringRepresentative;
        [HideInInspector] public SpriteRenderer spriteRenderer;
    }
    [SerializeField] private List<DrawingData> drawingData;
    [SerializeField] private bool useParentTransform;
    [SerializeField] private Transform initPointToDraw;
    [SerializeField] private GameObject drawObjectSpawn;
    [SerializeField] private float distanceBetweenObjectsDrawInX;
    [SerializeField] private float distanceBetweenObjectsDrawInY;
    [SerializeField] private float distanceBetweenObjectsDrawInZ;

    private float countObjectDraw = 0;

    public void Draw(int index)
    {
        if (index < 0 || index >= drawingData.Count)
            return;

        GameObject go = null;

        if (useParentTransform)
            go = Instantiate(drawObjectSpawn, initPointToDraw.position, initPointToDraw.rotation, initPointToDraw);
        else
            go = Instantiate(drawObjectSpawn, initPointToDraw.position , initPointToDraw.rotation);

        Vector3 direction = Vector3.zero;

        switch (dir)
        {
            case DirectionMoveToSpawn.forward:
                direction = transform.forward;
                break;
            case DirectionMoveToSpawn.right:
                direction = transform.right;
                break;
            case DirectionMoveToSpawn.up:
                direction = transform.up;
                break;
        }

        Debug.Log(direction);

        if (useParentTransform)
        {
            go.transform.localPosition += new Vector3(distanceBetweenObjectsDrawInX * countObjectDraw,
                distanceBetweenObjectsDrawInY * countObjectDraw,
                distanceBetweenObjectsDrawInZ * countObjectDraw);
        }
        else
        {
            go.transform.position += new Vector3(distanceBetweenObjectsDrawInX * countObjectDraw,
                distanceBetweenObjectsDrawInY * countObjectDraw,
                distanceBetweenObjectsDrawInZ * countObjectDraw);
        }

        go.transform.localScale = initPointToDraw.localScale;
        drawingData[index].spriteRenderer = go.GetComponent<SpriteRenderer>();
        drawingData[index].spriteRenderer.sprite = drawingData[index].sprite;
        countObjectDraw++;
    }

    public void Draw()
    {
        for (int i = 0; i < drawingData.Count; i++)
        {
            GameObject go = null;

            if (useParentTransform)
                go = Instantiate(drawObjectSpawn, initPointToDraw.position, initPointToDraw.rotation, initPointToDraw);
            else
                go = Instantiate(drawObjectSpawn, initPointToDraw.position, initPointToDraw.rotation);

            Vector3 direction = Vector3.zero;

            switch (dir)
            {
                case DirectionMoveToSpawn.forward:
                    direction = transform.forward;
                    break;
                case DirectionMoveToSpawn.right:
                    direction = transform.right;
                    break;
                case DirectionMoveToSpawn.up:
                    direction = transform.up;
                    break;
            }

            if (useParentTransform)
            {
                go.transform.localPosition += new Vector3(distanceBetweenObjectsDrawInX * countObjectDraw * direction.x,
                    distanceBetweenObjectsDrawInY * countObjectDraw * direction.y,
                    distanceBetweenObjectsDrawInZ * countObjectDraw * direction.z);
            }
            else
            {
                go.transform.position += new Vector3(distanceBetweenObjectsDrawInX * countObjectDraw * direction.x,
                   distanceBetweenObjectsDrawInY * countObjectDraw * direction.y,
                   distanceBetweenObjectsDrawInZ * countObjectDraw * direction.z);
            }

            go.transform.localScale = initPointToDraw.localScale;
            drawingData[i].spriteRenderer = go.GetComponent<SpriteRenderer>();
            drawingData[i].spriteRenderer.sprite = drawingData[i].sprite;
            countObjectDraw++;
        }
    }

    public void SetInitPositionDraw(Transform value) => initPointToDraw = value;

    public void ResetCountObjectDraw()
    {
        countObjectDraw = 0;
    }

    public string GetStringRepresentativeToDrawingData(int index)
    {
        return drawingData[index].stringRepresentative;
    }

    public string GetStringRepresentativeToDrawingData()
    {
        string data = "";

        for (int i = 0; i < drawingData.Count; i++)
        {
            data += drawingData[i].stringRepresentative;
        }

        return data;
    }
    public int GetCountDrawingData()
    {
        if (drawingData != null)
        {
            return drawingData.Count;
        }
        return 0;
    }
}
