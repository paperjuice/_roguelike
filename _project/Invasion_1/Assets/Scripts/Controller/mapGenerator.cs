using UnityEngine;
using System.Collections;

public class mapGenerator : MonoBehaviour {

	[SerializeField] private GameObject[] tiles;
	[SerializeField] private int numberOfTilesOnHorizontal;
	[SerializeField] private int numberOfTilesOnVertical;

	private Vector3 initialPos;
	private int randomTile;
	private int rollRandomRotation;
	private float[] randomTileRotation = { 0f, 90f, 180f, 270f };




	void Start()
	{
		initialPos = transform.position;

		for (int j = 0; j < numberOfTilesOnVertical; j++)
		{
			for (int i = 0; i < numberOfTilesOnHorizontal; i++)
			{
				randomTile = Random.Range(0, tiles.Length );
				rollRandomRotation = Random.Range(0, 4);
				Instantiate(tiles[randomTile], transform.position,  tiles[randomTile].transform.rotation = Quaternion.Euler(0f, randomTileRotation[rollRandomRotation], 0f));
				transform.position += new Vector3(90f, 0f, 0f);
			}

			transform.position = new Vector3(initialPos.x, 0f, transform.position.z);
			transform.position += new Vector3(0f, 0, 90f);
		}
	}
}
