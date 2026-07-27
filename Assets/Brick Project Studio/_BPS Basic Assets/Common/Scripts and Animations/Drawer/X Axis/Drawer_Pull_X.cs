using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SojaExiles
{
    public class Drawer_Pull_X : MonoBehaviour
    {
        public Animator pull_01;
        public bool open;
        public Transform Player;

        void Start()
        {
            open = false;

            // Player가 비어 있으면 태그로 자동 할당
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

                if (dist < 10f)
                {
                    if (Input.GetMouseButtonDown(0))
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
        }

        IEnumerator opening()
        {
            print("you are opening the drawer");
            pull_01.Play("openpull_01");
            open = true;

            yield return new WaitForSeconds(.5f);
        }

        IEnumerator closing()
        {
            print("you are closing the drawer");
            pull_01.Play("closepush_01");
            open = false;

            yield return new WaitForSeconds(.5f);
        }
    }
}