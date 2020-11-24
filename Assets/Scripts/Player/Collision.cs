using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    //Class for handling side and bottom collisions.
    //You have to tweak the Offsets Vector2 to get it working properly.

    [Header("Layers")]
    public LayerMask groundLayer;

    [Space]
    public bool onGround;
    public bool onWall;
    public bool onRightWall;
    public bool onLeftWall;
    public int wallSide;
    public bool onWallTop;

    [Header("Collision")]
    public float collisionRadius = .25f;
    public Vector2 bottomOffset, rightOffset, leftOffset;
    public Vector2 rightTopOffset, leftTopOffset;
    public Vector2 verticalOffset;
    private Color debugCollisionColor = Color.red;

    // Update is called once per frame
    void Update()
    {
        onGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, collisionRadius, groundLayer);
        onWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, groundLayer)
              || Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, groundLayer);

        onRightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset + verticalOffset, collisionRadius, groundLayer);
        onLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset + verticalOffset, collisionRadius, groundLayer);

        wallSide = onRightWall ? -1 : 1;

        onWallTop = Physics2D.OverlapCircle((Vector2)transform.position + rightTopOffset, collisionRadius, groundLayer)
        || Physics2D.OverlapCircle((Vector2)transform.position + leftTopOffset, collisionRadius, groundLayer);
    }

	private void OnDrawGizmos()
	{
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, collisionRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset + verticalOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset + verticalOffset, collisionRadius);

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere((Vector2)transform.position + rightTopOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftTopOffset, collisionRadius);

    }
}
