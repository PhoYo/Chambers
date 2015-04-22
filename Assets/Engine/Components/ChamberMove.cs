using UnityEngine;
using System.Collections;

public class ChamberMove : MonoBehaviour {
	
	Ray2D ray;
	RaycastHit2D rayHit;

	private Vector2 initPos;
		
	// Update is called once per frame
	void Update () 
	{

		///
		// When the mouse button is first pressed down
		///
		if (Input.GetMouseButtonDown (0)) 
		{
			// get mouse position and 2d raycast hit
			Vector2 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast (mousePos, Vector2.zero);
			
			if (hit.collider != null) 
			{
				if (hit.collider.tag == "Chamber")
				{
					//define intial chamber position
					initPos = hit.collider.gameObject.transform.position;

				}
			}
		}

		///
		// If left mouse is down
		///
		if (Input.GetMouseButton (0)) 
		{
			// get mouse position and 2d raycast hit
			Vector2 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast (mousePos, Vector2.zero);

			if (hit.collider != null) 
			{
				if (hit.collider.tag == "Chamber")
				{
					// define chamber position
					Vector2 pos = hit.collider.gameObject.transform.position;

					// round the postion so it is only moves by one unit
					pos.x = Mathf.Round(mousePos.x);
					pos.y = Mathf.Round(mousePos.y);
					// move the chamber with the new round positions
					hit.collider.gameObject.transform.position = new Vector2( pos.x , pos.y );

					// determine which direction the chamber is being moved
					float differencePos = Vector2.Distance (initPos, pos);

					Debug.Log("the difference : " + differencePos);


				}
				
			}
		}

		///
		// if lef mouse Button is up
		///
		if (Input.GetMouseButtonUp (0)) 
		{


		}
	}
	
}
