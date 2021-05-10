using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Window_grapg : MonoBehaviour
{
    [SerializeField] private Sprite circleSprite;//создание спрайта, наша точка для построения графа
    private RectTransform graphContainer;

    private void Awake()
    {
        graphContainer = transform.Find("graphContainer").GetComponent<RectTransform>();
        List<int> valueList = new List<int>() {5, 23, 54, 67, 98, 32, 54, 65, 32};
        ShowGraph(valueList);
    }

    private void CreateCircle(Vector2 anchoredPosition)
    {
        GameObject gameObject = new GameObject("circle", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().sprite = circleSprite;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.sizeDelta = new Vector2(11, 11);
        rectTransform.anchorMin = new Vector2(0,0);
        rectTransform.anchorMax = new Vector2(0,0);
    }

    private void ShowGraph(List<int> valueList)
    {
        int i = 0;
        float graphHeight = graphContainer.sizeDelta.y; //Определяем высоту контейнера для графика
        float graphWidth = graphContainer.sizeDelta.x; //Определяем ширину контейнера для графика
        
        float yMaximum = valueList.Max; //100f; Вычисляем максимальное значение по Y для всех значений списка valueList
        float yMin = valueList.Min; //Вычисляем минимальное значение  по Y для всех значений списка valueList
        float xMaximum = float(valueList.Count); //Вычисляем максимальное значение по Х для всех значений списка valueList. Оно равно количеству записей в списке.
        float xSize = xMaximum/graphWidth; //50f;//Вычисляем нормировочный коэффициент масштабирования по X
        float ySize = (valueList.Max-valueList.Min)/graphHeight; //100f;//Вычисляем нормировочный коэффициент масштабирования по Y
        for (i = 0; i < valueList.Count; i++) //Запускаем цикл по всем значениям списка в valueList.
        {
            float xPosition = i * xSize; //Вычисляем позицию X для очередной точки на графике
            float yPosition = valueList[i]* graphHeight;//Вычисляем позицию Y для очередной точки на графике
            CreateCircle(new Vector2(xPosition, yPosition));//Строим новую точку на графике в координату xPosition, yPosition 
        }
    }

    void Start()
    {

    }

    void Update()
    {
        
    }
}
