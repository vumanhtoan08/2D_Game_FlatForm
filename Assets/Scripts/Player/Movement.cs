using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //Khai báo biến
    [SerializeField] float speed = 4f; // Player speed
    [SerializeField] float jumpPower = 4f; //Player jump power

    Rigidbody2D _body;

    float _inputHorizontal; // đầu vào của trục x 

    //Biến kiểm tra
    [SerializeField] Transform groundCheck; // check ground collider 
    bool _isJumping; 

    //Layer Mask
    [SerializeField] LayerMask groundLayer;

    #region Getter và Setter
    public bool IsJumping
    {
        get { return _isJumping; }   // Cho phép các script khác truy cập giá trị
        private set { _isJumping = value; } // Chỉ có thể set trong script Movement
    }

    public float InputHorizontal
    {
        get { return _inputHorizontal; }
        private set { _inputHorizontal = value; }
    }    
    #endregion

    void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
    }

    #region Hàm Update
    void Update()
    {
        _inputHorizontal = Input.GetAxis("Horizontal");
        //FlipFace();

        Jump();
    }

    void FixedUpdate()
    {
        Move();
    }
    #endregion

    #region Hành động nhân vật
    void Move() // Phương thức di chuyển
    {
        _body.velocity = new Vector2(_inputHorizontal * speed, _body.velocity.y);
    }
    void Jump() // Phương thức nhảy
    {
        if (Input.GetKey(KeyCode.Space) && IsGround())
        {
            _isJumping = true;
            _body.velocity = new Vector2(_body.velocity.x, jumpPower);
        }
        if (IsGround() && _body.velocity.y <= 0)
        {
            _isJumping = false;
        }
    }

    #endregion

    #region Một số hàm Check
    private bool IsGround() // kiểm tra xem người chơi có đang chạm đất hay không
    {
        return Physics2D.OverlapBox(groundCheck.position, new Vector2(0.4f, 0.1f), 0, groundLayer);
    }
    #endregion
}
