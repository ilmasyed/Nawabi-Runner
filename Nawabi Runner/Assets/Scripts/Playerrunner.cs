using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerrunner : MonoBehaviour
{
    public Animator playeranimator;

    public Rigidbody rb;
    private Vector2 touchStartPos;
    private Vector2 touchEndPos;
    private bool isTouching = false;
    // Start is called before the first frame update
    void Start()
    {

        
    }
    public IEnumerator playanimation()
    {
        playeranimator.SetInteger("Jump", 0);
        yield return new WaitForSeconds(0.7f);
        playeranimator.SetInteger("Jump", 1);

    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Check the phase of the touch
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    // Store the starting position of the touch
                    touchStartPos = touch.position;
                    isTouching = true;
                    break;

                case TouchPhase.Moved:
                    // Update the ending position of the touch while moving
                    touchEndPos = touch.position;
                    break;

                case TouchPhase.Ended:
                    // Check if touch ended
                    if (isTouching)
                    {
                        // Calculate the difference between starting and ending positions
                        Vector2 touchDelta = touchEndPos - touchStartPos;

                        // Check if the touch delta is significant to consider it a slide
                        if (touchDelta.magnitude > 50f)
                        {
                            // Check the direction of the slide
                            if (Mathf.Abs(touchDelta.x) > Mathf.Abs(touchDelta.y))
                            {
                                // Horizontal slide
                                if (touchDelta.x > 0)
                                {
                                    Debug.Log("Right slide detected!");
                                }
                                else
                                {
                                    Debug.Log("Left slide detected!");
                                }
                            }
                            else
                            {
                                // Vertical slide
                                if (touchDelta.y > 0)
                                {
                                    Debug.Log("Up slide detected!");
                                    transform.position = new Vector3(transform.position.x, transform.position.y + 1f, 10);
                                    //rb.AddForce(0,500f,0);
                                    StartCoroutine(playanimation());
                                    
                                }
                                else
                                {
                                    Debug.Log("Down slide detected!");
                                }
                            }
                        }
                        else
                        {
                            // If touch delta is not significant, consider it as a tap
                            Debug.Log("Tap detected!");
                        }

                        isTouching = false;
                    }
                    break;
            }
        }
        

    }
    private void FixedUpdate()
    {
        rb.AddForce(0, transform.position.y, 1000f);

    }
}
