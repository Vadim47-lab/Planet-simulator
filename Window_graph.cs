using System.Collections;
using CodeMonkey.Utils;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Window_graph : MonoBehaviour
{
    [SerializeField] private Sprite circleSprite;//создание спрайта, наша точка для построения графа
    private RectTransform graphContainer;

    private void Awake()
    {
        graphContainer = transform.Find("graphContainer").GetComponent<RectTransform>();
        List<int> valueList = new List<int>() {5, 23, 54, 67, 98, 32, 54, 65, 32};
        ShowGraph(valueList);
    }

    private GameObject CreateCircle(Vector2 anchoredPosition)
    {
        GameObject gameObject = new GameObject("circle", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().sprite = circleSprite;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.sizeDelta = new Vector2(11, 11);
        rectTransform.anchorMin = new Vector2(0,0);
        rectTransform.anchorMax = new Vector2(0,0);
        return gameObject;
    }

    private void ShowGraph(List<int> valueList)
    {
        int i;
        float graphHeight = graphContainer.sizeDelta.y; //Определяем высоту контейнера для графика
        float graphWidth = graphContainer.sizeDelta.x; //Определяем ширину контейнера для графика
        float yMaximum = 98;//valueList.Max; //100f; Вычисляем максимальное значение по Y для всех значений списка valueList
        float yMin = 5;//valueList.Min; //Вычисляем минимальное значение  по Y для всех значений списка valueList
        float xMaximum = valueList.Count - 1; //Вычисляем максимальное значение по Х для всех значений списка valueList. Оно равно количеству записей в списке.
        float xSize = graphWidth / xMaximum; //50f;//Вычисляем нормировочный коэффициент масштабирования по X
        float ySize = graphHeight / (yMaximum - yMin); //100f;//Вычисляем нормировочный коэффициент масштабирования по Y
        GameObject LastCircleGameObject = null;
        for (i = 0; i < valueList.Count; i++)
        {
            float xPosition = i * xSize; //Вычисляем позицию X для очередной точки на графике
            float yPosition = valueList[i] * ySize;//Вычисляем позицию Y для очередной точки на графике
            GameObject circleGameObject = CreateCircle(new Vector2(xPosition, yPosition));//Строим новую точку на графике в координату xPosition, yPosition 
            if (LastCircleGameObject != null) 
            {
                CreateDoConnection(LastCircleGameObject.GetComponent<RectTransform>().anchoredPosition, circleGameObject.GetComponent<RectTransform>().anchoredPosition);
            }
            LastCircleGameObject = circleGameObject;
        }
    }

    private void CreateDoConnection(Vector2 dotPositionA, Vector2 dotPositionB)
    {
        GameObject gameObject = new GameObject("dotConnection", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().color = new Color(1, 1, 1, .5f);
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        Vector2 dir = (dotPositionB - dotPositionA).normalized;
        float distance = Vector2.Distance(dotPositionA, dotPositionB);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.sizeDelta = new Vector2(distance, 3f);
        rectTransform.anchoredPosition = dotPositionA + dir * distance * .5f;
        rectTransform.localEulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVectorFloat(dir));
    }

    void Start()
    {

    }

    void Update()
    {
        
    }
}
