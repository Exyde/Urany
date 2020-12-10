using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector2 focusAreaSize;

    FocusArea focusArea;
    Bounds bounds;

    [Header("Offsets")]
    public float verticalOffset;
    public float lookAheadDstX;
    public float lookSmoothTimeX;
    public float verticalSmoothTime;

    public float mnemoOffset = 2.5f;
    private float startOffset;

    //Current look and target we want to smooth to.
    float currentLookAheadX;
    float targetLookAheadX;

    float lookAheadDirX;
    float smoothLookVelocityX;
    float smoothVelocityY;

    bool lookAheadStopped;

	private void Start()
	{
        bounds = target.GetComponent<BoxCollider2D>().bounds;
        focusArea = new FocusArea(bounds, focusAreaSize);
        startOffset = verticalOffset;
	}

    public void Dezoom()
	{
        // verticalOffset = mnemoOffset;
        StopAllCoroutines();
        StartCoroutine(ChangeOffset(mnemoOffset));
	}

    public void FocusPlayer()
	{
        // verticalOffset = startOffset;
        StopAllCoroutines();
        StartCoroutine(ChangeOffset(startOffset));
	}

    IEnumerator ChangeOffset (float target)
	{

        float lerpDuration = 10f;
        float timeElapsed = 0f;

        while (timeElapsed < lerpDuration)
		{
            verticalOffset = Mathf.Lerp(verticalOffset, target, timeElapsed / lerpDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        verticalOffset = target;

    }

	private void LateUpdate()
	{
        focusArea.Update(target.GetComponent<BoxCollider2D>().bounds);

        Vector2 focusPosition = focusArea.centre + Vector2.up * verticalOffset; // Y offset

        if (focusArea.velocity.x != 0)
		{
            lookAheadDirX = Mathf.Sign(focusArea.velocity.x);

            if (Mathf.Sign(target.GetComponent<Movement>().inputs.x) == Mathf.Sign(focusArea.velocity.x) && target.GetComponent<Movement>().inputs.x != 0)
			{
                lookAheadStopped = false;
                targetLookAheadX = lookAheadDirX * lookAheadDstX;

            } else
			{
                if (!lookAheadStopped)
				{
                    lookAheadStopped = true;
                    targetLookAheadX = currentLookAheadX + (lookAheadDirX * lookAheadDstX - currentLookAheadX) / 10f;
				}
			}
		}

        currentLookAheadX = Mathf.SmoothDamp(currentLookAheadX, targetLookAheadX, ref smoothLookVelocityX, lookSmoothTimeX);

        focusPosition.y = Mathf.SmoothDamp(transform.position.y, focusPosition.y, ref smoothVelocityY, verticalSmoothTime);
        focusPosition += Vector2.right * currentLookAheadX;

        transform.position = (Vector3)focusPosition + Vector3.forward * -10; // Update camera pos and set Z.
    }

	private void OnDrawGizmos()
	{
        Gizmos.color = new Color(0, 0, 1, .5f);
        Gizmos.DrawCube(focusArea.centre, focusAreaSize);
	}

	struct FocusArea
	{
        public Vector2 centre;
        public Vector2 velocity;
        float left, right;
        float top, bottom;

        public FocusArea(Bounds targetBounds, Vector2 size)
		{
            left = targetBounds.center.x - size.x / 2;
            right = targetBounds.center.x + size.x / 2;
            bottom = targetBounds.min.y;
            top = targetBounds.min.y + size.y;

            centre = new Vector2((left + right) / 2, (top + bottom) / 2);
            velocity = Vector2.zero;
        }

        public void Update(Bounds targetBounds)
		{
            float shiftX = 0;
            float shiftY = 0;

            //Compute Horizontal Shifting
            if (targetBounds.min.x < left)
			{
                shiftX = targetBounds.min.x - left;
			} else if (targetBounds.max.x > right)
			{
                shiftX = targetBounds.max.x - right;
			}

            //Compute Vertical Shifting
            if (targetBounds.min.y < bottom)
            {
                shiftY= targetBounds.min.y - bottom;
            }
            else if (targetBounds.max.y > top)
            {
                shiftY= targetBounds.max.y - top;
            }

            // Add all the shifting
            left += shiftX;
            right += shiftX;
            top += shiftY;
            bottom += shiftY;
            //Update center
            centre = new Vector2((left + right) / 2, (top + bottom) / 2);
            velocity = new Vector2(shiftX, shiftY); 

        }
    }
}
