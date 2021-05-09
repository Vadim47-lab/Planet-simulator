using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    public GameObject[,] grass = new GameObject[100, 100];//матрица элементов травы
    public GameObject[,] grassFantom = new GameObject[100, 100];//фантомная матрица элементов травы
    GameObject plant;//закрытый игровой объект травы
    int i, j;//элементы для матрицы
    public GameObject Grass;//игровой объект травы
    public GameObject rabbit5;//пустой игровой объект со скриптом мозга кролика
    public GameObject information;//игровой объект в виде панели для отображения и изменения информации объектов симулятора
    public GameObject help;//игровой объект в виде панели для отображения подсказки о запуске панели информации
    public float grassSum1 = 0;//колличество травы, значение которого берется из скрипта кролика и выводится на экран
    public static float eatGrass = 0;//колличество съеденной травы, значение которого берется из скрипта кролика и выводится на экран
    public float speedGame;//скорость игры, которое выводится на экран
    public float grassSpeed = 0.1f;//скорость роста травы, которое выводится на экран
    public float time = 1;
    public float CurrentTime;//считает колличество секунд
    public float GameSeconds;//количество секунд
    public float GameMinutes;//количество минут
    string StringSecond;//количество секунд в виде строки
    string StringMinutes;//количество минут в виде строки
    [Header("Text")]//название типа элемента в программе (вывод текста и значений)
    public Text rabbits;//блок для вывода информации о колличестве кроликах
    public Text grass2;//блок для вывода информации о колличестве травы
    public Text Speedgame;//блок для вывода информации о скорости игры
    public Text growthrateGrass;//блок для вывода информации о скорости роста травы
    public Text Healthrabbit;//блок для вывода информации о колличестве здоровья кролика
    public Text Healthfox; //блок для вывода информации о колличестве здоровья лисы
    public Text Eatgrass;//блок для вывода информации о колличестве съеденной травы
    public Text TextTime;//блок для вывода информации о колличестве времени действия симуляции
    [Header("Button")]//название типа элемента в программе (кнопки)
    public Button exit;//кнопка: выход из симулятора
    public Button stop;//кнопка: остановка симулятора
    public Button continue1;//кнопка: возобновление симуляции
    public Button close;//кнопка: закрытие панели информации и ее изменений
    public Button plusspeedgame;//кнопка: увеличение скорости симуляции
    public Button minusspeedgame;//кнопка: уменьшение скорости симуляции
    public Button plusgrowthrate;//кнопка: увеличение скорости роста травы
    public Button minusgrowthrate;//кнопка: уменьшение скорости роста травы
    public KeyCode showMenuKey = KeyCode.Escape;//обработка нажатия клавишы Esc

    void Start()
    {
        exit.onClick.AddListener(Exit);
        stop.onClick.AddListener(Stop);
        continue1.onClick.AddListener(Continue);
        close.onClick.AddListener(Close);
        plusspeedgame.onClick.AddListener(Plusspeedgame);
        minusspeedgame.onClick.AddListener(Minusspeedgame);
        plusgrowthrate.onClick.AddListener(Plusgrowthrate);
        minusgrowthrate.onClick.AddListener(Minusgrowthrate);
        Time.timeScale = 1;
        grassSeeder(50, 50);
        grassSeeder(55, 50);
        grassSeeder(51, 53);
        grassSeeder(40, 40);
        grassSeeder(45, 45);
        grassSeeder(41, 41);
        grassSeeder(50, 50);
        grassSeeder(55, 50);
        grassSeeder(51, 53);
        grassSeeder(40, 40);
        grassSeeder(45, 45);
        grassSeeder(41, 41);
        InvokeRepeating("grassRun", grassSpeed, grassSpeed);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Stop()
    {
        Time.timeScale = 0f;
    }

    public void Continue()
    {
        Time.timeScale = 1f;
    }

    public void Close()
    {
        information.SetActive(false);
        help.SetActive(true);
    }

    public void Plusspeedgame()
    {
        Time.timeScale++;
    }

    public void Minusspeedgame()
    {
        Time.timeScale--;
    }

    public void Plusgrowthrate()
    {
        grassSpeed++;
    }

    public void Minusgrowthrate()
    {
        grassSpeed--;
    }

    private void grassSeeder(int x, int y)
    {
        GameObject plant = Instantiate(Grass, new Vector3(x * 0.5f + Random.Range(-0.4f, 0.4f), 0, y * 0.5f + Random.Range(-0.4f, 0.4f)), transform.rotation);
        grass[x, y] = plant; // - трава
        grassSum1++;
    }

    private void grassFantomSeeder(int x, int y)
    {
        GameObject plant = Instantiate(Grass, new Vector3(x * 0.5f + Random.Range(-0.4f, 0.4f), 0, y * 0.5f + Random.Range(-0.4f, 0.4f)), transform.rotation);
        grassFantom[x, y] = plant; // - трава
    }

    private void grassRun()
    {
        for (i = 0; i < 100; i++)
        {
            for (j = 0; j < 100; j++)
            {
                grassFantom[i, j] = grass[i, j];  
            }
        }

        for (i = 0; i < 100; i++)
        {
            for (j = 0; j < 100; j++)
            {
                if (grass[i, j] != null)
                {
                    if (i != 100 - 1) if (grassFantom[i + 1, j] == null) grassFantomSeeder(i + 1, j);
                    if (i != 0) if (grassFantom[i - 1, j] == null) grassFantomSeeder(i - 1, j);
                    if (j != 100 - 1) if (grassFantom[i, j + 1] == null) grassFantomSeeder(i, j + 1);
                    if (j != 0) if (grassFantom[i, j - 1] == null) grassFantomSeeder(i, j - 1);
                }
            }
        }

        for (i = 0; i < 100; i++)//порекомендовали grass.Length
        {
            for (j = 0; j < 100; j++)
            {
                grass[i, j] = grassFantom[i, j];
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(showMenuKey))
        {
            information.SetActive(true);
            help.SetActive(false);
        }

        grassSum1 = grassSum1 + AI_rabbit.grassSum;
        speedGame = Time.timeScale;// для вывода информации в unity в inspector

        GameSeconds = GameSeconds + Time.deltaTime;
        StringSecond = GameSeconds.ToString();
        StringMinutes = GameMinutes.ToString();
        CurrentTime += Time.deltaTime;

        if (GameSeconds >= 60.0f)
        {
            GameMinutes = GameMinutes + 1.0f;
            GameSeconds = 0.0f;
        }

        rabbits.text = "Популяция кроликов = " + AI_rabbit.counter2;
        grass2.text = "Количество травы = " + grassSum1;
        Speedgame.text = "Скорость симуляции = " + speedGame;
        growthrateGrass.text = "Скорость роста травы = " + grassSpeed;
        Healthrabbit.text = "Здоровье кролика = " + AI_rabbit.health;
        Eatgrass.text = "Количество съеденной травы = " + AI_rabbit.counterGrass;
        Healthfox.text = "Здоровье лисы = " + AI_fox.health;
        TextTime.text = "Время - " + StringMinutes + ":" + StringSecond;
    }
}