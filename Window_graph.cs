using System.Collections;
using CodeMonkey.Utils;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

public class Window_graph : MonoBehaviour
{
    [SerializeField] private Sprite circleSpriteRabbit;//создание спрайта, наша точка для построения графа
    [SerializeField] private Sprite circleSpriteFox;//создание спрайта, наша точка для построения графа
    [SerializeField] private Sprite circleSpriteGrass;//создание спрайта, наша точка для построения графа
    private RectTransform graphContainer;
    public List<int> valueRabbitList = new List<int>() { 1, 1 };
    public List<int> valueFoxList = new List<int>() { 1, 1 };
    public List<int> valueGrassList = new List<int>() { 1, 1 };
    public Image imageRabbit;
    public Image imageFox;
    public Image imageGrass;
    int i;

    private void Awake()
    {
        graphContainer = transform.Find("graphContainer").GetComponent<RectTransform>();
    }

    void Update()
    {
        if (valueRabbitList.Count == 200) valueRabbitList.RemoveAt(0);
        if (valueFoxList.Count == 200) valueFoxList.RemoveAt(0);
        if (valueGrassList.Count == 200) valueGrassList.RemoveAt(0);
    }

    private GameObject CreateCircle(Vector2 anchoredPosition)
    {
        GameObject gameObject = new GameObject("circle", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().sprite = circleSpriteRabbit;
        gameObject.GetComponent<Image>().sprite = circleSpriteFox;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.sizeDelta = new Vector2(5, 5);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        return gameObject;
    }

    /*
 http://virq.ru/theme164.html
    
 List<int> x = new List<int>() { 5, 27, -6, 14, 70, 14, 178 };
x.Remove(14);
    
 List<int> x = new List<int>() { 5, 27, -6, 14, 70, 14, 178 };
int n = x.Count; //7 элементов
int m = x.Count - 1; //Получить индекс последнего элемента
    
 List<int> x = new List<int>() { 5, 27, -6, 14, 70, 14, 178 };
x.Reverse(); //Получить обратный порядок элементов, т.е. 178, 14, 70, 14, -6, 27, 51
x.Sort(); //Сортировать элементы по порядку с увеличением
int a = x.Min(); //Найти наименьшее значение в списке. Получим -6
int b = x.Max(); //Найти наибольшее значение в списке. Получим 178
int c = x.Sum(); //Найти сумму элементов. Получим 302
int d = x.Average(); //Найти среднее значение чисел. Получим примерно 43,14
List<int> x = new List<int>() { 5, 27, -6, 14, 70, 14, 178 };
x.Insert(1, 1000);
List<int> x = new List<int>() { 5, 27, -6, 14, 70, 14, 178 };
int a = x.IndexOf(5); //Получим 0-ую позицию
int b = x.IndexOf(-6); //Получим 2-ую позицию
int k = x.IndexOf(70); //Получим 4-ую позицию
int q = x.IndexOf(166); //Получим -1
List<int> x = new List<int>() { 5, 27, -6, 14, 70, 14, 178 };
x.RemoveAt(2); //Будет удалён 3-ий элемент по счёту
    */

    public void ShowGraph()
    {
        GameObject[] tmp = GameObject.FindGameObjectsWithTag("Circle");
        for (int i = 0; i < tmp.Length; i++)
        {
            Destroy(tmp[i]);
        }

        tmp = GameObject.FindGameObjectsWithTag("dotConnection");
        for (int i = 0; i < tmp.Length; i++)
        {
            Destroy(tmp[i]);
        }
        valueRabbitList.Add(Main.Sumrabbit);
        valueFoxList.Add(Main.FoxSum);
        valueGrassList.Add(Main.grassSum1);
        //Перенести инициализацию на вверх
        float graphHeight = graphContainer.sizeDelta.y; //Определяем высоту контейнера для графика
        float graphWidth = graphContainer.sizeDelta.x; //Определяем ширину контейнера для графика
        float yMaximumRabbit = 10;//valueList.Max; //100f; Вычисляем максимальное значение по Y для всех значений списка valueList
        float yMaximumFox = 10;//valueList.Max; //100f; Вычисляем максимальное значение по Y для всех значений списка valueList
        float yMaximumGrass = 10;//valueList.Max; //100f; Вычисляем максимальное значение по Y для всех значений списка valueList
        if (Main.Sumrabbit > 10) yMaximumRabbit = valueRabbitList.Max();//Main.Sumrabbit;
        if (Main.FoxSum > 10) yMaximumFox = valueFoxList.Max();
        if (Main.grassSum1 > 10) yMaximumGrass = valueGrassList.Max();
        float yMinRabbit = 1;//valueRabbitList.Min; //Вычисляем минимальное значение по Y для всех значений списка valueList
        float yMinFox = 1;//valueFoxList.Min; //Вычисляем минимальное значение по Y для всех значений списка valueList
        float yMinGrass = 1;//valueGrassList.Min; //Вычисляем минимальное значение по Y для всех значений списка valueList
        //float xMaximum = valueRabbitList[valueRabbitList.Count - 1]; //Вычисляем максимальное значение по Х для всех значений списка valueList. Оно равно количеству записей в списке.
        float xMinimum = 1;
        float xMaximum = valueRabbitList.Count - 1;
        float xSize = graphWidth / (xMaximum - xMinimum); //50f;//Вычисляем нормировочный коэффициент масштабирования по X
        float ySizeRabbit = (graphHeight - 15) / (yMaximumRabbit - yMinRabbit); //100f;//Вычисляем нормировочный коэффициент масштабирования по Y
        float ySizeFox = (graphHeight - 15) / (yMaximumFox - yMinFox); //100f;//Вычисляем нормировочный коэффициент масштабирования по Y
        float ySizeGrass = (graphHeight - 15) / (yMaximumGrass - yMinGrass); //100f;//Вычисляем нормировочный коэффициент масштабирования по Y
        GameObject LastCircleGameObjectRabbit = null;
        GameObject LastCircleGameObjectFox = null;
        GameObject LastCircleGameObjectGrass = null;
        for (i = 0; i < valueRabbitList.Count - 1; i++)
        {
            float xPosition = i * xSize; //Вычисляем позицию X для очередной точки на графике
            float yPositionRabbit = valueRabbitList[i] * ySizeRabbit;//Вычисляем позицию Y для очередной точки на графике
            float yPositionFox = valueFoxList[i] * ySizeFox;//Вычисляем позицию Y для очередной точки на графике
            float yPositionGrass = valueGrassList[i] * ySizeGrass;//Вычисляем позицию Y для очередной точки на графике
            GameObject circleGameObjectRabbit = CreateCircle(new Vector2(xPosition, yPositionRabbit));//Строим новую точку на графике в координату xPosition, yPosition 
            GameObject circleGameObjectFox = CreateCircle(new Vector2(xPosition, yPositionFox));//Строим новую точку на графике в координату xPosition, yPosition
            GameObject circleGameObjectGrass = CreateCircle(new Vector2(xPosition, yPositionGrass));//Строим новую точку на графике в координату xPosition, yPosition 
            circleGameObjectRabbit.tag = "Circle";
            circleGameObjectFox.tag = "Circle";
            circleGameObjectGrass.tag = "Circle";
            if (LastCircleGameObjectRabbit != null)
            {
                CreateDoConnection(new Color(1, 1, 1, .5f), LastCircleGameObjectRabbit.GetComponent<RectTransform>().anchoredPosition, circleGameObjectRabbit.GetComponent<RectTransform>().anchoredPosition);
            }
            LastCircleGameObjectRabbit = circleGameObjectRabbit;

            if (LastCircleGameObjectFox != null)
            {
                CreateDoConnection(new Color(1, 0, 0, .5f), LastCircleGameObjectFox.GetComponent<RectTransform>().anchoredPosition, circleGameObjectFox.GetComponent<RectTransform>().anchoredPosition);
            }
            LastCircleGameObjectFox = circleGameObjectFox;

            if (LastCircleGameObjectGrass != null)
            {
                CreateDoConnection(new Color(1, 1, 0, .5f), LastCircleGameObjectGrass.GetComponent<RectTransform>().anchoredPosition, circleGameObjectGrass.GetComponent<RectTransform>().anchoredPosition);
            }
            LastCircleGameObjectGrass = circleGameObjectGrass;
        }
    }

    private void CreateDoConnection(Color color, Vector2 dotPositionA, Vector2 dotPositionB)
    {
        //Перенести инициализацию наверх
        GameObject gameObjectRabbit = new GameObject("dotConnection", typeof(Image));
        gameObjectRabbit.tag = "dotConnection";
        gameObjectRabbit.transform.SetParent(graphContainer, false);
        gameObjectRabbit.GetComponent<Image>().color = color;
        RectTransform rectTransformRabbit = gameObjectRabbit.GetComponent<RectTransform>();
        Vector2 dirRabbit = (dotPositionB - dotPositionA).normalized;
        float distanceRabbit = Vector2.Distance(dotPositionA, dotPositionB);
        rectTransformRabbit.anchorMin = new Vector2(0, 0);
        rectTransformRabbit.anchorMax = new Vector2(0, 0);
        rectTransformRabbit.sizeDelta = new Vector2(distanceRabbit, 3f);
        rectTransformRabbit.anchoredPosition = dotPositionA + dirRabbit * distanceRabbit * .5f;
        rectTransformRabbit.localEulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVectorFloat(dirRabbit));
    }

    void Start()
    {
        InvokeRepeating("ShowGraph", 1, 10);
        Debug.Log("Start: valueListRabbit.Count = " + valueRabbitList.Count);
    }
}
