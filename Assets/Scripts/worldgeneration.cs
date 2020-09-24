using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;



public class worldgeneration : MonoBehaviour
{


    public TextMeshProUGUI _cansou;

    private bool _end = false;

    public float randomChangeHeight = 0.02f;
    public float randomNoGround = 0.8f;

    public float randomReturnGround = 0.01f;


    public float randomCoffeeChance = 0.2f;


    private float nextActionTime = 0.0f;
    public float period = 0.1f;



    public float startCameraX = -2.87f;
    public float viewCameraSize = 2.87f * 2;
    private int cameraIndex = 1;
    private float relativeCurrentCamera = 0.0f;

    public float lowestBlockHeight = -1.45f;
    public float blockSize = 0.32f;

    private int currentType = 2;

    private int currentHeight = 0;
    private int currentHeightDirection = 1;

    private float blockX = 0.0f;
    private float blockY = 0.0f;


    public int blockDifficulty = 4;

    private int currentWithOutGround = 0;

    private GameObject player;


    private List<GameObject> grounds = new List<GameObject>();

    public Sprite terrainDownRight;
    public Sprite terrainDownLeft;
    public Sprite terrainDownCenter;
    public Sprite terrainRight;
    public Sprite terrainLeft;
    public Sprite terrainCenter;
    public GameObject terrainNoAtAll;
    public GameObject terrain;

    public GameObject coffeeObj;

    private bool initCoffee = false;


    public GameObject soloBlock;
    public GameObject soloCoin;
    // Start is called before the first frame update
    void Start()
    {
        nextActionTime = 0.0f;
        cameraIndex = 0;
        relativeCurrentCamera = startCameraX;
        startCameraX += viewCameraSize;
        generateTerrain();
        player = GameObject.FindGameObjectWithTag("Player");
        //player.GetComponent<PlayerController>().RefreshSpeed();
    }

