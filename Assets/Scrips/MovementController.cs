using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
    Rigidbody2D rb;  // Biến lưu trữ tham chiếu đến Rigidbody2D của đối tượng

    [SerializeField] int speed;  // Tốc độ di chuyển cơ bản, có thể điều chỉnh trong Unity Inspector
    float speedMultiplier;  // Hệ số điều chỉnh tốc độ

    [Range(1, 20)]
    [SerializeField] float acceleration;  // Gia tốc, có thể điều chỉnh từ 1 đến 10 trong Unity Inspector
    bool btnPressed;  // Biến kiểm tra xem nút bấm có được nhấn không

    bool isWallTouch;  // Biến kiểm tra xem đối tượng có chạm vào tường không
    public LayerMask wallLayer;  // Lớp tường để kiểm tra va chạm
    public Transform wallCheckPoint;  // Điểm kiểm tra va chạm với tường

    Vector2 relativeTranfrom;  // Vector đại diện cho định hướng của đối tượng trong hệ tọa độ địa phương

    public bool isOnPlatform;
    public Rigidbody2D platformRb;

    public ParticleControl particleController;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();  // Lấy tham chiếu đến Rigidbody2D khi đối tượng được khởi tạo
    }

    private void Start()
    {
        UpdateRelativeTransform();  // Cập nhật giá trị ban đầu cho relativeTranfrom
    }

    private void FixedUpdate()
    {
        UpdateSpeedMultiplier();  // Cập nhật giá trị speedMultiplier dựa trên trạng thái nút bấm
        float targetSpeed = speed * speedMultiplier * relativeTranfrom.x;
        if (isOnPlatform)
        {
            rb.velocity = new Vector2(targetSpeed+platformRb.velocity.x, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(targetSpeed, rb.velocity.y);

        }
        

        // Kiểm tra va chạm với tường ở vị trí wallCheckPoint
        isWallTouch = Physics2D.OverlapBox(wallCheckPoint.position, new Vector2(0.06f, 0.6f), 0, wallLayer);
        if (isWallTouch)
        {
            Flip();  // Đảo ngược hướng di chuyển nếu có va chạm với tường
        }
    }

    public void Flip()
    {
        particleController.PlayTouchParticle(wallCheckPoint.position);
        // Xoay đối tượng 180 độ quanh trục y
        transform.Rotate(0, 180, 0);
        UpdateRelativeTransform();  // Cập nhật lại giá trị relativeTranfrom sau khi xoay
    }

    void UpdateRelativeTransform()
    {
        // Cập nhật relativeTranfrom dựa trên hệ tọa độ địa phương
        relativeTranfrom = transform.InverseTransformVector(Vector2.one);
    }

    public void Move(InputAction.CallbackContext value)
    {
        // Xử lý đầu vào từ hệ thống Input
        if (value.started)
        {
            btnPressed = true;  // Nút bấm được nhấn
        }
        else if (value.canceled)
        {
            btnPressed = false;  // Nút bấm không còn nhấn
        }
    }

    void UpdateSpeedMultiplier()
    {
        // Cập nhật giá trị speedMultiplier
        if (btnPressed && speedMultiplier < 1)
        {
            // Tăng speedMultiplier nếu nút bấm được nhấn và speedMultiplier nhỏ hơn 1
            speedMultiplier += Time.deltaTime * acceleration;
        }
        else if (!btnPressed && speedMultiplier > 0)
        {
            // Giảm speedMultiplier nếu nút bấm không còn nhấn và speedMultiplier lớn hơn 0
            speedMultiplier -= Time.deltaTime * acceleration;
            if (speedMultiplier < 0) speedMultiplier = 0;  // Đảm bảo speedMultiplier không nhỏ hơn 0
        }
    }
}
