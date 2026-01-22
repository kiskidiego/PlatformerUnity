using UnityEngine;

public class CharacterDirection : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    float _facing = 1f;
    private Vector2 direction = Vector2.right;
    bool flipped = false;
    private float facing
    {
        get { return _facing; }
        set 
        { 
            _facing = value; 
            if(_facing < 0f)
            {
                spriteRenderer.flipX = true;
            }
            else if(_facing > 0f)
            {
                spriteRenderer.flipX = false;
            }
        }
    }
    public bool Flipped
    {
        get { return flipped; }
        set 
        { 
            if(flipped != value)
            {
                flipped = value;
                facing *= -1f;
            }
        }
    }
    public Vector2 Direction
    {
        get { return direction; }
        set 
        { 
            direction = value.normalized; 
            if (!Mathf.Approximately(direction.x, 0f))
            {
                facing = flipped ? -Mathf.Sign(direction.x) : Mathf.Sign(direction.x);
            }
        }
    }
    
    public float Facing
    {
        get { return _facing; }
    }
}