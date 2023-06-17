using UnityEngine;

public static class ExtensionMethods
{

	public static Vector3 GetRandomPoint(this Bounds bounds)
	{
		float x = Random.Range(bounds.min.x, bounds.max.x);
		float z = Random.Range(bounds.min.z, bounds.max.z);
		float y = Random.Range(bounds.min.y, bounds.max.y);

		return new Vector3(x, y, z);
	}
}
