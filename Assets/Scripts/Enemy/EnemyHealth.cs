using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public float sinkSpeed = 2.5f;
    public int scoreValue = 10;
    public AudioClip deathClip;
    public GameObject Loot;
    public int enemyType; // 0-slon, 1-pink, 2-green

    Animator anim;
    AudioSource enemyAudio;
    ParticleSystem hitParticles;
    CapsuleCollider capsuleCollider;
    bool isDead;
    bool isSinking;
    GameObject player;
    GameObject levelUI;
    GameObject gold;
    public QuestManager questManager;
    public LevelManager levelManager;
    //  public GoldManager goldManager;

    void Awake()
    {
        anim = GetComponent<Animator>();
        enemyAudio = GetComponent<AudioSource>();
        hitParticles = GetComponentInChildren<ParticleSystem>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        player = GameObject.FindGameObjectWithTag("Player");
        questManager = player.GetComponent<QuestManager>();
        levelUI = GameObject.FindGameObjectWithTag("Level");
        levelManager = levelUI.GetComponent<LevelManager>();
        gold = GameObject.FindGameObjectWithTag("Gold");
        //  goldManager = gold.GetComponent<GoldManager>();
        currentHealth = startingHealth;
    }


    void Update()
    {
        if (isSinking)
        {
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }


    public void TakeDamage(int amount, Vector3 hitPoint)
    {
        if (isDead)
            return;

        enemyAudio.Play();

        currentHealth -= amount;

        hitParticles.transform.position = hitPoint;
        hitParticles.Play();

        if (currentHealth <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        isDead = true;

        capsuleCollider.isTrigger = true;

        anim.SetTrigger("Dead");
        enemyAudio.clip = deathClip;
        enemyAudio.Play();
        questManager.EnemyKilled(enemyType);
        int randnum = Random.Range(0, 14);
        if (randnum == 0)
        {
            SpawnLoot();
        }
    }

    public void SpawnLoot()
    {
        Instantiate(Loot, gameObject.transform.position, Quaternion.identity);
    }

    public void StartSinking()
    {
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        isSinking = true;
        GameManager.Instance.gold += scoreValue;
        levelManager.TakeExp(scoreValue);
        Destroy(gameObject, 2f);
    }
}