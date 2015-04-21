using UnityEngine;
using System.Collections;

public class lastClickedObject : MonoBehaviour
{
	GameObject lastClicked;
	Ray ray;
	RaycastHit2D rayHit;

	private Vector2 initPostion;



	// update method
	void Update()
	{


		/*
		Ray2D ray = Camera.main.ScreenPointToRay( Input.mousePosition );
		RaycastHit2D hit;
		
		
		
		if( Physics2D.Raycast( ray, -Vector2.up) )
		{
			Debug.Log( hit.transform.gameObject.name );
		}


		// raycast line debug 
		Debug.DrawLine (Camera.main.transform.position,  Input.mousePosition, Color.cyan);
		*/


		if( Input.GetMouseButton(0) )
		{


			Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

			if(hit.collider != null)
			{





				if (gameObject.tag == "Chamber")
				{
					//set initPositionOfObject
					initPostion = hit.collider.gameObject.transform.position;

					//debug
					Debug.Log (hit.collider.gameObject.transform.name);


					Vector2 Pos = hit.collider.gameObject.transform.position;

					hit.collider.gameObject.transform.position = mousePos ;


					//Vector2 HelperPos = Helpers.RoundVec2(Pos);

					//Debug.Log(
				}

				/*
				// loop through the row or column based on the movement
				for ( int i = 0; i <= 7; i++) {

					string hitTile = hit.collider.gameObject.transform.name;
					Vector3 hitTileV = hit.collider.gameObject.transform.position;
					Vector2 hittileCol = GameObject.Find("chamber_" + hitTile[8] + "_" + i).transform.position;
					Vector2[] difference = new Vector2[9];
				


					Debug.Log(GameObject.Find("chamber_" + hitTile[8] + "_" + i).transform.name);
					//if (GameObject.Find("chamber_" + hitTile[8] + "_" + i) != null)
					//{

					//float dist = Vector3.Distance(hit.collider.gameObject.transform.position, GameObject.Find("chamber_" + hitTile[8] + "_" + i).transform.position);



					difference[i] = new Vector2 (hitTileV.x - hittileCol.x, hitTileV.y - hittileCol.y);

						//hitTileV.x - hittileCol.x,
						//hitTileV.y - hittileCol.y,
						//hitTileV.z - hittileCol.z);
					//Debug.Log(dist[i]);
					//Debug.Log("the difference" + difference[i]);



					GameObject.Find("chamber_" + hitTile[8] + "_" + i).transform.position = mousePos - difference[i];
					//}

					//Debug.Log(mousePos.z);


				}
				*/
			}

		}
	}


	// check if the chamber is inside the border
	/*
	public static bool insideBorder(Vector2 pos) {
		return ((int)pos.x >= 0 &&
		        (int)pos.x < w &&
		        (int)pos.y >= 0);
	}
	*/
	
}

