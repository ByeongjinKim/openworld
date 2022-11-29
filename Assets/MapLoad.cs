using System.Collections;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MapLoad : MonoBehaviour
{
    void Start()
    {
		StartCoroutine( Load(Application.streamingAssetsPath+"/Windows/Maps/Western.map") );
    }
	
	IEnumerator Load( string url )
	{
		if( string.IsNullOrEmpty(url) ) yield break;

        UnityWebRequest request = UnityWebRequest.Get(url);
		yield return request.SendWebRequest();

		if( request.result==UnityWebRequest.Result.Success )
		{
			AssetBundle assetBundle = AssetBundle.LoadFromMemory(request.downloadHandler.data);
			if( assetBundle!=null )
			{
				string[] sceneArray = assetBundle.GetAllScenePaths();
				foreach( string sceneName in sceneArray )
				{
					SceneManager.LoadScene( sceneName, LoadSceneMode.Additive );
//					SceneManager.SetActiveScene( 
				}
			}
		}
	}
}