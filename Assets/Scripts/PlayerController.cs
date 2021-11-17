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

    public void Start()
    {
        cameraOffset = new Vector3(0, 0, -10.0f);
        controller = GetComponent<CharacterController2D>();
    }

    public void Update()
    {
        m_horizontalMove = Input.GetAxis("Horizontal") * runSpeed;
        if(Input.GetButtonDown("Jump"))
        {
            m_jump = true;
        }

        playerCamera.transform.position = transform.position + cameraOffset;
    }

    public void FixedUpdate()
    {
        controller.Move(m_horizontalMove * Time.fixedDeltaTime, false, m_jump);
        m_jump = false;
    }
}
