using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SojaExiles
{
    public class Drawer_Pull_Z : MonoBehaviour
    {
        public Animator pull;
        public bool open;
        public Transform Player;

        void Start()
        {
            open = false;

            // Player가 비어 있으면 Player 태그로 자동 찾기
            if (Player == null)
            {
                GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

                if (playerObject != null)
                {
                    Player = playerObject.transform;
                }
                else
                {
                    Debug.LogWarning("Player 태그가 붙은 오브젝트를 찾지 못했습니다.");
                }
            }
        }

        void OnMouseOver()
        {
            if (Player != null)
            {
                float dist = Vector3.Distance(Player.position, transform.position);

                if (dist < 10f && Input.GetMouseButtonDown(0))
                {
                    if (!open)
                    {
                        StartCoroutine(opening());
                    }
                    else
                    {
                        StartCoroutine(closing());
                    }
                }
            }
        }

        IEnumerator opening()
        {
            print("you are opening the drawer");
            pull.Play("openpull");
            open = true;

            yield return new WaitForSeconds(.5f);
        }

        IEnumerator closing()
        {
            print("you are closing the drawer");
            pull.Play("closepush");
            open = false;

            yield return new WaitForSeconds(.5f);
        }
    }
}