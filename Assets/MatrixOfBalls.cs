using System.Collections.Generic;
using UnityEngine;

public class MatrixOfBalls : MonoBehaviour
{
    public const int BallLayer = 8;


    public GameObject Player;
    public GameObject Ball;
    public int Height;
    public int Width;

    private readonly Dictionary<Vector3, GameObject> _ballDict = new Dictionary<Vector3, GameObject>();
    

    void Start ()
    {
        var cloth = FindObjectOfType<Cloth>();
        var colliders = new List<ClothSphereColliderPair>();

        var scale = Ball.transform.localScale.x;
        var offset = new Vector3(scale*(-Width/2f + 0.5f), 0, scale*(-Height/2f + 0.5f));
        for (int x = 0; x < Width; x++)
	    {
	        for (int z = 0; z < Height; z++)
	        {
	            var ball = Instantiate(Ball);
                var position = offset + new Vector3(x * scale, 0, z * scale);
	            ball.transform.position = position;
	            _ballDict[position] = ball;

	            var adjacentPositions = new [] { position + Vector3.left, position + Vector3.back };

                for (var i = 0; i < adjacentPositions.Length; i++)
	            {
                    ConnectToAdjacentBall(ball, adjacentPositions[i], i);
	            }

                colliders.Add(new ClothSphereColliderPair(ball.GetComponent<SphereCollider>()));
            }
	    }

        colliders.Add(new ClothSphereColliderPair(Player.GetComponent<SphereCollider>()));
        cloth.sphereColliders = colliders.ToArray();
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(ray, out hit, 100, 1 << BallLayer);
            if (hit.transform != null)
            {
                hit.transform.GetComponent<Ball>().Click();
            }
        }
    }

    private void ConnectToAdjacentBall(GameObject ball, Vector3 position, int index)
    {
        if (_ballDict.ContainsKey(position))
        {
            ball.GetComponents<SpringJoint>()[index].connectedBody = _ballDict[position].GetComponent<Rigidbody>();

            Debug.Log("Connect");
            Debug.Log(ball.transform.localPosition);
            Debug.Log(ball.GetComponents<SpringJoint>()[index].anchor);
            Debug.Log(ball.GetComponents<SpringJoint>()[index].connectedAnchor);
        }
    }
}
