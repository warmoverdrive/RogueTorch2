using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLocation
{


	public int x, z;

	public MapLocation(int _x, int _z)
	{
		x = _x;
		z = _z;
	}
}

public class Maze_Base : MonoBehaviour
{
	[SerializeField] protected int width = 30; // x
	[SerializeField] protected int depth = 30; // z
	[SerializeField] int scale = 6;
	protected byte[,] map;
	protected List<MapLocation> directions = new List<MapLocation>(){
		new MapLocation(1,0),
		new MapLocation(0,1),
		new MapLocation(-1,0),
		new MapLocation(0,-1)
	};

	// Start is called before the first frame update
	void Start()
	{
		var startTime = Time.realtimeSinceStartup;

		InitializeMap();
		Generate();
		RenderMap();

		Debug.Log("Time to Generate: " + (Time.realtimeSinceStartup - startTime));
	}

	private void RenderMap()
	{
		for (int z = 0; z < depth; z++)
			for (int x = 0; x < width; x++)
			{
				if (map[x, z] == 1)
				{
					Vector3 pos = new Vector3(x * scale, 0, z * scale);
					GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
					wall.transform.localScale *= scale;
					wall.transform.position = pos;
				}
			}
	}

	protected virtual void Generate()
	{
		for (int z = 0; z < depth; z++)
			for (int x = 0; x < width; x++)
			{
				if (Random.Range(0, 100) < 50)
					map[x, z] = 0;
			}
	}

	private void InitializeMap()
	{
		map = new byte[width, depth];

		for (int z = 0; z < depth; z++)
			for (int x = 0; x < width; x++)
			{
				map[x, z] = 1;
			}
	}

	protected int CountSquareNeighbors(int x, int z)
	{
		int count = 0;

		if (x <= 0 || x >= width - 1 || z <= 0 || z >= depth - 1)
			return 5;

		if (map[x - 1, z] == 0) count++;
		if (map[x + 1, z] == 0) count++;
		if (map[x, z + 1] == 0) count++;
		if (map[x, z - 1] == 0) count++;

		return count;
	}

	protected int CountDiagonalNeighbors(int x, int z)
	{
		int count = 0;

		if (x <= 0 || x >= width - 1 || z <= 0 || z >= depth - 1)
			return 5;

		if (map[x + 1, z + 1] == 0) count++;
		if (map[x + 1, z - 1] == 0) count++;
		if (map[x - 1, z + 1] == 0) count++;
		if (map[x - 1, z - 1] == 0) count++;

		return count;
	}

	protected int CountAllNeighbors(int x, int z)
	{
		return CountSquareNeighbors(x, z) + CountDiagonalNeighbors(x, z);
	}
}