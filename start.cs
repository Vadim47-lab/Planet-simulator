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
    public AudioClip CloseMenu;
    public AudioClip OpenAuthor;
    public AudioClip CloseAuthor;
    [Header("GameObject")]//название типа элемента в программе (вывод игровых объектов)
    public GameObject Menu;//игровой объект в виде панели для отображения и изменения информации объектов симулятора  public GameObject Window_grapg1;//игровой объект в виде панели для отображения графика об информации в симуляторе
    public GameObject Author1;//игровой объект в виде панели для отображения автора игры
    [Header("Text")]//название типа элемента в программе (вывод текста и значений)
    public Text Speedgame;//блок для вывода информации о скорости игры
    public Text growthrateGrass;//блок для вывода информации о скорости роста травы
    public Text Healthrabbit;//блок для вывода информации о количестве здоровья кролика
    public Text Healthfox; //блок для вывода информации о количестве здоровья лисы
    public Text Healthcounter; //блок для вывода информации о количестве созданных лис
    public Text Eatgrass;//блок для вывода информации о количестве съеденной травы
    public Text TextTime;//блок для вывода информации о количестве времени действия симуляции
    [Header("Button")]//название типа элемента в программе (кнопки)
    public Button exit;//кнопка: выход из симулятора
    public Button settings;//кнопка: показ настроек игры
    public Button start1;//кнопка: запуск симуляции
    public Button close;//кнопка: закрытие панели настройки
    public Button close2;//кнопка: закрытие панели автор
    public Button author;//кнопка: открытие панели автор
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

    void Start()
    {
        exit.onClick.AddListener(Exit);
        settings.onClick.AddListener(Settings);
        start1.onClick.AddListener(Start1);
        close.onClick.AddListener(Close);
        close2.onClick.AddListener(Close2);
        author.onClick.AddListener(Author);
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
        AI_fox.StartHealth++;
    }

    public void Minusfoxhealth()
    {
        AI_fox.StartHealth--;
    }

    public void Plusfoxcounter()
    {
        Main.FoxSum++;
        Instantiate(AI_fox.Fox, transform.position, transform.rotation);
    }

    public void Minusfoxcounter()
    {
        Main.FoxSum--;
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

    public void Exit()
    {
        Application.Quit();
    }

    public void Settings()
    {
        GetComponent<AudioSource>().PlayOneShot(OpenMenu);
        Menu.SetActive(true);
    }

    public void Author()
    {
        GetComponent<AudioSource>().PlayOneShot(OpenAuthor);
        Author1.SetActive(true);
    }

    public void Close()
    {
        GetComponent<AudioSource>().PlayOneShot(CloseMenu);
        Menu.SetActive(false);
    }

    public void Close2()
    {
        GetComponent<AudioSource>().PlayOneShot(CloseAuthor);
        Author1.SetActive(false);
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
        Main.grassSpeed++;
    }

    public void Minusgrowthrate()
    {
        Main.grassSpeed--;
    }

    void Update()
    {
        GameSeconds = GameSeconds + Time.deltaTime;
        StringSecond = GameSeconds.ToString();
        StringMinutes = GameMinutes.ToString();
        CurrentTime += Time.deltaTime;

        if (GameSeconds >= 60.0f)
        {
            GameMinutes = GameMinutes + 1.0f;
            GameSeconds = 0.0f;
        }

        Speedgame.text = "Скорость симуляции = " + Main.speedGame;
        growthrateGrass.text = "Скорость роста травы = " + Main.grassSpeed;
        Healthrabbit.text = "Здоровье кролика = " + AI_rabbit.StartHealth;
        Eatgrass.text = "Количество съеденной травы = " + AI_rabbit.counterGrass;
        Healthfox.text = "Здоровье лисы = " + AI_fox.StartHealth;
        Healthcounter.text = "Количество лис = " + Main.FoxSum;
        TextTime.text = "Время - " + StringMinutes + ":" + StringSecond;
    }
}