    void generateTerrain()
    {   
        foreach (int indexBlock in Enumerable.Range(0, 18))
        {
            
            if (grounds.Count == indexBlock)
            {
                GameObject coffeeBlock = Instantiate(coffeeObj);
                coffeeBlock.transform.position = new Vector3(blockX, blockY + blockSize);
                grounds.Add(Instantiate(terrain));
                grounds[indexBlock].transform.position = new Vector3(blockX, blockY);
            }

            blockX = relativeCurrentCamera + (blockSize * indexBlock) + blockSize/2;
            
            if (currentHeight % 3 == 0)
            {
                blockY = lowestBlockHeight + currentHeight * blockSize * 1;
            }


            {
                switch (currentType)
                {
                    case 0:
                        grounds[indexBlock].GetComponent<SpriteRenderer>().sprite = terrainCenter;
                        grounds[indexBlock].transform.position = new Vector3(blockX, blockY);
                        currentType = 1;
                        if (currentHeight > 0)
                        {
                            GameObject groundBlock = Instantiate(terrainNoAtAll);
                            groundBlock.GetComponent<SpriteRenderer>().sprite = terrainDownCenter;
                            groundBlock.transform.position = new Vector3(blockX, blockY - blockSize);
                        }
                        break;
                    case 1:
                        
                        grounds[indexBlock].GetComponent<SpriteRenderer>().sprite = terrainCenter;
                        grounds[indexBlock].transform.position = new Vector3(blockX, blockY);

                        if (Random.Range(0.0f, 1.0f) < randomCoffeeChance)
                        {
                            GameObject coffeeBlock = Instantiate(coffeeObj);
                            coffeeBlock.transform.position = new Vector3(blockX, blockY + blockSize);
                        }
                        if (Random.Range(0.0f, 1.0f) <= randomChangeHeight && indexBlock < 15 && indexBlock > 3)
                        {
                            currentHeight += currentHeightDirection;
                            if (currentHeight % 3 == 0)
                            {
                                if (currentHeight % 2 == 0)
                                {

                                    currentHeightDirection *= -1;
                                    if (currentHeight == 0)
                                    {
                                        GameObject point = Instantiate(soloBlock);
                                        point.transform.position = new Vector3(blockX + blockDifficulty * blockSize, lowestBlockHeight + 6 * blockSize * 1);
                                        GameObject coin = Instantiate(soloCoin);
                                        coin.transform.position = new Vector3(blockX + blockDifficulty * blockSize, lowestBlockHeight + 7 * blockSize * 1);
                                    } else if (currentHeight == 6)
                                    {
                                        GameObject point = Instantiate(soloBlock);
                                        point.transform.position = new Vector3(blockX, lowestBlockHeight + 0 * blockSize * 1);
                                        GameObject coin = Instantiate(soloCoin);
                                        coin.transform.position = new Vector3(blockX, lowestBlockHeight + 1 * blockSize * 1);

                                    }
                                }
                                currentType = 2;
                                grounds[indexBlock].GetComponent<SpriteRenderer>().sprite = terrainRight;
                                grounds[indexBlock].transform.position = new Vector3(blockX, blockY);

                            }


                        }
                        if (Random.Range(0.0f, 1.0f) <= randomNoGround)
                        {
                            if (currentHeight > 0)
                            {
                                GameObject groundBlock = Instantiate(terrainNoAtAll);
                                groundBlock.GetComponent<SpriteRenderer>().sprite = terrainDownRight;
                                groundBlock.transform.position = new Vector3(blockX, blockY - blockSize);
                            }
                            currentType = 3;
                            grounds[indexBlock].GetComponent<SpriteRenderer>().sprite = terrainRight;
                            grounds[indexBlock].transform.position = new Vector3(blockX, blockY);
                        } else if (currentHeight > 0)
                        {
                            GameObject groundBlock = Instantiate(terrainNoAtAll);
                            groundBlock.GetComponent<SpriteRenderer>().sprite = terrainDownCenter;
                            groundBlock.transform.position = new Vector3(blockX, blockY - blockSize);
                        }

                        break;
                    case 2:
                        grounds[indexBlock].GetComponent<SpriteRenderer>().sprite = terrainLeft;
                        grounds[indexBlock].transform.position = new Vector3(blockX, blockY);
                        if (currentHeight > 0)
                        {
                            GameObject groundBlock = Instantiate(terrainNoAtAll);
                            groundBlock.GetComponent<SpriteRenderer>().sprite = terrainDownLeft;
                            groundBlock.transform.position = new Vector3(blockX, blockY - blockSize);
                        }
                        currentType = 0;
                        
                        break;

                    case 3:
                        currentWithOutGround += 1;
                        if (Random.Range(0.0f, 1.0f) <= randomReturnGround * (currentWithOutGround / 2) || !(indexBlock < 16 && indexBlock > 0))
                        {
                            currentWithOutGround = 0;
                            currentType = 2;
                        }

                        break;

                    default:
                        break;
                }
            }
            


        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextActionTime)
        {
            if (_end) {
                Destroy(player);
                SceneManager.LoadScene("Menu", LoadSceneMode.Single);
            }
            nextActionTime += period;
            player.GetComponent<PlayerController>().walkSpeed *= 0.95f;
            if (player.GetComponent<PlayerController>().walkSpeed < 0.2)
            {
                //Destroy(player);
                player.GetComponent<PlayerController>().walkSpeed = 0.0f;
                _cansou.SetText("Voce ficou sem tomar cafe e cansou ;(");
                _end = true;
            }
        }

        if (player.transform.position.x > startCameraX)
        {
            startCameraX += viewCameraSize;
            relativeCurrentCamera += viewCameraSize;
            cameraIndex += 1;
            if (cameraIndex > PlayerPrefs.GetInt("highScore"))
            {
                PlayerPrefs.SetInt("highScore", cameraIndex);
            }


            
            gameObject.transform.position += new Vector3(viewCameraSize, 0.0f);
            generateTerrain();
        }

        if (player.transform.position.y < lowestBlockHeight - blockSize)
        {
            _end = true;
            //Destroy(player);
            _cansou.SetText("Voce ficou sem tomar cafe e caiu de cansado ;(");
        }

    }
}
