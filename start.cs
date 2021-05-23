using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class start : MonoBehaviour
{
    [Header("variable")]//название типа элемента в программе (вывод переменных)
    public float CurrentTime;//считает колличество секунд
    public float GameSeconds;//количество секунд
    public float GameMinutes;//количество минут
    string StringSecond;//количество секунд в виде строки
    string StringMinutes;//количество минут в виде строки
    [Header("AudioClip")]//название типа элемента в программе (вывод аудиоклипа)
    public AudioClip OpenMenu;
    [Header("GameObject")]//название типа элемента в программе (вывод игровых объектов)
    public GameObject information;//игровой объект в виде панели для отображения и изменения информации объектов симулятора  public GameObject Window_grapg1;//игровой объект в виде панели для отображения графика об информации в симуляторе
    public GameObject help;//игровой объект в виде панели для отображения подсказки о запуске панели информации
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
    [Header("Button")]//название типа элемента в программе (кнопки)
    public Button exit;//кнопка: выход из симулятора
    public Button stop;//кнопка: остановка симулятора
    public Button start1;//кнопка: возобновление симуляции
    public Button continue1;//кнопка: возобновление симуляции
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
    public Button plusfoxcounter;//кнопка увеличивающая количество созданных лис
    public Button minusfoxcounter;//кнопка уменьшающая количество созданных лис
    [Header("Keys")]//название типа элемента в программе (клавишы)
    public KeyCode Escape = KeyCode.Escape;//обработка нажатия клавишы Esc

    void Start()
    {
        exit.onClick.AddListener(Exit);
        stop.onClick.AddListener(Stop);
        start1.onClick.AddListener(Start1);
        continue1.onClick.AddListener(Continue);
        plusspeedgame.onClick.AddListener(Plusspeedgame);
        minusspeedgame.onClick.AddListener(Minusspeedgame);
        plusgrowthrate.onClick.AddListener(Plusgrowthrate);
        minusgrowthrate.onClick.AddListener(Minusgrowthrate);
        plusfoxhealth.onClick.AddListener(Plusfoxhealth);
        minusfoxhealth.onClick.AddListener(Minusfoxhealth);
        plusfoxcounter.onClick.AddListener(Plusfoxcounter);
        minusfoxcounter.onClick.AddListener(Minusfoxcounter);
        plusrabbithealth.onClick.AddListener(Plusrabbithealth);
        minusrabbithealth.onClick.AddListener(Minusrabbithealth);
        pluseatgrass.onClick.AddListener(Pluseatgrass);
        minuseatgrass.onClick.AddListener(Minuseatgrass);
    }

    public void Plusfoxhealth()
    {
        AI_fox.health++;
    }

    public void Minusfoxhealth()
    {
        AI_fox.health--;
    }

    public void Plusfoxcounter()
    {
        Debug.Log("Лиса Рождается++");
        Main.FoxSum++;
        Instantiate(AI_fox.Fox, transform.position, transform.rotation);
    }

    public void Minusfoxcounter()
    {
        Main.FoxSum--;
    }

    public void Plusrabbithealth()
    {
        AI_rabbit.health++;
    }

    public void Minusrabbithealth()
    {
        AI_rabbit.health--;
    }

    public void Pluseatgrass()
    {
        AI_rabbit.counterGrass++;
    }

    public void Minuseatgrass()
    {
        AI_rabbit.counterGrass--;
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

    public void Start1()
    {
        if (SceneManager.GetActiveScene().name == "Start") SceneManager.LoadScene("SampleScene");
    }

    public void Plusspeedgame()
    {
        Main.speedGame++;
    }

    public void Minusspeedgame()
    {
        Main.speedGame--;
    }

    public void Plusgrowthrate()
    {
        Main.GrassSpeed++;
    }

    public void Minusgrowthrate()
    {
        Main.GrassSpeed--;
    }

    void Update()
    {
        if (Input.GetKeyDown(Escape))
        {
            GetComponent<AudioSource>().PlayOneShot(OpenMenu);
            information.SetActive(true);
            help.SetActive(false);
        }
        GameSeconds = GameSeconds + Time.deltaTime;
        StringSecond = GameSeconds.ToString();
        StringMinutes = GameMinutes.ToString();
        CurrentTime += Time.deltaTime;

        if (GameSeconds >= 60.0f)
        {
            GameMinutes = GameMinutes + 1.0f;
            GameSeconds = 0.0f;
        }

        rabbits.text = "Популяция кроликов = " + Main.Sumrabbit;
        grass2.text = "Количество травы = " + Main.grassSum1;
        Speedgame.text = "Скорость симуляции = " + Main.speedGame;
        growthrateGrass.text = "Скорость роста травы = " + Main.GrassSpeed;
        Healthrabbit.text = "Здоровье кролика = " + AI_rabbit.health;
        Eatgrass.text = "Количество съеденной травы = " + AI_rabbit.counterGrass;
        Healthfox.text = "Здоровье лисы = " + AI_fox.health;
        Healthcounter.text = "Количество лис = " + Main.FoxSum;
        TextTime.text = "Время - " + StringMinutes + ":" + StringSecond;
    }
}