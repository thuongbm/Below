using UnityEngine;

public class ToolBob : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public float bobAmplitude = 0.1f;
    public float bobFrequency = 2f;
    
    private Vector3 startPos;
    private SpriteRenderer spriteRenderer;
    void Start()
    {
        startPos = transform.localPosition;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float bobOffset = Mathf.Sin(Time.time * bobFrequency) * bobAmplitude;
        transform.localPosition = startPos + new Vector3(0f, bobOffset, 0f);
        float input = Input.GetAxisRaw("Horizontal"); 
        ReverseDirection(input);
    }

     void ReverseDirection(float input)
    {
        if (input > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (input < 0)
        {
            spriteRenderer.flipX = true;
        }
    }
}
