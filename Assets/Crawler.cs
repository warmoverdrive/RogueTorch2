using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crawler : Maze
{
	[SerializeField] int drunkards = 1;

	protected override void Generate()
	{
		bool done = false;
		int x = width / 2; // Random.Range(0, width);
		int z = depth / 2; // Random.Range(0, depth);

		map[x, z] = 0;

		int crawls = 0;
		while (crawls < drunkards)
		{
			while (!done)
			{
				map[x, z] = 0;

				if (Random.Range(0, 100) < 50)
					x += Random.Range(-1, 2);
				else
					z += Random.Range(-1, 2);

				done |= (x < 1 || x >= width - 1 || z < 1 || z >= depth - 1);
			}
			crawls++;
			done = false;
			x = width / 2; // Random.Range(0, width)
			z = depth / 2; // Random.Range(0, depth)
		}

	}
}
