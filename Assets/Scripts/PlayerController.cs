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

    public bool IsHitting = false;
    
    private Animator m_animator;
    
    public void Start()
    {
        cameraOffset = new Vector3(0, 0, -10.0f);
        controller = GetComponent<CharacterController2D>();
    
        m_animator = GetComponent<Animator>();
    }

    public void Update()
    {
        m_horizontalMove = Input.GetAxis("Horizontal") * runSpeed;

        if(Input.GetButtonDown("Attack"))
        {
            m_animator.SetInteger("ActionState", 2);
            // TODO: implement attack
        }else
        {
            if(Mathf.Abs(m_horizontalMove) > 0)
            {
                m_animator.SetInteger("ActionState", 1);
            }else
            {
                m_animator.SetInteger("ActionState", 0);
            }
        }
        
        if(Input.GetButtonDown("Jump"))
        {
            m_jump = true;
        }

        playerCamera.transform.position = transform.position + cameraOffset;
    }

    public void OnHitStart()
    {
        IsHitting = true;
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
