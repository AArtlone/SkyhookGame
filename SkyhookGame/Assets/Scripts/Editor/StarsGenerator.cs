using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
public class StarsGenerator : MonoBehaviour
{
	private static GameObject[] prefabs;
	private static Transform starsContainer;

	[MenuItem("Generators/Generate Stars")]
	static void Generate()
	{
		ChangeScene();
		ClearScene();
		CreateSolarSystems();
	}

	static void ChangeScene()
	{
		EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
		EditorSceneManager.OpenScene("Assets/Scenes/MapViews/StarsView.unity");
	}

	static void CreateSolarSystems()
	{
		prefabs = Resources.LoadAll<GameObject>("Prefabs/SolarSystems");
		if (starsContainer == null)
		{
			starsContainer =
				GameObject.FindGameObjectWithTag("StarsContainer").transform;
		}

		var map_width = 20;
		var map_height = 15;

		for (int x = 0; x < map_width; x++)
		{
			for (int y = 0; y < map_height; y++)
			{
				var solar_system_index = Random.Range(0, prefabs.Length);
				var rng = Random.Range(0, 101);

				if (rng < 12)
				{
					Instantiate(
						prefabs[solar_system_index],
						new Vector3(x, y, 0),
						Quaternion.identity,
						starsContainer);
				}
			}
		}
	}

	static void ClearScene()
	{
		if (starsContainer == null)
		{
			starsContainer =
				GameObject.FindGameObjectWithTag("StarsContainer").transform;
		}

		for (int i = 0; i < starsContainer.childCount; i++)
		{
			DestroyImmediate(starsContainer.GetChild(i).gameObject);
		}
	}
}
