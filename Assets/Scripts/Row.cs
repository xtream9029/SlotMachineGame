using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Row : MonoBehaviour
{
    private int randomValue;
    private float timeInterval;

    public bool rowStopped;
    public string stoppedSlot;//결정된 슬롯

    // Start is called before the first frame update
    void Start()
    {
        rowStopped = true;
        GameControl.HandlePulled += StartRotating;
    }

    private void StartRotating()
    {
        stoppedSlot = "";
        StartCoroutine("Rotate");
    }

    private IEnumerator Rotate()//코루틴
    {
        rowStopped = false;//슬롯머신의 릴이 돌아야 하므로
        timeInterval = 0.025f;

        //릴 돌리는 부분을 구현한것
        //각릴에서 첫번째 심볼과 마지막 심볼이 같아야 봣을때 도는것처럼 보여 질수 있음
        for(int i = 0; i < 30; i++)
        {
            if (transform.position.y <= -1.75f)
            {
                transform.position = new Vector2(transform.position.x, 3.5f);
            }

            else transform.position = new Vector2(transform.position.x, transform.position.y  -0.025f);

            yield return new WaitForSeconds(timeInterval);
        }

        randomValue = Random.Range(60, 100);

        switch(randomValue % 3)
        {
            case 1:
                randomValue += 2;
                break;
            case 2:
                randomValue += 1;
                break;
        }

        for(int i = 0; i < randomValue; i++)
        {
            if (transform.position.y <= -1.75f)
                transform.position = new Vector2(transform.position.x, 3.5f);

            else transform.position = new Vector2(transform.position.x, transform.position.y - 0.25f);
            
            //실제로 릴이 회전하는 속도를 결정짓는 부분
            if (i > Mathf.RoundToInt(randomValue * 0.25f))
                timeInterval = 0.05f;
            if (i > Mathf.RoundToInt(randomValue * 0.25f))
                timeInterval = 0.1f;
            if (i > Mathf.RoundToInt(randomValue * 0.25f))
                timeInterval = 0.15f;
            if (i > Mathf.RoundToInt(randomValue * 0.25f))
                timeInterval = 0.2f;

            yield return new WaitForSeconds(timeInterval);
        }

        //y좌표에 따라 슬롯을 결정하는 부분
        if (transform.position.y == -1.75f)
        {
            stoppedSlot = "Diamond";
        }
        else if (transform.position.y == -1.0f)
        {
            stoppedSlot = "Crown";
        }
        else if (transform.position.y == -0.25f)
        {
            stoppedSlot = "Watermelon";
        }
        else if (transform.position.y == 0.5f)
        {
            stoppedSlot = "Bar";
        }
        else if (transform.position.y == 1.25f)
        {
            stoppedSlot = "7";
        }
        else if (transform.position.y == 2.0f)
        {
            stoppedSlot = "Cherry";
        }
        else if (transform.position.y == 2.75f)
        {
            stoppedSlot = "Mango";
        }
        else if (transform.position.y == 3.5f)
        {
            stoppedSlot = "Diamond";
        }

        rowStopped = true;

    }//Rotate

    private void OnDestroy()
    {
        GameControl.HandlePulled -= StartRotating;
    }//OnDestroy
}
