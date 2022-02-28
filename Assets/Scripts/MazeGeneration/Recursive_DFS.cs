using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recursive_DFS : Maze_Base
{
	protected override void Generate()
	{
		Generate(Random.Range(1, width - 1), Random.Range(1, depth - 1));
	}

	void Generate(int x, int z)
	{
		if (CountSquareNeighbors(x, z) >= 2) return;

		map[x, z] = 0;

		directions.Shuffle();

		Generate(x + directions[0].x, z + directions[0].z);
		Generate(x + directions[1].x, z + directions[1].z);
		Generate(x + directions[2].x, z + directions[2].z);
		Generate(x + directions[3].x, z + directions[3].z);
	}
}
