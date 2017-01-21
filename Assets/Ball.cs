using UnityEngine;

public class Ball : MonoBehaviour
{
    void Update()
    {
        var y = transform.position.y < 0 ? 0 : transform.position.y;
        transform.localPosition = new Vector3(transform.localPosition.x, y, transform.localPosition.z);
    }

    public void Click()
    {
        Debug.Log(transform.position);
        GetComponent<Rigidbody>().AddForce(Vector3.up * 10, ForceMode.Impulse);
    }
}
