using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Camera playerCamera;
    private CharacterController2D controller;

    private Vector3 cameraOffset;

    public float runSpeed;

    private float m_horizontalMove;
    private bool m_jump;

    [HideInInspector]
    public bool IsHitting = false;

    // SOUNDS
    [SerializeField]
    private AudioSource m_walkAudio;
    [SerializeField]
    private AudioSource m_jumpAudio;
    [SerializeField]
    private AudioSource m_attackAudio;
    
    private Animator m_animator;

    private AudioSource m_audioSource;
    
    public void Start()
    {
        cameraOffset = new Vector3(0, 0, -10.0f);
        controller = GetComponent<CharacterController2D>();
    
        m_animator = GetComponent<Animator>();

        controller.jumpedAction = OnJump;
    }

    public void Update()
    {
        m_horizontalMove = Input.GetAxis("Horizontal") * runSpeed;

        if(Input.GetButtonDown("Attack"))
        {
            if(controller.IsGrounded())
                m_animator.SetInteger("ActionState", 2);
        }else
        {
            if(controller.IsGrounded())
            {
                if(Mathf.Abs(m_horizontalMove) > 0)
                {
                
                    m_animator.SetInteger("ActionState", 1);
                    if(!m_walkAudio.isPlaying)
                     m_walkAudio.Play();
                }else
                {
                    m_animator.SetInteger("ActionState", 0);
                    if(m_walkAudio.isPlaying)
                        m_walkAudio.Stop();
                }
            }else
            {
                m_walkAudio.Stop();
            }
        }
        
        if(Input.GetButtonDown("Jump"))
        {
            m_jump = true;
        }

        playerCamera.transform.position = transform.position + cameraOffset;
    }

    public void OnJump()
    {
        m_jumpAudio.Play();
        m_walkAudio.Stop();

        m_animator.SetInteger("ActionState", 3);
    }

    public void OnHitStart()
    {
        IsHitting = true;
        m_attackAudio.Play();
    }

    public void OnHitStop()
    {
        IsHitting = false;
    }

    public void FixedUpdate()
    {
        if(controller.IsGrounded())
        {
            GetComponent<CircleCollider2D>().sharedMaterial.friction = 0.4f;
        }else
        {
            GetComponent<CircleCollider2D>().sharedMaterial.friction = 0.0f;
        }

        controller.Move(m_horizontalMove * Time.fixedDeltaTime, false, m_jump);
        m_jump = false;
    }
}
