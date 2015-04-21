using UnityEngine;
using System.Collections;

public class chamber : MonoBehaviour {


	private Transform _t;
	private SpriteRenderer _s;

	public int tileType;
	public int Row;
	public int Column;

	void Awake () {
		_t = GetComponent<Transform>();
		_s = GetComponent<SpriteRenderer>();	
	}
	
	Vector3 tmpPos;
	public void UpdatePosition()
	{
		tmpPos = _t.position;
		tmpPos.x = (Column * gameBoard.TileSize) - gameBoard.WorldOffset;
		tmpPos.y = (Row * gameBoard.TileSize) - gameBoard.WorldOffset;
		_t.position = tmpPos;
		//_s.sprite = gameBoard.Current.chamber[TileColor];
	}

}
