using System.Collections;
using CodeMonkey.Utils;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Window_graph : MonoBehaviour
{
    [SerializeField] private Sprite circleSprite;//создание спрайта, наша точка для построения графа
    private RectTransform graphContainer;
    public int maxCounter2 = 0;
    public float CurrentTime;//считает колличество секунд
    public float GameSeconds;//количество секунд
    public List<int> valueList = new List<int>() { 2, 2 };
    int i;
    bool refresh = false;

    private void Awake()
    {
        graphContainer = transform.Find("graphContainer").GetComponent<RectTransform>();
        ShowGraph(valueList);
        Debug.Log("Awake: valueList.Capacity = " + valueList.Capacity);
        Console.WriteLine("Консоль Awake: valueList.Capacity = " + valueList.Capacity);
    }

    void Update()
    {
        if (valueList.Count == 20) valueList.RemoveAt(1);
        GameSeconds = GameSeconds + Time.deltaTime;
        CurrentTime += Time.deltaTime;
        if (GameSeconds <= 0.68f)
        {
            Debug.Log("AI_rabbit.counter2 = " + AI_rabbit.counter2);
            Debug.Log("Update: valueList.Count = " + valueList.Count);
            Debug.Log("valueList[valueList.Count] = " + valueList[2]);
            if (AI_rabbit.counter2 != valueList[valueList.Count] || valueList.Count == 0)
            {
                refresh = true;
                Destroy(GameObject.Find("circle"));
                Destroy(GameObject.Find("dotConnection"));
            }
            if (GameSeconds >= 0.69f && refresh == true)
            {
                if (Main.Sumrabbit > maxCounter2) maxCounter2 = Main.Sumrabbit;
                Debug.Log("i = " + i);
                valueList.Add(Main.Sumrabbit);
                ShowGraph(valueList);
                refresh = false;
            }
            if (GameSeconds >= 3f) GameSeconds = 0.0f;
        }
    }

    private GameObject CreateCircle(Vector2 anchoredPosition)
    {
        GameObject gameObject = new GameObject("circle", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().sprite = circleSprite;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.sizeDelta = new Vector2(5, 5);
        rectTransform.anchorMin = new Vector2(0,0);
        rectTransform.anchorMax = new Vector2(0,0);
        return gameObject;
    }
    
    /*
    http://virq.ru/theme164.html
    
    List<int> x = new List<int>() { 5, 27, -6, 14, 70, 14, 178 };
x.Remove(14);
    
    List<int> x = new List<int>() { 5, 27, -6, 14, 70, 14, 178 };
int n = x.Count;       //7 элементов
int m = x.Count - 1;   //Получить индекс последнего элемента
    
    List<int> x = new List<int>() { 5, 27, -6, 14, 70, 14, 178 };
x.Reverse();          //Получить обратный порядок элементов, т.е. 178, 14, 70, 14, -6, 27, 51
x.Sort();             //Сортировать элементы по порядку с увеличением
int a = x.Min();      //Найти наименьшее значение в списке. Получим -6
int b = x.Max();      //Найти наибольшее значение в списке. Получим 178
int c = x.Sum();      //Найти сумму элементов. Получим 302
int d = x.Average();  //Найти среднее значение чисел. Получим примерно 43,14

List<int> x = new List<int>() { 5, 27, -6, 14, 70, 14, 178 };
x.Insert(1, 1000);

List<int> x = new List<int>() { 5, 27, -6, 14, 70, 14, 178 };
int a = x.IndexOf(5);     //Получим 0-ую позицию
int b = x.IndexOf(-6);    //Получим 2-ую позицию
int k = x.IndexOf(70);    //Получим 4-ую позицию
int q = x.IndexOf(166);   //Получим -1

List<int> x = new List<int>() { 5, 27, -6, 14, 70, 14, 178 };
x.RemoveAt(2);    //Будет удалён 3-ий элемент по счёту
    */

    public void ShowGraph(List<int> valueList)
    {
        //Перенести инициализацию на вверх
        float graphHeight = graphContainer.sizeDelta.y; //Определяем высоту контейнера для графика
        float graphWidth = graphContainer.sizeDelta.x; //Определяем ширину контейнера для графика
        float yMaximum = 10;//valueList.Max; //100f; Вычисляем максимальное значение по Y для всех значений списка valueList
        if (Main.Sumrabbit > 10) yMaximum = Main.Sumrabbit;
        float yMin = 1;//valueList.Min; //Вычисляем минимальное значение  по Y для всех значений списка valueList
        float xMaximum = valueList.Count - 1; //Вычисляем максимальное значение по Х для всех значений списка valueList. Оно равно количеству записей в списке.
        float xSize = graphWidth / xMaximum; //50f;//Вычисляем нормировочный коэффициент масштабирования по X
        float ySize = (graphHeight - 15) / (yMaximum - yMin); //100f;//Вычисляем нормировочный коэффициент масштабирования по Y
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
        //Перенести инициализацию на вверх
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
        Debug.Log("Start: valueList.Count = " + valueList.Count);
    }
}
