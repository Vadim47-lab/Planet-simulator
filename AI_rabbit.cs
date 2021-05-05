using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class AI_rabbit : MonoBehaviour
{
    private float z = 0;
    private float x = 0;
    private float y = 0;
    private int counter = 0;
    private int xGrass, yGrass;
    public int counter2 = 0;
    public int sumGrass = 0;
    public int Health;
    public int randomT;
    public GameObject rabbit;
    public GameObject game;

    void Start()
    {
        xGrass = Random.Range(0, 99);
        yGrass = Random.Range(0, 99);
        Health = 40;
        InvokeRepeating("brain", 0, 1f);
        InvokeRepeating("life", 1f, 1f);
    }

    void Update()
    {
        if (transform.position.y > 0.01) Destroy(gameObject);
        int xRabbit, yRabbit;
        transform.Rotate(x, y, z);
        xRabbit = Mathf.RoundToInt(transform.position.x * 2f)-1;
        yRabbit = Mathf.RoundToInt(transform.position.z * 2f)-1;
        GameObject grass = null;
        if (xRabbit>=0 && xRabbit<100 && yRabbit >= 0 && yRabbit < 100)
        {
            grass = game.GetComponent<Main>().grass[xRabbit, yRabbit];
        }
        
        if (grass != null && Health <= 20)
        {
            Destroy(grass);
            //sumGrass--;
            counter++;
            if (counter == 4)
            {
                Health = 40;
                create();
                counter = 0;
                counter2++;
            }
            game.GetComponent<Main>().grass[xRabbit, yRabbit] = null;
        }
    }

    private void brain()
    {
        //Получить случайное число (в диапазоне от 0 до 1)
        randomT = Random.Range(0, 10);
        if (Health <= 20) randomT = 11;
        switch (randomT)
        {
           case 0:
               GetComponent<Animator>().SetBool("Run", false);
               break;
           case 1:
           case 2:
           case 3:
           case 4:
               GetComponent<Animator>().SetBool("Run", true);
               y = 0;
               break;
           case 9:
           case 5:
           case 6:
               GetComponent<Animator>().SetBool("Run", true);
               y = 2;
               break;
           case 7:
           case 8:
           case 10:
               GetComponent<Animator>().SetBool("Run", true);
               y = -2;
               break;
           case 11:
                y = 0;
                GameObject grass = null;
                if (game.GetComponent<Main>().grass[xGrass, yGrass] == null)
                {
                    grass = findGrass();
                }
                else
                {
                    grass = game.GetComponent<Main>().grass[xGrass, yGrass];
                }
                if (grass == null)
                {
                    GetComponent<Animator>().SetBool("Run", false);
                    break;
                }
                transform.LookAt(grass.transform);
                GetComponent<Animator>().SetBool("Run", true);
                break;
        }
    }

    private void create()
    {
        Instantiate(rabbit, transform.position, transform.rotation);
    }

    private void life()
    {
        if (tag == "rabbit") Health--;
        if (Health == 0 && tag == "rabbit")
        { 
            //GetComponent<Animator>().SetBool("Death", true);
            Destroy(gameObject, 2f);
            counter2--;
        }
    }

    private GameObject findGrass()
    {
        int i, j;
        int count = 0;
        for (i = 0; i < 100; i++)
        {
            for (j = 0; j < 100; j++)
            {
                if (game.GetComponent<Main>().grass[i, j] != null) count++;
            }
        }
        int random = Random.Range(1, count);
        count = 0;
        for (i = 0; i < 100; i++)
        {
           for (j = 0; j < 100; j++)
           {
                GameObject grass = game.GetComponent<Main>().grass[i, j];
                if (grass != null)
                {
                    count++;
                    if (count == random)
                    {
                        xGrass = i;
                        yGrass = j;
                        return grass;
                    }
                }
           }
        }
        return null;
    }
}