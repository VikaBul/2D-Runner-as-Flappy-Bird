using UnityEngine;

public class Player : MonoBehaviour
{
    private SpriteRenderer spriteRenderer; //using switch between sprites instead of animation
    public Sprite[] sprites; //массив спрайтов, между которыми буду переключаться
    private int spriteIndex; //переменная для отслеживания текущего индекса в массиве

    public float strength = 5f;
    public float gravity = -9.81f;
    public float tilt = 5f;

    private Vector3 direction;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        InvokeRepeating(nameof(AnimateSprite), 0.15f, 0.15f); //функция вызывается каждые 0,15 секунды
    }

    private void OnEnable() //reset player position by y
    {
        Vector3 position = transform.position;
        position.y = 0f;
        transform.position = position;
        direction = Vector3.zero;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            direction = Vector3.up * strength;
        }
        if (Input.touchCount > 0) //если коснуться пальцем
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began) //начало касания к экрану
            {
                direction = Vector3.up * strength;
            }
        }
        direction.y += gravity * Time.deltaTime; //гравитация вдоль оси у (direction - направление)
        transform.position += direction * Time.deltaTime; // 2 раза на Time.deltaTime ради гравитации

        Vector3 rotation = transform.eulerAngles;
        rotation.z = direction.y * tilt;
        transform.eulerAngles = rotation;
    }

    private void AnimateSprite()
    {
        spriteIndex++; 

        if (spriteIndex >= sprites.Length) //если индекс массива доходит до конца (>= длинне массива), то начинаю с начала
        {
            spriteIndex = 0;
        }

        if (spriteIndex < sprites.Length && spriteIndex >= 0) 
        {
            spriteRenderer.sprite = sprites[spriteIndex]; //новый спрайт должен появится на месте предыдущего в spriteRenderer, на получение которого ссылка в Awake
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            FindObjectOfType<GameManager>().GameOver();
        }
        else if (other.gameObject.CompareTag("Scoring"))
        {
            FindObjectOfType<GameManager>().IncreaseScore();
        }
    }

}