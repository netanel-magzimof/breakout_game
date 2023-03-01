using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AudioSource))]
public class Bat : MonoBehaviour
{
    private AudioSource audioClip;
    public SpriteRenderer stunSprite;
    private float stunTimeLeft;
    [SerializeField]
    private float DEAFULT_STUN_TIME;
    private float speed = 10;
    private Rigidbody2D physics;
    private bool rightClicked, leftClicked,isMoveable;
    
    private void Awake()
    {
        physics = GetComponent<Rigidbody2D>();
        audioClip = GetComponent<AudioSource>();
    }

    private void Start()
    {
        isMoveable = false;
    }

    private void Update()
    {
        if (stunTimeLeft > 0)
        {
            stunTimeLeft -= Time.deltaTime;
            rightClicked = false;
            leftClicked = false;
            return;
        }
        stunSprite.enabled = false;
        if (isMoveable)
        {
            rightClicked = Input.GetKey(KeyCode.RightArrow);
            leftClicked = Input.GetKey(KeyCode.LeftArrow);
        }
        else
        {
            rightClicked = false;
            leftClicked = false;
        }
    }

    public void ActivateBat(bool command)
    {
        rightClicked = false;
        leftClicked = false;
        isMoveable = command;
    }
    
    private void FixedUpdate()
    {
        Vector2 newVel = Vector2.zero;
        if (rightClicked)
        {
            newVel += Vector2.right*speed;
        }
        if (leftClicked)
        {
            newVel += Vector2.left*speed;
        }
        physics.velocity = newVel;
    }
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("LVL 3"))
        {
            stunTimeLeft = DEAFULT_STUN_TIME;
            stunSprite.enabled = true;
            audioClip.Play();
        }
    }
}