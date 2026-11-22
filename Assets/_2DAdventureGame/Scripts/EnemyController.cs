using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;
    public bool vertical;
    public float changeTime = 3.0f; // Скільки секунд іти в один бік
    Animator animator;
    bool broken = true;

    Rigidbody2D rigidbody2d;
    float timer;
    int direction = 1;
    public bool isBroken { get { return broken; } }

    // ЗВУКОВІ ЗМІННІ
    [Header("Enemy Audio")]
    public AudioClip fixSound;    // Звук, коли ворога лагодять
    public AudioClip stepSound;   // Звук кроків ворога
    private AudioSource audioSource;
    private float stepTimer;
    public float stepInterval = 0.5f; // Інтервал між кроками ворога

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        timer = changeTime;
        animator = GetComponent<Animator>();
        
        // Отримуємо компонент AudioSource з об'єкта ворога
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }
    }

    void FixedUpdate()
    {
        if (!broken)
        {
            return;
        }

        // Таймер для кроків ворога, поки він рухається
        stepTimer -= Time.fixedDeltaTime;
        if (stepTimer <= 0f)
        {
            if (stepSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(stepSound);
            }
            stepTimer = stepInterval;
        }

        Vector2 position = rigidbody2d.position;

        if (vertical)
        {
            position.y = position.y + speed * direction * Time.deltaTime;
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", direction);
        }
        else
        {
            position.x = position.x + speed * direction * Time.deltaTime;
            animator.SetFloat("Move X", direction);
            animator.SetFloat("Move Y", 0);
        }

        rigidbody2d.MovePosition(position);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            player.ChangeHealth(-1);
        }
    }

    public void Fix()
    {
        broken = false;
        rigidbody2d.simulated = false;
        animator.SetTrigger("Fixed");

        // Відтворюємо звук лагодження, коли ворога вилікували
        if (fixSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(fixSound);
        }
    }
}

