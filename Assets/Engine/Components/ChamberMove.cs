using UnityEngine;
using System.Collections;

public class ChamberMove : MonoBehaviour {
	
	Ray2D ray;
	RaycastHit2D rayHit;

	private Vector2 initPos;

	private enum MovementDir {

		Left,
		Right,
		Up,
		Down
	}
	private MovementDir movementDir;



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



	
					// determine which direction the chamber is being moved
					if (pos.x > initPos.x) {
						movementDir = MovementDir.Right;
					} else if ( pos.x < initPos.x) {
						movementDir = MovementDir.Left;
					} else if (pos.y > initPos.y) {
						movementDir = MovementDir.Up;
					} else if (pos.y < initPos.y) {
						movementDir = MovementDir.Down;
					}

					//Debug.Log(" chamber Movement Direction : " + movementDir);
				



					// find out the distance that the chamber has moved
					float differencePos = Vector2.Distance (initPos, pos);


					// go through all of the chambers in the array
					for (int x = 0; CreateChamberArray.arraySize < 0; x++)
					{
						// work through the whole array
						for (int y = 0; CreateChamberArray.arraySize < 0; y++)
						{
							// move left
							if (movementDir == MovementDir.Left){
								//CreateChamberArray.ChamberArray[x,y];
							} else if (movementDir == MovementDir.Right) {

							} else if (movementDir == MovementDir.Up) {

							} else if (movementDir == MovementDir.Down) {

							}
						}
					}



					// Move all the chambers that are selected

					// move the chamber with the new round position
					hit.collider.gameObject.transform.position = new Vector2( pos.x , pos.y );
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
