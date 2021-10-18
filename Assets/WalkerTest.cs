using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkerTest : ProcessingLite.GP21
{
	//This file is only for testing your movement/behavior.
	//The Walkers will compete in a different program!

	IRandomWalker walker;
	Vector2 walkerPos;

	IRandomWalker walker1;
	Vector2 walkerPos1;

	float scaleFactor = 0.05f;

	void Start()
	{
		//Some adjustments to make testing easier
		Application.targetFrameRate = 120;
		QualitySettings.vSyncCount = 0;

		//Create a walker from the class Example it has the type of WalkerInterface
		walker = new MarKol();
		walker1 = new JesCed();

		//Get the start position for our walker.
		walkerPos = walker.GetStartPosition((int)(Width / scaleFactor) , (int)(Height / scaleFactor));
		walkerPos1 = walker1.GetStartPosition((int)(Width / scaleFactor), (int)(Height / scaleFactor));
	}

	void Update()
	{
		//Draw the walker
		Stroke(255, 0, 0);
		Point(walkerPos.x * scaleFactor, walkerPos.y * scaleFactor);

		Stroke(255, 255, 255);
		Point(walkerPos1.x * scaleFactor, walkerPos1.y * scaleFactor);

		//Get the new movement from the walker.
		walkerPos += walker.Movement();
		walkerPos1 += walker1.Movement();
	}
}