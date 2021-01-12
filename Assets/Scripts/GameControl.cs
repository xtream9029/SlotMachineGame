using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameControl : MonoBehaviour
{
    public static event Action HandlePulled = delegate { };

    [SerializeField]//private 변수를 인스펙터에서 접근 가능하게 해주는 기능
    private Text prizeText;//점수표시

    [SerializeField]
    private Row[] rows;//슬롯 열형 배열

    [SerializeField]
    private Transform handle;//손잡이의 위치

    private int prizeValue;//실제 점수값

    private bool resultsChecked = false;

    void Update()
    {
        //슬롯의 릴이 돌고 점수가 나오는 부분을 컨트롤 하는 부분

        //슬롯의 모든릴이 멈춰져있을때
        if(!rows[0].rowStopped || !rows[1].rowStopped || !rows[2].rowStopped)
        {
            prizeValue = 0;
            prizeText.enabled = false;
            resultsChecked = false;
        }

        //슬롯의 모든릴이 돌고 있고 결과 체크값이 참일때
        if (rows[0].rowStopped && rows[1].rowStopped && rows[2].rowStopped && !resultsChecked)
        {
            CheckResults();
            prizeText.enabled = true;
            prizeText.text = "Prize:" + prizeValue;
        }
    }

    private void OnMouseDown()
    {
        if (rows[0].rowStopped && rows[1].rowStopped && rows[2].rowStopped)
        {
            StartCoroutine("PullHandle");//핸들을 클릭했을때 손잡이가 움직이도록 
        }
    }

    //실제로 손잡이가 회전하는 부분을 구현한 부분
    private IEnumerator PullHandle()
    {
        for (int i = 0; i < 15; i++)
        {
            handle.Rotate(0f, 0f, i);
            yield return new WaitForSeconds(0.1f);
        }

        HandlePulled();

        for(int i = 0; i < 15; i += 5)
        {
            handle.Rotate(0f, 0f, -i);
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void CheckResults()
    {//슬롯의 점수를 결정짓는 부분

        if (rows[0].stoppedSlot == "Diamond" && rows[1].stoppedSlot == "Diamond" || rows[0].stoppedSlot == "Diamond" && rows[2].stoppedSlot == "Diamond" 
            || rows[1].stoppedSlot == "Diamond" && rows[2].stoppedSlot == "Diamond")
            prizeValue = 100;

        else if (rows[0].stoppedSlot == "Diamond" && rows[1].stoppedSlot == "Diamond" && rows[2].stoppedSlot == "Diamond")
            prizeValue = 200;

        else if (rows[0].stoppedSlot == "Crown" && rows[1].stoppedSlot == "Crown" || rows[0].stoppedSlot == "Crown" && rows[2].stoppedSlot == "Crown"
            || rows[1].stoppedSlot == "Crown" && rows[2].stoppedSlot == "Crown")
            prizeValue = 300;

        else if (rows[0].stoppedSlot == "Crown" && rows[1].stoppedSlot == "Crown" && rows[2].stoppedSlot == "Diamond")
            prizeValue = 400;

        else if (rows[0].stoppedSlot == "Watermelon" && rows[1].stoppedSlot == "Watermelon" || rows[0].stoppedSlot == "Watermelon" && rows[2].stoppedSlot == "Watermelon"
            || rows[1].stoppedSlot == "Watermelon" && rows[2].stoppedSlot == "Watermelon")
            prizeValue = 500;

        else if (rows[0].stoppedSlot == "Watermelon" && rows[1].stoppedSlot == "Watermelon" && rows[2].stoppedSlot == "Watermelon")
            prizeValue = 600;

        else if (rows[0].stoppedSlot == "Bar" && rows[1].stoppedSlot == "Bar" || rows[0].stoppedSlot == "Bar" && rows[2].stoppedSlot == "Bar"
           || rows[1].stoppedSlot == "Bar" && rows[2].stoppedSlot == "Bar")
            prizeValue = 700;

        else if (rows[0].stoppedSlot == "Bar" && rows[1].stoppedSlot == "Bar" && rows[2].stoppedSlot == "Bar")
            prizeValue = 800;

        else if (rows[0].stoppedSlot == "7" && rows[1].stoppedSlot == "7" || rows[0].stoppedSlot == "7" && rows[2].stoppedSlot == "7"
           || rows[1].stoppedSlot == "7" && rows[2].stoppedSlot == "7")
            prizeValue = 1000;

        else if (rows[0].stoppedSlot == "7" && rows[1].stoppedSlot == "7" && rows[2].stoppedSlot == "7")
            prizeValue = 1500;

        else if (rows[0].stoppedSlot == "Cherry" && rows[1].stoppedSlot == "Cherry" || rows[0].stoppedSlot == "Cherry" && rows[2].stoppedSlot == "Cherry"
        || rows[1].stoppedSlot == "Cherry" && rows[2].stoppedSlot == "Cherry")
            prizeValue = 2000;

        else if (rows[0].stoppedSlot == "Cherry" && rows[1].stoppedSlot == "Cherry" && rows[2].stoppedSlot == "Cherry")
            prizeValue = 3000;

        else if (rows[0].stoppedSlot == "Mango" && rows[1].stoppedSlot == "Mango" || rows[0].stoppedSlot == "Mango" && rows[2].stoppedSlot == "Mango"
       || rows[1].stoppedSlot == "Mango" && rows[2].stoppedSlot == "Mango")
            prizeValue = 4000;

        else if (rows[0].stoppedSlot == "Mango" && rows[1].stoppedSlot == "Mango" && rows[2].stoppedSlot == "Mango")
            prizeValue = 5000;

        resultsChecked = true;//결과 체크값을 참으로 바꿔줌
    }

}
