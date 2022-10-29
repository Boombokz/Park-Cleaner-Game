using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10;
    [SerializeField] Transform waste;
    [SerializeField] Transform arms;
    [SerializeField] Transform posOverHead;
    [SerializeField] private Transform targetPos;
    [SerializeField] private float xRange;
    [SerializeField] private float zRange;

    private bool isWasteInHands = false;
    private bool isWasteFlying = false;
    private float t = 0;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGameStarted)
        {
            MovePlayer();
            if (isWasteInHands)
            {
                if (waste != null)
                {
                    waste.position = posOverHead.position;
                }
                else
                {
                    arms.localEulerAngles = Vector3.right * 0;
                }
                arms.localEulerAngles = Vector3.right * 180;
                transform.LookAt(targetPos.parent.position);
            }
            else
            {
                arms.localEulerAngles = Vector3.right * 0;
            }

            if (Input.GetKeyUp(KeyCode.Space) && isWasteInHands)
            {
                isWasteFlying = true;
                isWasteInHands = false;
                t = 0;
            }

            if (isWasteFlying)
            {
                t += Time.deltaTime;
                float duration = 0.5f;
                float t01 = t / duration;

                Vector3 a = posOverHead.position;
                Vector3 b = targetPos.position;
                Vector3 pos = Vector3.Lerp(a, b, t01);

                //move in arc
                Vector3 arc = Vector3.up * 5 * Mathf.Sin(t01 * 3.14f);

                if (waste != null)
                {
                    waste.position = pos + arc;
                }

                if (t01 >= 1)
                {
                    isWasteFlying = false;
                    gameManager.AddScore();
                }
            }
        }

    }

    void MovePlayer()
    {
        float xMovement = Input.GetAxisRaw("Horizontal");
        float zMovement = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(xMovement, 0, zMovement);

        transform.position += direction.normalized * Time.deltaTime * moveSpeed;
        transform.LookAt(transform.position + direction);

        ControlPlayerBounds();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isWasteInHands && gameManager.isGameStarted)
        {
            isWasteInHands = true;
            waste = other.gameObject.transform;
        }
    }

    private void ControlPlayerBounds()
    {
        if (transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }

        if (transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }

        if (transform.position.z < -zRange)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -zRange);
        }

        if (transform.position.z > zRange)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zRange);
        }
    }
}
