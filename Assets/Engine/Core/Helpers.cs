using UnityEngine;
using System.Collections;

public static class Helpers {
	/// <summary>
	/// Rounds the vec2.
	/// </summary>
	/// <returns>The vec2.</returns>
	/// <param name="v">V.</param>
	public static Vector2 RoundVec2 (Vector2 v) 
	{
		return new Vector2(Mathf.Round(v.x), Mathf.Round(v.y));

	}


	public static bool insideBorder(Vector2 pos, Level level) {
		return ((int)pos.x >= 0 &&
		        (int)pos.x < level.width &&
		        (int)pos.y >= 0 &&
		        (int)pos.y < level.height);
	}


	public static void UpdateLevel ()
	{
		foreach (Chamber cham in CreateChamberArray.level.LevelChambers) 
		{
			Vector2 chamberPos = new Vector2( Mathf.Round( cham.Object.transform.position.x ), Mathf.Round( cham.Object.transform.position.y) );

	
			// is the chamber in the level bounds
			// if not delete it 
			// if a chamber is not in a bound area then make that part a null object
				
		}


	}



	/// <summary>
	/// Insides the stage.
	/// </summary>
	/// <returns><c>true</c>, if stage was insided, <c>false</c> otherwise.</returns>
	/// <param name="pos">Position.</param>
	
}
