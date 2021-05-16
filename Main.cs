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
    public GameObject Window_grapg1;//игровой объект в виде панели для отображения графика об информации в симуляторе
    public GameObject help;//игровой объект в виде панели для отображения подсказки о запуске панели информации
    public GameObject help2;//игровой объект в виде панели для отображения подсказки о запуске панели информации
    public GameObject Targetgame;//Цель симуляциии
    public GameObject winRabbit;//победа кроликов
    public GameObject winFox;//победа лис
    public GameObject GameOver;
    public static int grassSum1 = 0;//количество травы, значение которого берется из скрипта кролика и выводится на экран
    public static int FoxSum = 0;//количество травы, значение которого берется из скрипта кролика и выводится на экран
    public static int eatGrass = 0;//колличество съеденной травы, значение которого берется из скрипта кролика и выводится на экран
    public float speedGame;//скорость игры, которое выводится на экран
    public float grassSpeed = 0.1f;//скорость роста травы, которое выводится на экран
    public static int Sumrabbit = 2; //Количество кроликов
    public float time = 1;
    public float CurrentTime;//считает колличество секунд
    public float GameSeconds;//количество секунд
    public float GameMinutes;//количество минут
    public static bool start = false;//Проверка на старт цели симуляции
    public bool escape = false;
    string StringSecond;//количество секунд в виде строки
    string StringMinutes;//количество минут в виде строки
    public AudioClip OpenMenu;
    public AudioClip OpenGraph;
    public AudioClip CloseMenu;
    public AudioClip CloseGraph;
    public AudioClip Backfon;
    public AudioClip gameOver;
    public AudioClip gameOver2;
    [Header("Text")]//название типа элемента в программе (вывод текста и значений)
    public Text rabbits;//блок для вывода информации о количестве кроликах
    public Text grass2;//блок для вывода информации о количестве травы
    public Text Speedgame;//блок для вывода информации о скорости игры
    public Text growthrateGrass;//блок для вывода информации о скорости роста травы
    public Text Healthrabbit;//блок для вывода информации о количестве здоровья кролика
    public Text Healthfox; //блок для вывода информации о количестве здоровья лисы
    public Text Healthcounter; //блок для вывода информации о количестве созданных лис
    public Text Eatgrass;//блок для вывода информации о количестве съеденной травы
    public Text TextTime;//блок для вывода информации о количестве времени действия симуляции
    public Text eatGrass2;//блок для вывода информации о количестве съеденной травы при старте цели симуляции
    public Text eatRabbit;//блок для вывода информации о количестве съеденных кроликов при старте цели симуляции
    [Header("Button")]//название типа элемента в программе (кнопки)
    public Button exit;//кнопка: выход из симулятора
    public Button stop;//кнопка: остановка симулятора
    public Button continue1;//кнопка: возобновление симуляции
    //public Button continue2;//кнопка: возобновление симуляции
    //public Button continue3;//кнопка: возобновление симуляции
    public Button close;//кнопка: закрытие панели информации и ее изменений
    public Button close2;//кнопка: закрытие панели изображения графика
    public Button plusspeedgame;//кнопка: увеличение скорости симуляции
    public Button minusspeedgame;//кнопка: уменьшение скорости симуляции
    public Button plusgrowthrate;//кнопка: увеличение скорости роста травы
    public Button minusgrowthrate;//кнопка: уменьшение скорости роста травы
    [Header("Keys")]//название типа элемента в программе (клавишы)
    public KeyCode Escape = KeyCode.Escape;//обработка нажатия клавишы Esc
    public KeyCode Tab = KeyCode.Tab;//обработка нажатия клавишы Tab
    public KeyCode F = KeyCode.F;//обработка нажатия клавишы Enter
    public KeyCode M = KeyCode.M;//обработка нажатия клавишы M

    void Start()
    {
        //StartCoroutine(gameOver3());
        escape = true;
        exit.onClick.AddListener(Exit);
        stop.onClick.AddListener(Stop);
        continue1.onClick.AddListener(Continue);
        //continue2.onClick.AddListener(Continue2);
        //continue3.onClick.AddListener(Continue3);
        close.onClick.AddListener(Close);
        close2.onClick.AddListener(Close2);
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
        Sumrabbit = 2;
        FoxSum = 0;
    }

    /*IEnumerator gameOver3()
    { 
            GameOver.SetActive(true);
            yield return new WaitForSeconds(6.5f);
            GetComponent<AudioSource>().PlayOneShot(gameOver2);
            GetComponent<AudioSource>().PlayOneShot(gameOver);
    }*/

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

    /*public void Continue2()
    {
        winRabbit.SetActive(false);
        Time.timeScale = 1f;
        Targetgame.SetActive(false);
        information.SetActive(false);
        escape = false;
        start = false;
    }

    public void Continue3()
    {
        winFox.SetActive(false);
        Time.timeScale = 1f;
        Targetgame.SetActive(false);
        information.SetActive(false);
        escape = false;
        start = false;
    }*/

    public void Close()
    {
        GetComponent<AudioSource>().PlayOneShot(CloseMenu);
        escape = false;
        information.SetActive(false);
        help.SetActive(true);
    }

    public void Close2()
    {
        GetComponent<AudioSource>().PlayOneShot(CloseGraph);
        Window_grapg1.SetActive(false);
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
        grassSum1++;
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
        if (Input.GetKeyDown(M))
        {
            GetComponent<AudioSource>().PlayOneShot(Backfon);
        }

        if (Input.GetKeyDown(Escape))
        {
            GetComponent<AudioSource>().PlayOneShot(OpenMenu);
            escape = true;
            information.SetActive(true);
            help.SetActive(false);
        }
        if (Input.GetKeyDown(Tab))
        {
            GetComponent<AudioSource>().PlayOneShot(OpenGraph);
            Window_grapg1.SetActive(true);
            help.SetActive(false);
        }
        if (escape == true && Input.GetKeyDown(F))
        {
            start = true;
            Targetgame.SetActive(true);
            help2.SetActive(false);
        }

        if (AI_rabbit.eatGrass2 == 30)
        {
            Time.timeScale = 0f;
            Window_grapg1.SetActive(false);
            winRabbit.SetActive(true);
        }
        if (AI_fox.eatRabbit == 5)
        {
            Time.timeScale = 0f;
            Window_grapg1.SetActive(false);
            winFox.SetActive(true);
        }
        if (Sumrabbit == 0)
        {
            GameOver.SetActive(true);
            GetComponent<AudioSource>().PlayOneShot(gameOver2);
            GetComponent<AudioSource>().PlayOneShot(gameOver);
        }

        // grassSum1 = grassSum1 + AI_rabbit.grassSum;
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

        rabbits.text = "Популяция кроликов = " + Sumrabbit;
        grass2.text = "Количество травы = " + grassSum1;
        Speedgame.text = "Скорость симуляции = " + speedGame;
        growthrateGrass.text = "Скорость роста травы = " + grassSpeed;
        Healthrabbit.text = "Здоровье кролика = " + AI_rabbit.health;
        Eatgrass.text = "Количество съеденной травы = " + AI_rabbit.counterGrass;
        Healthfox.text = "Здоровье лисы = " + AI_fox.health;
        Healthcounter.text = "Количество лис = " + FoxSum;
        TextTime.text = "Время - " + StringMinutes + ":" + StringSecond;
        eatGrass2.text = "Съеденная трава = " + AI_rabbit.eatGrass2;
        eatRabbit.text = "Съеденные кролики = " + AI_fox.eatRabbit;
    }
}