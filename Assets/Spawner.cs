using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject coin;
    GameObject spawnCoin;
    private List<GameObject> coins = new List<GameObject>();
    char[,] grid;
    public Vector2[,] positionGrid;
    bool gameOver = false;
    [SerializeField] Text winMessage;
    // Start is called before the first frame update
    void Start()
    {
        float startX = -5.7f;
        float startY = -3.543154f;
        float xIncrement = 1.884355f;
        float yIncrement = 1.37f;
        float currentX = 0f;
        float currentY = 0f;

        positionGrid = new Vector2[6,7];

        for (int r = 0; r < 6; r++)
        {
            for (int c = 0; c < 7; c++)
            {

                Debug.Log("Y increment: " + yIncrement + "/n");
                Debug.Log("X increment: " + xIncrement + "/n");

                currentY = startY + (r * yIncrement);
                Debug.Log("Y: " + currentY + "/n");

                currentX = startX + (c * xIncrement);
                Debug.Log("X: " + currentX + "/n");

                positionGrid[r, c] = new Vector2(currentX, currentY);

            }
        }
        Debug.Log("IN START");

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        spawnCoin = Instantiate(coin, mousePos, Quaternion.identity);
        
        spawnCoin.transform.position = mousePos;

        int numRows = 6;
        int numColumns = 7;
        grid = new char[numRows, numColumns];
        for (int i = 0; i < numRows; i++)
        {
            for (int j = 0; j < numColumns; j++)
            {
                grid[i, j] = 'N';
            }
        }
    }

    public void resetGame()
    {
        gameOver = false;
        
        for (int x = 0; x < coins.Count; x++)
        {
            Destroy(coins[x]);
        }
        coins.Clear();
        winMessage.text = " ";

        float startX = -5.7f;
        float startY = -3.543154f;
        float xIncrement = 1.884355f;
        float yIncrement = 1.37f;
        float currentX = 0f;
        float currentY = 0f;

        positionGrid = new Vector2[6, 7];

        for (int r = 0; r < 6; r++)
        {
            for (int c = 0; c < 7; c++)
            {
                currentY = startY + (r * yIncrement);

                currentX = startX + (c * xIncrement);

                positionGrid[r, c] = new Vector2(currentX, currentY);

            }
        }


        spawnCoin.SetActive(true);

        /*Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        spawnCoin = Instantiate(coin, mousePos, Quaternion.identity);

        spawnCoin.transform.position = mousePos;*/

        int numRows = 6;
        int numColumns = 7;
        grid = new char[numRows, numColumns];
        for (int i = 0; i < numRows; i++)
        {
            for (int j = 0; j < numColumns; j++)
            {
                grid[i, j] = 'N';
            }
        }
    }

    public void ColumnClicked(int columnNumber)
    {
        if (gameOver)
        {
            return;
        }

        for (int row = 0; row < 6; row++)
        {
            if (grid[row, columnNumber] == 'N')
            {
                Vector2 specificPosition = positionGrid[row, columnNumber];

                GameObject newCoin = Instantiate(coin, specificPosition, Quaternion.identity);
                coins.Add(newCoin);

                grid[row, columnNumber] = ChangeColor();

                if (grid[row, columnNumber] == 'R')
                {
                    newCoin.GetComponent<SpriteRenderer>().color = Color.red;
                }
                else
                {
                    newCoin.GetComponent<SpriteRenderer>().color = Color.black;
                }

                newCoin.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;

                if (isWinner('R'))
                {
                    spawnCoin.SetActive(false);
                    gameOver = true;
                    winMessage.text = "Red Won!";
                }
                else if (isWinner('B'))
                {
                    winMessage.text = "Black Won!";
                    spawnCoin.SetActive(false);
                    gameOver = true;
                }
                else if (isTie())
                {
                    winMessage.text = "It's a Tie";
                    spawnCoin.SetActive(false);
                    gameOver = true;
                }
                

                break;
            }
        }

    }


    // Change color of the spawn coin
    public char ChangeColor()
    {

        if (spawnCoin.GetComponent<SpriteRenderer>().color == Color.black)
        {
            spawnCoin.GetComponent<SpriteRenderer>().color = Color.red;
            return 'B';
        }
        else
        {
            spawnCoin.GetComponent<SpriteRenderer>().color = Color.black;
            return 'R';
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver == true && Input.GetKey(KeyCode.R))
        {
            resetGame();
        }


        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (mousePos.x < -6.23f)
        {
            spawnCoin.transform.position = new Vector2(-6.317545f, 4.898858f);
        }
        else if (mousePos.x > 5.80f)
        {

            spawnCoin.transform.position = new Vector2(6.18f, 4.898858f);
        }
        else
        {
            spawnCoin.transform.position = new Vector2(mousePos.x, 4.898858f);
        }
    }

    public bool isTie()
    {
        for (int r = 0; r < 6; r++)
        {
            for (int c = 0; c < 7; c++)
            {
                if (grid[r,c] == 'N')
                {
                    return false;
                }
            }
        }
        return true;
    }

    public bool isWinner(char letter)
    {
        for (int r = 0; r < 6; r++)
        {
            for (int c = 0; c <= 3; c++)
            {
                if (grid[r,c] == letter && grid[r,c + 1] == letter && grid[r,c + 2] == letter && grid[r,c + 3] == letter)
                {
                    return true;
                }
            }
        }

        for (int r = 0; r < 3; r++)
        {
            for (int c = 0; c <= 3; c++)
            {
                if (grid[r,c] == letter && grid[r + 1,c] == letter && grid[r + 2,c] == letter && grid[r + 3,c] == letter)
                {
                    return true;
                }
            }
        }

        for (int r = 0; r < 3; r++)
        {
            for (int c = 0; c <= 3; c++)
            {
                if (grid[r, c] == letter && grid[r + 1, c + 1] == letter && grid[r + 2, c + 2] == letter && grid[r + 3, c + 3] == letter)
                {
                    return true;
                }
            }
        }

        for (int r = 0; r < 3; r++)
        {
            for (int c = 3; c <= 6; c++)
            {
                if (grid[r + 3, c - 3] == letter && grid[r + 2, c - 2] == letter && grid[r + 1, c - 1] == letter && grid[r, c] == letter)
                {
                    return true;
                }
            }
        }

        for (int r = 3; r < 6; r++)
        {
            for (int c = 3; c <= 6; c++)
            {
                if (grid[r - 3, c - 3] == letter && grid[r - 2, c - 2] == letter && grid[r - 1, c - 1] == letter && grid[r, c] == letter)
                {
                    return true;
                }
            }
        }

        return false;
    }
}
