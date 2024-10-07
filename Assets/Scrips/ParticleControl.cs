using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleControl : MonoBehaviour
{
    // Tham chiếu đến hệ thống hạt (Particle System) sẽ được kích hoạt khi điều kiện thỏa mãn
    [SerializeField] ParticleSystem movementParticle;
    [SerializeField] ParticleSystem FallParticle;
    [SerializeField] ParticleSystem touchParticle;
    [SerializeField] ParticleSystem DieParticle;
    // Ngưỡng vận tốc khi vượt qua giá trị này, hiệu ứng hạt sẽ được kích hoạt
    [Range(0, 10)]
    [SerializeField] int occurAfterVelocity;

    // Khoảng thời gian giữa hai lần hình thành hiệu ứng hạt bụi (tính bằng giây)
    [Range(0, 0.2f)]
    [SerializeField] float dustFormationPeriod;

    // Tham chiếu đến thành phần Rigidbody2D được gán cho nhân vật
    [SerializeField] Rigidbody2D playerRb;

    // Biến đếm thời gian để xác định khi nào nên tạo hiệu ứng hạt tiếp theo
    float counter;
    bool isOnGround;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager=GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public void PlayTouchParticle( Vector2 pos )
    {
        touchParticle.transform.position = pos;
        touchParticle.Play();
        audioManager.PlaySFX(audioManager.wallTouch);
    }
    public void PlayDieParticle(Vector2 pos)
    {
        audioManager.PlaySFX(audioManager.death);
        DieParticle.transform.position = pos;
        DieParticle.Play();
    }

    private void Start()
    {
        touchParticle.transform.parent = null;
    }
    private void Update()
    {
        // Tăng biến đếm theo thời gian mỗi frame
        counter += Time.deltaTime;

        // Kiểm tra xem vận tốc theo trục X của nhân vật có vượt qua ngưỡng occurAfterVelocity không
        if (isOnGround==true&&Mathf.Abs(playerRb.velocity.x) > occurAfterVelocity)
        {
            // Nếu thời gian đếm vượt quá dustFormationPeriod, hiệu ứng hạt sẽ được kích hoạt
            if (counter > dustFormationPeriod)
            {
                // Kích hoạt hiệu ứng hạt chuyển động
                movementParticle.Play();

                // Đặt lại biến đếm về 0 để chuẩn bị cho lần kiểm tra tiếp theo
                counter = 0;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            isOnGround = true;
            FallParticle.Play();
            audioManager.PlaySFX(audioManager.wallTouch);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            isOnGround=false;
        }
    }
}
