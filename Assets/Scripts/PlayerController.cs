using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class PlayerController : MonoBehaviour
{
    #region Inspector   
    public AnimationReferenceAsset dead;
    public AnimationReferenceAsset fight;
    public AnimationReferenceAsset idle;
    public AnimationReferenceAsset idleSad;
    public AnimationReferenceAsset jump;
    public AnimationReferenceAsset ride;
    public AnimationReferenceAsset run;
    #endregion          //виды анимации
    public GameObject _camera;
    public GameObject _player;
    public GameObject _bullet;
    public Transform _shootPos;
    Rigidbody2D _rb;
    SkeletonAnimation _skeletonAnimation;

    public static Vector3 playerPosition;
    public  bool isAlive = true;

    public AudioSource audioRun,audioJump,audioShoot,audioLoad,audioCoin;

    public int heal;
    public int gold;

    public float speed;         
    public float jumpForce,animJump;     

    public bool m_ToggleChange, m_Play;

    public bool canRun = true;
    public bool isRight = true; 
    public bool isGrounded;     //проверка земли
    
    public string currentState, currentAnimation,previousState;     

    void Start()
    {
        m_Play = false;
        heal = 3;
        isAlive = true;
        _rb = GetComponent<Rigidbody2D>();
        _skeletonAnimation = GetComponent<SkeletonAnimation>();
        currentState = "Idle";
        SetCharterState(currentState);
    }
    void Update()
    {
        if (heal <= 0) heal = 0;
        Death();
        Shoot();
        AudioPlay();
    }
        void FixedUpdate()
        {
            Move();
            Jump();
        }
    
    void Move()     //движение
    {
        float move = Input.GetAxis("Horizontal");       
       
        if (move != 0 && isAlive==true && canRun==true)
        {
            
            _rb.velocity = new Vector2(move * speed * Time.fixedDeltaTime, _rb.velocity.y);
            if (!currentState.Equals("Jump")) SetCharterState("Run");
            if (move > 0 && isRight)
            {
                isRight = false;
                Flip();
            }
            else if (move < 0 && !isRight)
            {
                isRight = true;
                Flip();
            }

        }
        else 
        {
            
            if (!currentState.Equals("Jump") && !currentState.Equals("Fight")) SetCharterState("Idle");
        }
        

    }

    void Shoot()
    {
        if (Input.GetMouseButtonDown (0))
        {
            SetCharterState("Fight");
            if (!audioJump.isPlaying) audioShoot.Play(); 
        }        
    }
    void Jump() 
    {

        if (Input.GetAxis("Jump") > 0)
        {
            if (isGrounded && isAlive)
            {
                //_rb.AddForce(Vector2.up * jumpForce);
                _rb.velocity = new Vector2(_rb.velocity.x, jumpForce*Time.fixedDeltaTime); 
                if (!currentState.Equals("Jump")) previousState = currentState;
                SetCharterState("Jump");
                if (!audioJump.isPlaying) audioJump.Play();
            }
            
        }
        
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        IsGroundedUpate(collision, true);
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        IsGroundedUpate(collision, false);
    }
    void IsGroundedUpate(Collision2D collision, bool value)
    {
        if (collision.gameObject.tag == ("Ground"))
        {
            isGrounded = value;
        }
    }       //проверка на земле ли 
    void Flip()
    {
        if (isRight == true) 
        {
            transform.Rotate(0f, 180f, 0f);
        } 
        else if (isRight == false)
        {
            transform.Rotate(0f,180f,0f);            
        }
    }   
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == ("Checkpoint"))
        {
            Save();
            collision.gameObject.GetComponent<Chekpoint>().isGo = true;

        }
        else if (collision.gameObject.tag == ("Gold")) audioCoin.Play();
    }
    void Save()         
    {
        PlayerPrefs.SetFloat("PlayerX", _player.transform.position.x);
        PlayerPrefs.SetFloat("PlayerY", _player.transform.position.y+2f);
        PlayerPrefs.SetFloat("PlayerZ", _player.transform.position.z);
        PlayerPrefs.SetInt("Heal", heal);
        PlayerPrefs.SetInt("Gold", gold);
        

       // Debug.Log("SavePlay");
    }
    void Load()         
    {
        if (!_player)
        {
            _player = gameObject;
        }

        _player.transform.position = new Vector3(PlayerPrefs.GetFloat("PlayerX"), PlayerPrefs.GetFloat("PlayerY"));
        gold = PlayerPrefs.GetInt("Gold");
        heal = PlayerPrefs.GetInt("Heal");
        isAlive = true;
        audioLoad.Play();
       // Debug.Log("Load");
        
    }
    public void Death()        
    {
        if (heal <= 0)
        {
            isAlive = false;
            SetCharterState("Dead");
            Load();

        }
        else if (gameObject.transform.position.y <= -23f)
        {
            
            Load();
        }
    }
    public void SetAnimation(AnimationReferenceAsset animation, bool loop, float timeScale) //название анимации, повтор, скорость анимации
    {
        if (animation.name.Equals(currentAnimation)) return;        //возврат, если та же анимация играет
        Spine.TrackEntry animationEntry = _skeletonAnimation.AnimationState.SetAnimation(0, animation, loop);
        animationEntry.TimeScale = timeScale;
        animationEntry.Complete += AnimationEntry_Complete;
        
        currentAnimation = animation.name;
    }

  

    void AnimationEntry_Complete(Spine.TrackEntry trackEntry)//после заверщения анимации прыжка выбирается предыдущая анимация
    {
        if (currentState.Equals("Jump")) SetCharterState(previousState);
        if (currentState.Equals("Fight"))
        {
            Instantiate(_bullet, _shootPos.position, _shootPos.rotation);
            SetCharterState("Idle");        
        }
        if (currentState.Equals("Dead")) Load();

    }

    void SetCharterState(string state)		//анимации
    {
        if (isAlive == true)
        {
            if (state.Equals("Run")) SetAnimation(run, true, 1f);
            
            else if (state.Equals("IdleSad")) SetAnimation(idleSad, true, 1f);
            else if (state.Equals("Jump")) SetAnimation(jump, false, animJump);
            else if (state.Equals("Ride")) SetAnimation(ride, false, 1f);
            else if (state.Equals("Fight")) SetAnimation(fight, false, 1.6f);
            else SetAnimation(idle, true, 1f);
            
        }
        else SetAnimation(dead, false, 1f);
        currentState = state;
    }
    public void TakeDamagePlayer(int damage)             
    {
        heal -= damage;
    }
    void AudioPlay()
    {
        //if (!audioRun.isPlaying)
        //{
        //    audioRun.Play();
        //}
        if (currentState == "Run") m_ToggleChange = true;
        else m_ToggleChange = false;
        if (m_ToggleChange == true)
        {
            if (!audioRun.isPlaying) audioRun.Play();
        }
        else audioRun.Stop();

    }
}
