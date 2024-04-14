using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPSanim : MonoBehaviour
{
    [SerializeField] private int delay = 0;
    [SerializeField] private Animator animator;
    [SerializeField] private float distanceToTeleport = 10f; // ���������� ��� ������������
    private Vector3 initialPosition; // �������� ������� �������

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        Invoke("StartAnim", delay);
    }

    void StartAnim()
    {
        animator.Play("walking nps");
        StartCoroutine(MoveObject());
    }

    IEnumerator MoveObject()
    {
        while (true)
        {
            float distanceMoved = 0f;

            while (transform.localPosition.x < 993)
            {
                transform.position += Vector3.right /** Time.deltaTime*/; // ������� ������ �������

                /*distanceMoved += Time.deltaTime;*/

                yield return null;
            }

            // ������������� ������ �� �������� �������
            transform.position = initialPosition;
        }
    }
}

