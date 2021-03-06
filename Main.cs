using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    public static GameObject[,] grass = new GameObject[100, 100];//матрица элементов травы
    public static GameObject[,] grassFantom = new GameObject[100, 100];//фантомная матрица элементов травы
    readonly GameObject plant;//закрытый игровой объект травы
    public static GameObject[,] rock = new GameObject[100, 100];//матрица элементов камней
    readonly GameObject pebble;//закрытый игровой объект камня
    int i, j;//элементы для матрицы
    public GameObject Grass;//игровой объект травы
    public GameObject Rock;//игровой объект травы
    public GameObject information;//игровой объект в виде панели для отображения и изменения информации объектов симулятора
    public GameObject Window_grapg1;//игровой объект в виде панели для отображения графика об информации в симуляторе
    public GameObject help;//игровой объект в виде панели для отображения подсказки о запуске панели информации
    public GameObject help2;//игровой объект в виде панели для отображения подсказки о запуске панели информации
    public GameObject Targetgame;//Цель симуляциии
    public GameObject winRabbit;//победа кроликов
    public GameObject winFox;//победа лис
    public GameObject GameOver;//появляется текст GameOver, когда на сцене нет кроликов
    public GameObject Waring;//Предупреждение о выходе за диапазон регулирования скорости роста травы
    public GameObject prefab;//1 префаб для спавна
    public GameObject prefab2;//2 префаб для спавна
    public GameObject prefab3;//3 префаб для спавна
    private Ray ray;//технология, которая преобразует ограниченный набор данных (максимально упрощенная карта, либо же план этажа)
                   //в 3D проекцию путем «бросания лучей» из точки обзора по всей области видимости
    private RaycastHit hit;//некоторый луч, испускаемый из некоторого объекта в некотором направлении некоторой длины
                           //(либо бесконечный) для определения коллизий(столкновений) с объектами.
    public static int grassSum1 = 0;//количество травы, значение которого берется из скрипта кролика и выводится на экран
    public static int FoxSum = -1;//количество лис;
    public static int eatGrass = 0;//колличество съеденной травы, значение которого берется из скрипта кролика и выводится на экран
    private int xGrass, yGrass;//коррдинаты травы
    public static float speedGame;//скорость игры, которое выводится на экран
    public static float grassSpeed = 10f;//расчет скорости роста травы
    public static int Sumrabbit = -1; //Количество кроликов
    public float time = 1;//количество игрового времени и скорости игры
    public float CurrentTime;//считает колличество секунд
    public float GameSeconds;//количество секунд
    public float GameMinutes;//количество минут
    public int screenGrassSpeed = 5;//скорость роста травы, которое выводится на экран
    public bool music = false;//Обработка нажатия клавишы M
    public static bool start = false;//Проверка на старт цели симуляции
    public bool escape = false;//обработка нажатия клавиши Esc
    public bool tab = false;//обработка нажатия клавиши Tab
    public bool rab = false;//Проверка на выбор спавна кролика
    public bool fos = false;//Проверка на выбор спавна лисы
    public bool grac = false;//Проверка на выбор спавна травы
    string StringSecond;//количество секунд в виде строки
    string StringMinutes;//количество минут в виде строки
    public AudioClip OpenMenu;//включение звука открытия панели управления
    public AudioClip CloseMenu;//включение звука закрытия панели управления
    public AudioClip OpenGraph;//включение звука открытия графика
    public AudioClip CloseGraph;//включение звука закрытия графика
    public AudioClip gameOver;//включение голоса gameOver
    public AudioClip gameOver2;//включение звука gameOver
    public AudioClip BackFon;//включение музыки
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
    public Text Spawn;//субъект для спавна
    [Header("Button")]//название типа элемента в программе (кнопки)
    public Button exit;//кнопка: выход из симулятора
    public Button exitWinRabbit;//кнопка: выход из симулятора
    public Button exitWinFox;//кнопка: выход из симулятора
    public Button stop;//кнопка: остановка симулятора
    public Button continue1;//кнопка: возобновление симуляции
    public Button close;//кнопка: закрытие панели информации и ее изменений
    public Button close2;//кнопка: закрытие панели изображения графика
    public Button CloseHelp;//кнопка: закрытие панели информации и ее изменений
    public Button OpenHelp;//кнопка: закрытие панели изображения графика
    public Button backMenu;//кнопка: открытие главного меню
    public Button plusspeedgame;//кнопка: увеличение скорости симуляции
    public Button minusspeedgame;//кнопка: уменьшение скорости симуляции
    public Button plusgrowthrate;//кнопка: увеличение скорости роста травы
    public Button minusgrowthrate;//кнопка: уменьшение скорости роста травы
    public Button plusrabbithealth;//кнопка увеличивающая жизнь кролику
    public Button minusrabbithealth;//кнопка уменьшающая жизнь кролику
    public Button pluseatgrass;//кнопка увеличивающая колличество сЪеденной травы
    public Button minuseatgrass;//кнопка уменьшающая колличество сЪеденной травы
    public Button plusfoxhealth;//кнопка увеличивающая жизнь лисе
    public Button minusfoxhealth;//кнопка уменьшающая жизнь лисе
    public Button fox;//кнопка увеличивающая количество созданных лис
    public Button rabbit;//кнопка увеличивающая количество созданных кроликов
    public Button gras;//кнопка: рост травы
    [Header("Keys")]//название типа элемента в программе (клавишы)
    public KeyCode Escape = KeyCode.Escape;//обработка нажатия клавишы Esc
    public KeyCode Tab = KeyCode.Tab;//обработка нажатия клавишы Tab
    public KeyCode F = KeyCode.F;//обработка нажатия клавишы Enter
    public KeyCode M = KeyCode.M;//обработка нажатия клавишы M
    public KeyCode N = KeyCode.N;//обработка нажатия клавишы N

    void Start()
    {
        exit.onClick.AddListener(Exit);
        exitWinRabbit.onClick.AddListener(Exit);
        exitWinFox.onClick.AddListener(Exit);
        stop.onClick.AddListener(Stop);
        continue1.onClick.AddListener(Continue);
        close.onClick.AddListener(Closemenu);
        close2.onClick.AddListener(Closegraph);
        CloseHelp.onClick.AddListener(Closehelp);
        OpenHelp.onClick.AddListener(Openhelp);
        backMenu.onClick.AddListener(BackMenu);
        fox.onClick.AddListener(Fox);
        rabbit.onClick.AddListener(Rabbit);
        gras.onClick.AddListener(Gras);
        plusspeedgame.onClick.AddListener(Plusspeedgame);
        minusspeedgame.onClick.AddListener(Minusspeedgame);
        plusgrowthrate.onClick.AddListener(Plusgrowthrate);
        minusgrowthrate.onClick.AddListener(Minusgrowthrate);
        plusfoxhealth.onClick.AddListener(Plusfoxhealth);
        minusfoxhealth.onClick.AddListener(Minusfoxhealth);
        plusrabbithealth.onClick.AddListener(Plusrabbithealth);
        minusrabbithealth.onClick.AddListener(Minusrabbithealth);
        pluseatgrass.onClick.AddListener(Pluseatgrass);
        minuseatgrass.onClick.AddListener(Minuseatgrass);
        Time.timeScale = 1;
        GrassSeeder(50, 50);//cделано чтобы в начале было больше травы
        GrassSeeder(55, 50);
        GrassSeeder(55, 55);
        GrassSeeder(60, 55);
        GrassSeeder(45, 50);
        GrassSeeder(45, 45);

        GrassSeeder(95, 99);
        GrassSeeder(90, 95);
        GrassSeeder(90, 90);
        GrassSeeder(85, 90);
        GrassSeeder(85, 85);
        GrassSeeder(80, 85);

        i = 0;//нижняя стена из камней
        for (j = 0; j < 100; j++)
        {
            RockSeeder(i, j);
        }

        i = 99;//верхняя стена из камней
        for (j = 0; j < 100; j++)
        {
            RockSeeder(i, j);
        }

        j = 0;//правая стена из камней
        for (i = 0; i < 100; i++)
        {
            RockSeeder(i, j);
        }

        j = 99;//левая стена из камней
        for (i = 0; i < 100; i++)
        {
            RockSeeder(i, j);
        }

        i = 50;//нижняя стена из камней
        for (j = 0; j < 40; j++)
        {
            RockSeeder(i, j);
        }

        j = 50;//левая стена из камней
        for (i = 0; i < 50; i++)
        {
            RockSeeder(i, j);
        }
        //InvokeRepeating("grassRun", grassSpeed, grassSpeed);
        Sumrabbit = -1;
        FoxSum = -1;
        grassSum1 = 12;
        fos = false;
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

    public void BackMenu()
    {
        winRabbit.SetActive(false);
        winFox.SetActive(false);
        if (SceneManager.GetActiveScene().name == "SampleScene") SceneManager.LoadScene("Start");
    }

    public void Closemenu()
    {
        GetComponent<AudioSource>().PlayOneShot(CloseMenu);
        escape = false;
        information.SetActive(false);
    }

    public void Closegraph()
    {
        GetComponent<AudioSource>().PlayOneShot(CloseGraph);
        Window_grapg1.SetActive(false);
    }

    public void Closehelp()
    {
        help.SetActive(false);
    }

    public void Openhelp()
    {
        help.SetActive(true);
    }

    public void Rabbit()
    {
        rab = true;
        fos = false;
        grac = false;
    }

    public void Fox()
    {
        fos = true;
        rab = false;
        grac = false;
    }

    public void Gras()
    {
        grac = true;
        fos = false;
        rab = false;
    }

    public void Plusfoxhealth()
    {
        AI_fox.StartHealth++;
    }

    public void Minusfoxhealth()
    {
        AI_fox.StartHealth--;
    }

    public void Plusrabbithealth()
    {
        AI_rabbit.StartHealth++;
    }

    public void Minusrabbithealth()
    {
        AI_rabbit.StartHealth--;
    }

    public void Pluseatgrass()
    {
        AI_rabbit.counterGrass++;
    }

    public void Minuseatgrass()
    {
        AI_rabbit.counterGrass--;
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
        if (screenGrassSpeed < 10) screenGrassSpeed++;
        if (screenGrassSpeed > 9) Waring.SetActive(true);
        if (screenGrassSpeed < 9) Waring.SetActive(false);
    }

    public void Minusgrowthrate()
    {
        if (screenGrassSpeed > 0) screenGrassSpeed--;
        if (screenGrassSpeed < 1) Waring.SetActive(true);
        if (screenGrassSpeed > 1) Waring.SetActive(false);
    }

    private void GrassSeeder(int x, int y)
    {
        GameObject plant = Instantiate(Grass, new Vector3(x * 0.5f + Random.Range(-0.1f, 0.1f), 0, y * 0.5f + Random.Range(-0.1f, 0.1f)), transform.rotation);
        grass[x, y] = plant; // - трава
        grassSum1++;
    }

    private void GrassFantomSeeder(int x, int y)
    {
        GameObject plant = Instantiate(Grass, new Vector3(x * 0.5f + Random.Range(-0.1f, 0.1f), 0, y * 0.5f + Random.Range(-0.1f, 0.1f)), transform.rotation);
        grassFantom[x, y] = plant; // - трава
        grassSum1++;
    }

    private void RockSeeder(int x, int y)
    {
        GameObject pebble = Instantiate(Rock, new Vector3(x * 0.5f + Random.Range(-0.1f, 0.1f), 0, y * 0.5f + Random.Range(-0.1f, 0.1f)), transform.rotation);
        rock[x, y] = pebble; // - камень
    }

    private void GrassRun()
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
                    if (i != 100 - 1) if (grassFantom[i + 1, j] == null) GrassFantomSeeder(i + 1, j);
                    if (i != 0) if (grassFantom[i - 1, j] == null) GrassFantomSeeder(i - 1, j);
                    if (j != 100 - 1) if (grassFantom[i, j + 1] == null) GrassFantomSeeder(i, j + 1);
                    if (j != 0) if (grassFantom[i, j - 1] == null) GrassFantomSeeder(i, j - 1);
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
        grassSpeed = (10 - screenGrassSpeed) * 2;
        time += Time.deltaTime;
        if (time % grassSpeed >= grassSpeed - Time.deltaTime)
        {
            GrassRun();
        }

        if (rab == true)
        {
            Spawn.text = "Субъект спавна = " + prefab;
            if (Input.GetKey(KeyCode.Mouse0) && escape != true)
            {
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {

                    if (hit.transform.CompareTag("ground"))
                    {
                        prefab.tag = "rabbit";
                        GameObject obj = Instantiate(prefab, new Vector3(hit.point.x, 0, hit.point.z), Quaternion.identity) as GameObject;
                        prefab.tag = "Spawn";
                    }
                }
            }
            fos = false;
            grac = false;
        }

        if (fos == true)
        {
            Spawn.text = "Субъект спавна = " + prefab2;
            if (Input.GetKey(KeyCode.Mouse0) && escape != true)
            {
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {

                    if (hit.transform.CompareTag("ground"))
                    {
                        prefab2.tag = "fox";
                        GameObject obj = Instantiate(prefab2, new Vector3(hit.point.x, 0, hit.point.z), Quaternion.identity) as GameObject;
                        prefab2.tag = "Spawn";
                    }
                }
            }
            rab = false;
            grac = false;
        }

        if (grac == true)
        {
            Spawn.text = "Субъект спавна = " + prefab3;
            if (Input.GetKey(KeyCode.Mouse0) && escape != true)
            {
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {

                    if (hit.transform.CompareTag("ground"))
                    {
                       // GameObject obj = Instantiate(prefab3, new Vector3(hit.point.x, 0, hit.point.z), Quaternion.identity) as GameObject;
                        xGrass = Mathf.RoundToInt(hit.point.x * 2f) - 1;
                        yGrass = Mathf.RoundToInt(hit.point.z * 2f) - 1;
                        GrassSeeder(xGrass, yGrass);
                    }
                }
            }
            rab = false;
            fos = false;
        }
        
        if (Input.GetKeyDown(Escape))
        {
            GetComponent<AudioSource>().PlayOneShot(OpenMenu);
            escape = !escape;//Переводит в противоположный знак (станет true после 1 нажатия)
            if (escape) information.SetActive(true);
            else information.SetActive(false);
        }
        if (Input.GetKeyDown(Tab))
        {
            GetComponent<AudioSource>().PlayOneShot(OpenGraph);
            tab = !tab;//Переводит в противоположный знак (станет true после 1 нажатия)
            if (tab) Window_grapg1.SetActive(true);
            else Window_grapg1.SetActive(false);
        }
        if (Input.GetKeyDown(M) && music == false)
        {
            music = true;
            GetComponent<AudioSource>().PlayOneShot(BackFon);
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
            information.SetActive(false);
            winRabbit.SetActive(true);
        }
        if (AI_fox.eatRabbit == 5)
        {
            Time.timeScale = 0f;
            Window_grapg1.SetActive(false);
            information.SetActive(false);
            winFox.SetActive(true);
        }

        if (Sumrabbit == 0)//исправить ошибку
        {
            GameOver.SetActive(true);
            GetComponent<AudioSource>().PlayOneShot(gameOver2);
            GetComponent<AudioSource>().PlayOneShot(gameOver);
        }

        speedGame = Time.timeScale;// для вывода информации в unity в inspector
        GameSeconds = GameSeconds + Time.deltaTime + .0f;
        StringSecond = GameSeconds.ToString("###");
        if (GameMinutes != 0) StringMinutes = GameMinutes.ToString() + ":";
        else
        {
            StringMinutes = "";
        }
        //CurrentTime += Time.deltaTime;

        if (GameSeconds >= 60.0f)
        {
            GameMinutes++;
            GameSeconds = 0.0f;
        }

        rabbits.text = "Популяция кроликов = " + Sumrabbit;
        grass2.text = "Количество травы = " + grassSum1;
        Speedgame.text = "Скорость симуляции = " + speedGame;
        growthrateGrass.text = "Скорость роста травы = " + screenGrassSpeed;
        Healthrabbit.text = "Здоровье кролика = " + AI_rabbit.StartHealth;
        Eatgrass.text = "Количество съеденной травы = " + AI_rabbit.counterGrass;
        Healthfox.text = "Здоровье лисы = " + AI_fox.StartHealth;
        Healthcounter.text = "Количество лис = " + FoxSum;
        TextTime.text = "Время - " + StringMinutes + StringSecond;
        eatGrass2.text = "Съеденная трава = " + AI_rabbit.eatGrass2;
        eatRabbit.text = "Съеденные кролики = " + AI_fox.eatRabbit;
    }
}