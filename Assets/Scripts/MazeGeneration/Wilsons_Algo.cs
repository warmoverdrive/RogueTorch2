using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wilsons_Algo : Maze_Base
{
	List<MapLocation> availableCells = new List<MapLocation>();
	int x, z;

	protected override void Generate()
	{
		// create random starting cell
		x = Random.Range(2, width - 1);
		z = Random.Range(2, depth - 1);

		map[x, z] = 2;

		int lCounter = 0;

		while (GetAvailableCells() > 1 && lCounter < 5000)
		{
			RandomWalk();
			lCounter++;
		}
	}

	int GetAvailableCells()
	{
		availableCells.Clear();

		for (int z = 1; z < depth - 1; z++)
			for (int x = 1; x < width - 1; x++)
			{
				if (CountSquareMazeNeighbors(x, z) == 0)
					availableCells.Add(new MapLocation(x, z));
			}

		return availableCells.Count;
	}

	void RandomWalk()
	{
		List<MapLocation> inWalk = new List<MapLocation>();

		int randStart = Random.Range(0, availableCells.Count);
		int currentX = availableCells[randStart].x;
		int currentZ = availableCells[randStart].z;

		inWalk.Add(new MapLocation(currentX, currentZ));

		int loopCount = 0;
		bool validPath = false;

		while (currentX > 0 && currentX < width - 1 &&
			currentZ > 0 && currentZ < depth - 1 &&
			loopCount < 5000 && !validPath)
		{
			map[currentX, currentZ] = 0;

			if (CountSquareMazeNeighbors(currentX, currentZ) > 1)
				break;

			int randDirection = Random.Range(0, directions.Count);

			int nextX = currentX + directions[randDirection].x;
			int nextZ = currentZ + directions[randDirection].z;

			if (CountSquareNeighbors(nextX, nextZ) < 2)
			{
				currentX += directions[randDirection].x;
				currentZ += directions[randDirection].z;

				inWalk.Add(new MapLocation(currentX, currentZ));
			}

			validPath = CountSquareMazeNeighbors(currentX, currentZ) == 1;

			loopCount++;
		}

		if (validPath)
		{
			map[currentX, currentZ] = 0;
			Debug.Log("Path Found");

			foreach (var loc in inWalk)
				map[loc.x, loc.z] = 2;
		}
		else
			foreach (var loc in inWalk)
				map[loc.x, loc.z] = 1;

		inWalk.Clear();
	}

	int CountSquareMazeNeighbors(int x, int z)
	{
		int count = 0;

		if (x <= 0 || x >= width - 1 || z <= 0 || z >= depth - 1)
			return 5;

		foreach (var dir in directions)
		{
			int nx = x + dir.x;
			int nz = z + dir.z;
			if (map[nx, nz] == 2)
				count++;
		}

		return count;
	}
}
