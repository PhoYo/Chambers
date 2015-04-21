using UnityEngine;
using System.Collections;

public class ChamberMove : MonoBehaviour {
	
	Ray2D ray;
	RaycastHit2D rayHit;
		
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetMouseButton (0)) 
		{


			Vector2 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast (mousePos, Vector2.zero);
			if (hit.collider != null) 
			{
				if (hit.collider.tag == "Chamber")
				{
					Vector2 initPos = hit.collider.gameObject.transform.position;
					//hit.collider.gameObject.GetComponent<SpriteRenderer>().sprite = 
					//Debug.Log (hit.collider.name);
					Debug.Log(mousePos);

					Vector2 pos = hit.collider.gameObject.transform.position;

					pos.x = Mathf.Round(mousePos.x);
					pos.y = Mathf.Round(mousePos.y);

					Vector2 RoundedPos = Helpers.RoundVec2(pos);

					hit.collider.gameObject.transform.position = new Vector2( pos.x , pos.y );

					foreach (Chamber cham in CreateChamberArray.level.LevelChambers)
					{


					}

				//	Debug.Log(pos.x);


				}
				
			}
		}

		if (Input.GetMouseButtonUp (0)) 
		{


		}
	}

}
