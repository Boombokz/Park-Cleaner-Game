using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10;
    [SerializeField] private Transform waste;
    [SerializeField] private Transform arms;
    [SerializeField] private Transform posOverHead;
    [SerializeField] private Transform targetPos;
    [SerializeField] private float xRange;
    [SerializeField] private float zRange;

    private bool _isWasteInHands;
    private bool _isWasteFlying;
    private float _t;

    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (!_gameManager.IsGameStarted) return;
        MovePlayer();
        if (_isWasteInHands)
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

        if (Input.GetKeyUp(KeyCode.Space) && _isWasteInHands)
        {
            _isWasteFlying = true;
            _isWasteInHands = false;
            _t = 0;
        }

        if (!_isWasteFlying) return;
        _t += Time.deltaTime;
        const float duration = 0.5f;
        float t01 = _t / duration;

        Vector3 a = posOverHead.position;
        Vector3 b = targetPos.position;
        Vector3 pos = Vector3.Lerp(a, b, t01);

        //move in arc
        Vector3 arc = Vector3.up * (5 * Mathf.Sin(t01 * 3.14f));

        if (waste != null)
        {
            waste.position = pos + arc;
        }

        if (!(t01 >= 1)) return;
        _isWasteFlying = false;
        _gameManager.AddScore();
    }

    private void MovePlayer()
    {
        float xMovement = Input.GetAxisRaw("Horizontal");
        float zMovement = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(xMovement, 0, zMovement);

        var transform1 = transform;
        var position = transform1.position;
        position += direction.normalized * (Time.deltaTime * moveSpeed);
        transform1.position = position;
        transform.LookAt(position + direction);

        ControlPlayerBounds();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_isWasteInHands && _gameManager.IsGameStarted)
        {
            _isWasteInHands = true;
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