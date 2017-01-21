using System.Collections.Generic;
using UnityEngine;

public class MatrixOfBalls : MonoBehaviour
{
    public const int BallLayer = 8;


    public GameObject Ball;
    public int Height;
    public int Width;

    private readonly Dictionary<Vector3, GameObject> _ballDict = new Dictionary<Vector3, GameObject>();
    

    void Start ()
	{
        transform.position = new Vector3(-Width/2f, 0, -Height/2f);

	    for (int x = 0; x < Width; x++)
	    {
	        for (int z = 0; z < Height; z++)
	        {
	            var ball = Instantiate(Ball);
                var position = new Vector3(x, 0, z);
	            ball.transform.SetParent(transform);
	            ball.transform.localPosition = position;
//	            ball.GetComponent<Ball>().InitPosition = position;
	            _ballDict[position] = ball;

	            var adjacentPositions = new [] { position + Vector3.left, position + Vector3.back };

                for (var i = 0; i < adjacentPositions.Length; i++)
	            {
                    ConnectToAdjacentBall(ball, adjacentPositions[i], i);
	            }
            }
	    }
	}

    void Update()
    {
//        if (Input.GetMouseButtonUp(0))
//        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(ray, out hit, 100, 1 << BallLayer);
            if (hit.transform != null)
            {
                hit.transform.GetComponent<Ball>().Click();
            }
//        }
    }

    private void ConnectToAdjacentBall(GameObject ball, Vector3 position, int index)
    {
        if (_ballDict.ContainsKey(position))
        {
            ball.GetComponents<HingeJoint>()[index].connectedBody = _ballDict[position].GetComponent<Rigidbody>();
        }
    }
}
