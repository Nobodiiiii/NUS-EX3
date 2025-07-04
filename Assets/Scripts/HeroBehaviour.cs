using UnityEngine;

public class HeroBehavoiur : MonoBehaviour
{
    public enum ControlMode { Mouse, Keyboard }
    public ControlMode controlMode = ControlMode.Mouse;

    private float currentSpeed = 20f;
    private float turnSpeed = 45f;

    public GameObject eggPrefab;
    private float lastShootTime = -999f;
    private float shootCooldown = 0.2f;
    
    // 添加CoolDownBar引用
    public CoolDownBar mCoolDown;

    void Start()
    {
        Debug.Assert(mCoolDown != null);   // Must be set in the editor
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            GameManager.instance.heroControlMode = controlMode == ControlMode.Mouse ? "Keyboard" : "Mouse";
            controlMode = controlMode == ControlMode.Mouse ? ControlMode.Keyboard : ControlMode.Mouse;
        }

        if (controlMode == ControlMode.Mouse)
        {
            MouseControl();
        }
        else
        {
            KeyboardControl();
        }

        if (Input.GetKey(KeyCode.Space)&& Time.time - lastShootTime >= shootCooldown)
        {
            
            
                GameObject egg = Instantiate(eggPrefab, transform.position, transform.rotation);
                lastShootTime = Time.time;
                Debug.Log("Spawn Eggs:" + egg.transform.localPosition);
                GameManager.instance.eggCount++;
                mCoolDown.TriggerCoolDown();
            
        }
        
        // make sure cool down period is that of the slider bar
        mCoolDown.SetCoolDownLength(shootCooldown);
    }

    void MouseControl()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        transform.position = mousePos;
    }

    void KeyboardControl()
    {
        float vertical = Input.GetAxis("Vertical");  // W=1, S=-1
        currentSpeed += vertical * 40f * Time.deltaTime;
        currentSpeed = Mathf.Clamp(currentSpeed, -40f, 40f);

        float horizontal = Input.GetAxis("Horizontal");  // A=-1, D=1
        transform.Rotate(0, 0, -horizontal * turnSpeed * Time.deltaTime);

        transform.position += transform.up * currentSpeed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            GameManager.instance.heroCollisionCount++;
        }
    }
}
