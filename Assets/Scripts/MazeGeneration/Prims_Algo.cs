using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prims_Algo : Maze_Base
{
	int x, z;
	List<MapLocation> walls = new List<MapLocation>();

	protected override void Generate()
	{
		x = 2;
		z = 2;

		map[x, z] = 0;

		TrackNewWalls();

		int countLoops = 0;

		while (walls.Count > 0 && countLoops < 2500)
		{
			int rWall = Random.Range(0, walls.Count);
			x = walls[rWall].x;
			z = walls[rWall].z;

			walls.RemoveAt(rWall);

			if (CountSquareNeighbors(x, z) == 1)
			{
				map[x, z] = 0;
				TrackNewWalls();
			}

			countLoops++;
		}
	}

	private void TrackNewWalls()
	{
		walls.Add(new MapLocation(x + 1, z));
		walls.Add(new MapLocation(x - 1, z));
		walls.Add(new MapLocation(x, z + 1));
		walls.Add(new MapLocation(x, z - 1));
	}
}
