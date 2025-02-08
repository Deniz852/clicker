using UnityEngine;

public class Square : MonoBehaviour
{
    public Vector2 targetPos;
    public float moveStep;
    public bool isTrap;
    public float speedFactor;
    public float scale;
    public int catchCount;
 
    void Start()
    {
        if (!isTrap) PlayerPrefs.squares.Add(this);
        targetPos = GetRandomPoint(); 
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPos, moveStep * Time.deltaTime);
        if ((Vector2)transform.position == targetPos) targetPos = GetRandomPoint();
    }
    Vector2 GetRandomPoint()
    {
        Vector2 randomV = new Vector2();
        randomV.x = Random.Range(-6, 6);
        randomV.y = Random.Range(-3, 3);
        return randomV;
    }
    private void OnMouseDown()
    {
        if (isTrap) Player.Defeat();
        else Catch();
    }
    void Catch()
    {
        Player.score++;
        catchCount--;
        if(catchCount == 0)
        {
            Player.squares.remove(this);
            Destroy(gameObject);
        }
        else
        {
            moveStep += speedFactor;
            transform.localScale = (Vector2)transform.localScale - new Vector2(scale, scale);
            transform.position = GetRandomPoint();
        }
    }
}
