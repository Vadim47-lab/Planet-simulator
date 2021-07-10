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
    public float Health = 50;//колличество здоровья у лисы
    public static float eatRabbit = 0;
    public static float StartHealth = 60;//колличество здоровья у лисы, которое передается в файл Main для дальнейшего вывода на экран
    public GameObject target = null;//игровой объект для поедания лисой
    public GameObject fox;//игровой объект лиса
    public static GameObject Fox;//игровой объект лиса для передачи объекта в главное меню
    public int random2;//обход камня
    public int random;//мозг лисы
    public int maxChild = 3;//сколько лис можно родить
    private int counterRabbit = 1; //сколько нужно съесть кроликов для размножения
    public int age = 90;//биологический возраст максимальный
    public static float counter4 = 1;//колисчество созданных лис, для вывода на экран в Main

    void Start()
    {
        Health = StartHealth;
        Main.FoxSum++;
        //int counterRabbit = 2;
        maxChild = 3;
        age = 110;
        target = null;
        //GameObject[] rabbits = GameObject.FindGameObjectsWithTag("rabbit");
        //target = rabbits[Random.Range(0, rabbits.Length)];
        y = 0f;
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
        transform.Rotate(x, y, z);
        if (transform.position.y > 0.01)
        {
            //Main.FoxSum--;
            Destroy(gameObject);
        }
    }

    private void brain()
    {
        if (tag != "Spawn") age--;
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
                if (tag != "Spawn") GetComponent<Animator>().SetBool("Run", false);
                break;
            case 1:
            case 2:
            case 3:
            case 4:
                if (tag != "Spawn") GetComponent<Animator>().SetBool("Run", true);
                y = 0;
                break;
            case 5:
            case 6:
            case 7:
                if (tag != "Spawn") GetComponent<Animator>().SetBool("Run", true);
                y = 2;
                break;
            case 8:
            case 9:
            case 10:
                if (tag != "Spawn") GetComponent<Animator>().SetBool("Run", true);
                y = -2;
                break;
            case 11:
                y = 0;
                if (tag != "Spawn") GetComponent<Animator>().SetBool("Run", true);
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
        if (tag != "Spawn") Health--;
        if (Health <= 0)
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
            Health += 10;
            target = null;
            Destroy(other.gameObject);
            if (Main.start == true) eatRabbit++;
            counterRabbit--;
            //Main.Sumrabbit--;

            if (counterRabbit <= 0)
            { //родим лису если съедено нужное количество кроликов
                counterRabbit = 2;
                //Main.FoxSum++;
                Health = StartHealth;
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