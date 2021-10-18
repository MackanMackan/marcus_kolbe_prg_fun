using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkerTest : ProcessingLite.GP21
{
	//This file is only for testing your movement/behavior.
	//The Walkers will compete in a different program!

	IRandomWalker marKol;
	Vector2 MarKolPos;

	IRandomWalker jesCed;
	Vector2 jesCedPos;

	float scaleFactor = 0.05f;

	void Start()
	{
		//Some adjustments to make testing easier
		Application.targetFrameRate = 120;
		QualitySettings.vSyncCount = 0;

		//Create a walker from the class Example it has the type of WalkerInterface
		marKol = new MarKol();
		jesCed = new JesCed();

		//Get the start position for our walker.
		MarKolPos = marKol.GetStartPosition((int)(Width / scaleFactor) , (int)(Height / scaleFactor));
		jesCedPos = jesCed.GetStartPosition((int)(Width / scaleFactor), (int)(Height / scaleFactor));
	}

	void Update()
	{
		//Draw the walker
		Stroke(255, 0, 0);
		Point(MarKolPos.x * scaleFactor, MarKolPos.y * scaleFactor);

		Stroke(255, 255, 255);
		Point(jesCedPos.x * scaleFactor, jesCedPos.y * scaleFactor);

		//Get the new movement from the walker.
		MarKolPos += marKol.Movement();
		jesCedPos += jesCed.Movement();
	}
}