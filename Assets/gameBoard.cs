using UnityEngine;
using System.Collections;

public class gameBoard : MonoBehaviour {

	public const float TileSize = 0.94f;
	public const float WorldOffset = 1.92f;
	
	public const int BoardSize = 7;
	public static chamber[,] chambers = new chamber[BoardSize, BoardSize];

	public static gameBoard Current;

	public Sprite[] TexChamber;
	
	public static void UpdateIndexes(bool updatePositions)
	{
		for (int y = 0; y < BoardSize; y++)
		{
			for (int x = 0; x < BoardSize; x++)
			{
				if (chambers[x,y] != null)
				{
					chambers[x, y].Row = y;
					chambers[x, y].Column = x;
					if (updatePositions)
						chambers[x, y].UpdatePosition();
				}
			}
		}
	}
}
