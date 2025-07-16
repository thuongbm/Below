using UnityEngine;

public class PickaxeControler : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Transform handPosition;
    public float flySpeed = 5f;
    public float rotateSpeed = 360f;
    
    private bool isMining = false;
    private Vector3 targetPos;
    private Quaternion originalRotation;
    private ToolBob toolBob;

    void Start()
    {
        originalRotation = transform.rotation;
        toolBob = GetComponent<ToolBob>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (isMining)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * flySpeed);
            transform.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime);
        }

        else
        {
            transform.position = Vector3.MoveTowards(transform.position, handPosition.position, Time.deltaTime * flySpeed);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, originalRotation, rotateSpeed * Time.deltaTime);
        }
    }

    public void StartMining(Vector3 tileWorldPos)
    {
        targetPos = tileWorldPos + new Vector3(0.5f, 0.5f, 0);
        Debug.Log("Rìu bay tới: " + targetPos);
        isMining = true;

        if (toolBob != null)
        {
            toolBob.enabled = false;
        }
    }

    public void StopMining()
    {
        isMining = false;

        if (toolBob != null)
        {
            toolBob.enabled = true;
        }
    }
    
}
