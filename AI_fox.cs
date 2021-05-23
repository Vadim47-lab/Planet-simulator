using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class AI_fox: MonoBehaviour
{
    private float z = 0;//коррдинаты кролика по оси z
    private float x = 0;//коррдинаты кролика по оси x
    private float y = 0;//коррдинаты кролика по оси y
    public float Health = 60;//колличество здоровья у лисы
    public static float eatRabbit = 0;
    public static float health = 0;//колличество здоровья у лисы, которое передается в файл Main для дальнейшего вывода на экран
    public GameObject target = null;//игровой объект для поедания лисой
    public GameObject fox;//игровой объект лиса
    public static GameObject Fox;//игровой объект лиса
    public int random2;//обход камня
    public int random;//мозг лисы
    public int maxChild = 3;//сколько лис можно родить
    public static int MaxChild = 0;
    private int counterRabbit = 2; //сколько нужно съесть кроликов для размножения
    public int age = 130;//биологический возраст максимальный
    public static int Age = 0;
    public static float counter4 = 1;//колисчество созданных лис, для вывода на экран в Main
   // public Button plusfoxhealth;//кнопка увеличивающая жизнь лисе
   // public Button minusfoxhealth;//кнопка уменьшающая жизнь лисе
   // public Button plusfoxcounter;//кнопка увеличивающая количество созданных лис
   // public Button minusfoxcounter;//кнопка уменьшающая количество созданных лис

    void Start()
    {
        Main.FoxSum++;
        //int counterRabbit = 2;
        maxChild = 3;
        age = 130;
      //  plusfoxhealth.onClick.AddListener(Plusfoxhealth);
      //  minusfoxhealth.onClick.AddListener(Minusfoxhealth);
      //  plusfoxcounter.onClick.AddListener(Plusfoxcounter);
       // minusfoxcounter.onClick.AddListener(Minusfoxcounter);
        target = null;
        //GameObject[] rabbits = GameObject.FindGameObjectsWithTag("rabbit");
        //target = rabbits[Random.Range(0, rabbits.Length)];
        y = 0f;
        Health = 60;
        InvokeRepeating("brain", 0, 1f);
        InvokeRepeating("life", 1f, 1f);
    }

    public void Plusfoxhealth()
    {
        Health++;
    }

    public void Minusfoxhealth()
    {
        Health--;
    }

    public void Plusfoxcounter()
    {
        Debug.Log("Лиса Рождается++");
        //Main.FoxSum++;
        Instantiate(fox, transform.position, transform.rotation);
    }

    public void Minusfoxcounter()
    {
        //Main.FoxSum--;
    }

    void Update()
    {
        Fox = fox;
        Age = age;
        MaxChild = maxChild;
        transform.Rotate(x, y, z);
        if (transform.position.y > 0.01)
        {
            //Main.FoxSum--;
            Destroy(gameObject);
        }
        health = Health;
    }

    private void brain()
    {
        age--;
        if (age == 0)
        {
            //Main.FoxSum--;
            Destroy(gameObject);
        }
        random = Random.Range(0, 10);
        if (Health <= 20) random = 11;
        switch (random)
        {
            case 0:
                y = 0;
                if (tag == "fox") GetComponent<Animator>().SetBool("Run", false);
                break;
            case 1:
            case 2:
            case 3:
            case 4:
                if (tag == "fox") GetComponent<Animator>().SetBool("Run", true);
                y = 0;
                break;
            case 5:
            case 6:
            case 7:
                if (tag == "fox") GetComponent<Animator>().SetBool("Run", true);
                y = 2;
                break;
            case 8:
            case 9:
            case 10:
                if (tag == "fox") GetComponent<Animator>().SetBool("Run", true);
                y = -2;
                break;
            case 11:
                y = 0;
                if (tag == "fox") GetComponent<Animator>().SetBool("Run", true);
                if (target == null)
                {
                    GameObject[] rabbits = GameObject.FindGameObjectsWithTag("rabbit");
                    if (rabbits.Length != 0)
                    {
                        target = rabbits[Random.Range(0, rabbits.Length)];
                    }
                }
                else
                {
                    transform.LookAt(target.transform);
                }
                break;
        }
    }

    private void life()
    {
        if (tag == "fox") Health--;
        if (Health <= 0 && tag == "fox")
        {
            //GetComponent<Animator>().SetBool("Death", true);
            Destroy(gameObject);
            //Main.FoxSum--;
        }
    }

    private void OnDestroy()
    {
        Main.FoxSum--;
    }

    private void OnTriggerEnter(Collider other)
    {
        random2 = Random.Range(0, 2);
        if (other.tag == "stone" && random2 == 0)
        {
            y = 2;
        }
        if (other.tag == "stone" && random2 == 1)
        {
            y = -2;
        }
        if (other.tag == "stone" && random2 == 2)
        {
            y = 0;
        }

        if (other.tag == "rabbit" && Health < 20)
        {
            Health += 20;
            target = null;
            Destroy(other.gameObject);
            if (Main.start == true) eatRabbit++;
            counterRabbit--;
            //Main.Sumrabbit--;

            if (counterRabbit <= 0)
            { //родим лису если съедено нужное количество кроликов
                counterRabbit = 2;
                //Main.FoxSum++;
                Health = 60;
                Instantiate(fox, transform.position, transform.rotation);
                maxChild--;
                if (maxChild == 0)
                {
                    //Main.FoxSum--;
                    Destroy(gameObject);
                }
            }
        }
    }
}