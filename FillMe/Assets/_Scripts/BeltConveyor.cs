using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class BeltConveyor : MonoBehaviour
{
    [Header("Set in Inspector")]
    public float speed = 1f;

    private Rigidbody rb;

    static private BeltConveyor _S;

    public delegate void onWaterTouchedGround();
    public static event onWaterTouchedGround OnWaterTouchedGround;

    private void Awake()
    {
        _S = this;
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rb.position -= transform.right * speed * Time.deltaTime;
        rb.MovePosition(rb.position + transform.right * speed * Time.deltaTime);
    }
    
    private void OnParticleCollision(GameObject other)
    {
        print(other.name);
        OnWaterTouchedGround?.Invoke();
    }
    
    //------------Static-------------\\

    public static BeltConveyor S
    {
        get
        {
            if (_S == null) Debug.Log("BeltConveyor: Trying to access S when it's NULL");
            return _S;
        }
        private set
        {
            if (_S == null)
            {
                _S = value;
            }
        }
    }
}
