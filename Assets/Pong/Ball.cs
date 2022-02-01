using TMPro;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float Speed = 2;
    public Vector2 Direction;
    public Vector2 Boundary;

    public Transform LeftPaddle;
    public Transform RightPaddle;

    public float PaddleAngleMultiplier;

    [SerializeField]
    private TextMeshProUGUI[] PlayerScores;

    private int[] _scores;

    private void Start()
    {
        SetDirection();

        _scores = new[] { 0, 0 };
    }

    private void Update()
    {
        transform.Translate(Time.deltaTime * Speed * Direction);

        if (transform.position.y > Boundary.y)
            Direction.y *= -1;

        if (transform.position.y < -Boundary.y)
            Direction.y *= -1;

        if (transform.position.x > Boundary.x)
            Score(0);

        if (transform.position.x < -Boundary.x)
            Score(1);

        CheckPaddle();
    }

    private void Score(int forPlayer)
    {
        transform.SetPositionAndRotation(Vector2.zero, Quaternion.identity);
        SetDirection();

        _scores[forPlayer]++;
        PlayerScores[forPlayer].text = _scores[forPlayer].ToString("00");
    }

    private void CheckPaddle()
    {
        //left paddle
        if (transform.position.x - 0.5f <= LeftPaddle.position.x + 0.5f)
            if (transform.position.y < LeftPaddle.position.y + LeftPaddle.localScale.y / 2 &&
                transform.position.y > LeftPaddle.position.y - LeftPaddle.localScale.y / 2)
            {
                Direction.x *= -1;
                Direction.y = PaddleAngleMultiplier * (transform.position.y - LeftPaddle.position.y) / LeftPaddle.localScale.y / 2;
                Direction.Normalize();
                transform.Translate(Time.deltaTime * Speed * Direction);
            }

        // Right paddle
        if (transform.position.x + 0.5f >= RightPaddle.position.x - 0.5f)
            if (transform.position.y < RightPaddle.position.y + RightPaddle.localScale.y / 2 &&
                transform.position.y > RightPaddle.position.y - RightPaddle.localScale.y / 2)
            {
                Direction.x *= -1;
                Direction.y = PaddleAngleMultiplier * (transform.position.y - RightPaddle.position.y) / RightPaddle.localScale.y / 2;
                Direction.Normalize();
                transform.Translate(Time.deltaTime * Speed * Direction);
            }
    }

    private void SetDirection()
    {
        Direction = new Vector2(Random.Range(0, 1) > 0 ? 1 : -1, Random.Range(-1.0f, 1.0f));
    }
}