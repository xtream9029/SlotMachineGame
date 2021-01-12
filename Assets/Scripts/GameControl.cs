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

        //점수를 결정하는 로직을 좀 더 단순하게 해볼것
        ScoreDecision();
        resultsChecked = true;//결과 체크값을 참으로 바꿔줌
    }

    private void ScoreDecision()
    {
        int[] A=new int[8];
        int cnt = 0;    

        for(int i = 1; i < A.Length; i++)
        {
            A[i] = 0;
        }

        for(int i = 0; i < rows.Length; i++)
        {
            int k = rows[i].stoppedSlot;
            A[k]++;
        }

        int MaxA = -1;
        for(int i = 1; i < A.Length; i++)
        {
            if (MaxA < A[i])
            {
                MaxA = i;
                cnt = A[i];
            }
        }

        if (MaxA == 1)
        {
            if (cnt >=2)
            {
                if (cnt == 3) prizeValue = 200;
                else prizeValue = 100;
            }
        }
        else if (MaxA == 2)
        {
            if (cnt >= 2)
            {
                if (cnt == 3) prizeValue = 400;
                else prizeValue = 300;
            }
        }
        else if (MaxA == 3)
        {
            if (cnt >= 2)
            {
                if (cnt == 3) prizeValue = 600;
                else prizeValue = 500;
            }
        }
        else if (MaxA == 4)
        {
            if (cnt >= 2)
            {
                if (cnt == 3) prizeValue = 800;
                else prizeValue = 700;
            }
        }
        else if (MaxA == 5)
        {
            if (cnt >= 2)
            {
                if (cnt == 3) prizeValue = 1500;
                else prizeValue = 1000;
            }
        }
        else if (MaxA == 6)
        {
            if (cnt >= 2)
            {
                if (cnt == 3) prizeValue = 3000;
                else prizeValue = 2000;
            }
        }
        else if (MaxA == 7)
        {
            if (cnt >= 2)
            {
                if (cnt == 3) prizeValue = 5000;
                else prizeValue = 4000;
            }
        }
    }

}
