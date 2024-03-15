using System;
using TMPro;
using UnityEngine;

public class Grid<TGridObject> 
{
    
    public event EventHandler<OnGridValueChangedEnetArgs> OnGridValueChanged;
    public class OnGridValueChangedEnetArgs : EventArgs
    {
        public int x;
        public int y;
    }
    public const int sortingOrderDefault = 5000;
    int width; 
    int height;
    float cellSize;
    Vector3 originPosition;
    TGridObject[,] gridArray;
    

    public Grid(int width, int height, float cellSize, Vector3 originPosition, Func<Grid<TGridObject>, int, int, TGridObject> createGridObject) 
    {
        this.width = width;
        this.height = height;
        this.cellSize= cellSize;
        this.originPosition = originPosition;

        gridArray = new TGridObject[width, height];

        for(int x = 0; x < gridArray.GetLength(0); x++)
        {
            for(int y = 0; y < gridArray.GetLength(1); y++)
            {
                gridArray[x, y] = createGridObject(this, x, y);
            }
        }

        bool showDebug = true;
        if (showDebug)
        {
            TextMesh[,] debugtextArray = new TextMesh[width, height];

            for (int x = 0; x < gridArray.GetLength(0); x++)
            {
                for (int y = 0; y < gridArray.GetLength(1); y++)
                {
                    debugtextArray[x, y] = CreateWorldText(gridArray[x, y]?.ToString(), null, GetWorldposition(x, y) + new Vector3(cellSize, cellSize) * 0.5f, 20, Color.white, TextAnchor.MiddleCenter);
                    Debug.DrawLine(GetWorldposition(x, y), GetWorldposition(x, y + 1), Color.white, 100f);
                    Debug.DrawLine(GetWorldposition(x, y), GetWorldposition(x + 1, y), Color.white, 100f);
                }
            }
            Debug.DrawLine(GetWorldposition(0, height), GetWorldposition(width, height), Color.white, 100f);
            Debug.DrawLine(GetWorldposition(width, 0), GetWorldposition(width, height), Color.white, 100f);

            OnGridValueChanged += (object sender, OnGridValueChangedEnetArgs eventArgs) =>{
                debugtextArray[eventArgs.x, eventArgs.y].text = gridArray[eventArgs.x, eventArgs.y]?.ToString();
            };
        }
        
    }
    public int GetWidth()
    { 
        return width; 
    }
    public int GetHeight()
    { 
        return height;
    }
    public float GetCellSize()
    {
        return cellSize;
    }
    Vector3 GetWorldposition(int x, int y) 
    {
        return new Vector3(x, y) * cellSize + originPosition;
    }

    public void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
        y = Mathf.FloorToInt((worldPosition - originPosition).y / cellSize);
    }
    public void SetGridObject(int x, int y, TGridObject value)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            gridArray[x, y] = value;
            if(OnGridValueChanged != null)
                OnGridValueChanged(this, new OnGridValueChangedEnetArgs { x=x, y=y });
        }
    }
    public void TriggerGridObjectChanged(int x, int y)
    {
        if (OnGridValueChanged != null)
            OnGridValueChanged(this, new OnGridValueChangedEnetArgs { x = x, y = y });
    }
    public void SetGridObject(Vector3 worldPosition, TGridObject value)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        SetGridObject(x, y, value);
    }

    public TGridObject GetGridObject(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            return gridArray[x, y];
        }
        else
            return default(TGridObject);
    }
    public TGridObject GetGridObject(Vector3 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        return GetGridObject(x, y);
    }
    public static TextMesh CreateWorldText(string text, Transform parent= null, Vector3 localPosition = default(Vector3), int fontSize = 40, Color? color = null, TextAnchor textAnchor = TextAnchor.UpperLeft, TextAlignment textAlignment = TextAlignment.Left, int sortingOrder = sortingOrderDefault)
    {
        if (color == null) color = Color.white;
        return CreateWorldText(parent, text, localPosition, fontSize, (Color)color, textAnchor, textAlignment, sortingOrder);
    }

    public static TextMesh CreateWorldText(Transform parent, string text, Vector3 localPosition, int fontSize, Color color, TextAnchor textAnchor, TextAlignment textAlignment, int sortingOrder) 
    {
        GameObject gameObject= new GameObject("World_Text", typeof(TextMesh));
        Transform transform= gameObject.transform;
        transform.SetParent(parent, false);
        transform.localPosition = localPosition;
        TextMesh textMesh = gameObject.GetComponent<TextMesh>();
        textMesh.anchor= textAnchor;
        textMesh.alignment = textAlignment;
        textMesh.text = text;
        textMesh.fontSize = fontSize;
        textMesh.color = color;
        textMesh.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;
        return textMesh;
    }
}
