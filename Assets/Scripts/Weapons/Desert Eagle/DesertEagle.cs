using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesertEagle : Weapon
{
    // Khai báo thuộc tính 
    [SerializeField] Transform _firePoint; // điểm bắn cho súng
    [SerializeField] int _clipSize = 7; // Số lượng đạn trong băng
    int _currentAmmo; // số lượng đạn hiện tại 
    [SerializeField] float _fireRange; // tầm bắn của súng
    [SerializeField] float _reloadTime; // thời gian nạp đạn
    float _attackRateTimmer = 0; // bộ đếm thời gian khi bắn 
    bool _isReloading = false; 

    GameObject _playerPos; // vị trí của Player
    Vector3 _mouPos; // vị trí của mouse

    Animator _anim; // anim của súng

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _currentAmmo = _clipSize;
        GameManager.instance.SetCurrentAmmo(_currentAmmo);
        _playerPos = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnEnable()
    {
        _anim.SetBool("reload", false);
        _isReloading = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isReloading) // khi đang nạp đạn thì không thao tác được những code ở dưới
            return;

        if ((Input.GetKeyDown(KeyCode.R) || _currentAmmo <= 0) && _currentAmmo != _clipSize)
        {
            StartCoroutine(ReLoad());
            return;
        }

        if (Input.GetMouseButton(0) && _attackRateTimmer >= attackRate && _currentAmmo > 0)
        {
            Attack();
        }

        _attackRateTimmer += Time.deltaTime;

    }
    public override void Attack()
    {
        _attackRateTimmer = 0;
        _currentAmmo--;
        GameManager.instance.SetCurrentAmmo(_currentAmmo); // lấy số đạn hiện tại gán vào trong CurrentAmmo của Manager
        _anim.SetTrigger("shoot");
        Audio.instance.PlayShootClip();

        GameObject bullet = BulletPool.instance.GetPooledObject();

        if (bullet != null)
        {
            bullet.transform.position = _firePoint.position;

            _mouPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 direction = _mouPos - _firePoint.position;
            direction.z = 0; // Đảm bảo rằng viên đạn chỉ bay trong mặt phẳng 2D
            direction.y += 0; // Chỉnh sửa để hướng được chính xác

            if (_playerPos.transform.position.x < _mouPos.x)
            {
                bullet.transform.localScale = Vector2.one;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // chuyển đổi từ radian thành độ 
                Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward); // sử dụng Quaternion để xoay với góc xoay -10
                bullet.transform.rotation = rotation;
            } // xoay hướng mặt của nhân vật dựa theo chuột
            else if (_playerPos.transform.position.x > _mouPos.x)
            {
                bullet.transform.localScale = new Vector2(-1, 1);
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Quaternion rotation = Quaternion.AngleAxis(angle + 180, Vector3.forward); // Sử dụng Quaternion để xoay với góc xoay +170
                bullet.transform.rotation = rotation;
            } // xoay hướng mặt nhân vật dựa theo chuột 

            Bullet bulletScript = bullet.GetComponent<Bullet>();
            bulletScript.SetDirection(direction.normalized); // Gọi hàm để thiết lập hướng cho viên đạn
            bulletScript.SetDamage(damage);
            bullet.SetActive(true);
        }
    }

    IEnumerator ReLoad()
    {
        _isReloading = true; // bắt đầu phương thức đặt _isReloading = true
        Debug.Log("Reloading...");
        _anim.SetBool("reload", true);
        Audio.instance.PlayReloadClip();

        yield return new WaitForSeconds(_reloadTime);
        _anim.SetBool("reload", false);

        Debug.Log("Done...");
        _currentAmmo = _clipSize;
        GameManager.instance.SetCurrentAmmo(_currentAmmo);
        _isReloading = false; // sau khi nạp đạn xong đặt lại _isReloading - false
    }
}
