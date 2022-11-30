using System.IO;
using System;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LibraryBase
{
	//문자열을 확인하기 위한 함수
    public static bool Is( string value )
    {
		if( value==null ) return false;

		if( string.IsNullOrEmpty(value) )
		{
			return false;
		}

		return true;
    }

	//장면을 얻기 위한 함수
	public static Scene GetScene( int buildIndex )
	{
		for( int i=0; i<SceneManager.sceneCount; i++ )
		{
			if( SceneManager.GetSceneAt(i).buildIndex==buildIndex )
			{
				return SceneManager.GetSceneAt(i);
			}
		}

		return new Scene();
	}

	// 장면을 얻기 위한 함수
	public static Scene	GetScene( string sceneName )
	{
		if( !Is(sceneName) ) return new Scene();

		for( int i=0; i<SceneManager.sceneCount; i++ )
		{
			if( SceneManager.GetSceneAt(i).name==sceneName )
			{
				return SceneManager.GetSceneAt(i);
			}
		}

		return new Scene();
	}

	//장면을 얻기 위한 함수
	public static Scene GetScene( SceneLevel buildIndex )
	{
		return GetScene( (int)buildIndex );
	}

	//장면이 로드되어 있는지 확인하기 위한 함수
	public static bool IsScene( SceneLevel buildIndex )
	{
		Scene scene = GetScene(buildIndex);
		if( scene!=null )
		{
			return scene.isLoaded;
		}

		return false;
	}

	//장면을 불러오기 위한 함수
	public static bool LoadScene( SceneLevel buildIndex, Action<object, object> func=null, object wParam=null, object lParam=null )
	{
//		if( func==null ) return;	//(NULL)값을 허용함
//		if( wParam==null ) return;	//(NULL)값을 허용함
//		if( lParam==null ) return;	//(NULL)값을 허용함

		if( wParam==null ) wParam = buildIndex;

		if( !IsScene(buildIndex) )
		{
			AsyncOperation asyncOperation = SceneManager.LoadSceneAsync( (int)buildIndex, LoadSceneMode.Additive );
			if (func != null)
			{
				ApplicationBehaviour.This.Coroutine( asyncOperation, func, wParam, lParam );
			}

//			ApplicationBehaviour.This.Process.Register(asyncOperation);
			return true;
		}
		else
		if( func!=null )
		{
			func( wParam, lParam );
			return true;
		}

		return false;
	}

	//장면을 언로드하기 위한 함수
	public static void UnloadScene( Scene scene, Action<object, object> func=null, object wParam=null, object lParam=null )
	{
		if( scene==null ) return;
//		if( func==null ) return;	//(NULL)값을 허용함
//		if( wParam==null ) return;	//(NULL)값을 허용함
//		if( lParam==null ) return;	//(NULL)값을 허용함

		if( scene.isLoaded )
		{
			if( ApplicationBehaviour.IsStartup() )
			{
				ApplicationBehaviour.This.Coroutine( SceneManager.UnloadSceneAsync(scene), func, wParam, lParam );
			}
			else
			{
				SceneManager.UnloadSceneAsync(scene);
			}
		}
	}

	//장면을 언로드하기 위한 함수
	public static void UnloadScene( SceneLevel buildIndex, Action<object, object> func=null, object wParam=null, object lParam=null )
	{
//		if( func==null ) return;		//(NULL)값을 허용함
//		if( wParam==null ) return;		//(NULL)값을 허용함
//		if( lParam==null ) return;		//(NULL)값을 허용함

		Scene scene = GetScene( (int)buildIndex );
		if( scene!=null && scene.isLoaded )
		{
			UnloadScene( scene, func, wParam, lParam );
		}
		else
		if( scene==null )
		{
			if( func!=null )
			{
				func( wParam, lParam );
			}
		}
	}

	//장면 설정을 활성화 하기 위한 함수
	public static void ActiveScene( Scene scene )
	{
		if( scene==null ) return;
		if( !scene.isLoaded ) return;

		SceneManager.SetActiveScene( scene );
	}

	//장면 설정을 활성화 하기 위한 함수
	public static void funcActiveScene( object wParam=null, object lParam=null )
	{
		Scene scene;

		if( ( wParam==null || wParam.GetType()!=typeof(SceneLevel) ) && Is((string)lParam) ) 
		{
			ActiveScene( GetScene((string)lParam) );
		}
		else
		if( wParam!=null && wParam.GetType()==typeof(Scene) )
		{
			ActiveScene( (Scene)wParam );
			return;
		}

		if( wParam==null || wParam.GetType()!=typeof(SceneLevel) ) return;

		scene = GetScene((int)wParam);
		if( scene!=null && scene.isLoaded )
		{
			ActiveScene( scene );
		}
	}

	//파일을 확인하기 위한 함수
	public static bool IsFile( string filepath )
	{
		if( !Is(filepath) ) return false;

		if( File.Exists(filepath) ) 
		{
			return true;
		}

		return false;
	}

	//파일의 확장자를 얻기 위한 함수
	public static string Ext( string filepath )
	{
		if( !Is(filepath) ) return null;

		string[] urlarray	= filepath.Split('?');
		string[] strarray	= urlarray[0].Split('.');
		if( strarray.Length<1 ) return null;

		return strarray[strarray.Length-1].ToLower();
	}

	//파일 이름을 얻기 위한 함수
	public static string GetFileName( string filepath )
	{
		if( !Is(filepath) ) return null;

		string filenameext = GetFileNameExt(filepath);

		string[] strarray = filenameext.Split('.');
		if( strarray.Length<1 ) return null;

		return filenameext.Substring( 0, filenameext.Length-Ext(filenameext).Length+1 );
	}

	//파일 이름을 얻기 위한 함수
	public static string GetFileNameExt( string filepath )
	{
		if( !Is(filepath) ) return null;

		string[] strarray = filepath.Replace( "\\", "/" ).Split('/');
		if( strarray.Length<1 ) return null;

		return strarray[strarray.Length-1];
	}

	//파일 이름을 얻기 위한 함수
	public static string GetPath( string filepath )
	{
		if( !Is(filepath) ) return null;
		return filepath.Substring( 0, filepath.Length-GetFileNameExt(filepath).Length );
	}

	//범위를 제한하기 위한 함수
	public static float Limit( float fMin, float fMax, float value )
	{
		float min = Mathf.Min( fMin, fMax );
		float max = Mathf.Max( fMin, fMax );

		value = Mathf.Max( min, value );
		value = Mathf.Min( max, value );

		return value;
	}

	//컴포넌트를 삭제하기 위한 함수
	public static void Destroy( Component component )
	{
		if( component==null ) return;
		Component.Destroy( component );
	}

	public static float GetDeltaTime( float value, bool harp=false )
	{
		if( harp )
		{
			return Mathf.Min( SYSTEM.UNSCALE_TIME_GRAPHIC_LIMIT_UNDER_FRAME_HARP, value );
		}

		return Mathf.Min( SYSTEM.UNSCALE_TIME_GRAPHIC_LIMIT_UNDER_FRAME, value );
	}

	public static float GetUnscaleFixedDeltaTime( bool harp=false )
	{
		return GetDeltaTime( Time.fixedUnscaledDeltaTime, harp );
	}

	public static float GetUnscaleDeltaTime( bool harp=false )
	{
		return GetDeltaTime( Time.unscaledDeltaTime, harp );
	}

	public static float GetFixedDeltaTime( bool harp=false )
	{
		return GetDeltaTime( Time.fixedDeltaTime, harp );
	}

	public static float GetDeltaTime( bool harp=false )
	{
		return GetDeltaTime( Time.deltaTime, harp );
	}

	public static int Random( int min, int max )
	{
		return UnityEngine.Random.Range( min, max+1 );
	}

	//난수를 얻기 위한 함수
	public static float Random( float min, float max )
	{
		return UnityEngine.Random.Range( Mathf.Min(min, max), Mathf.Max(min, max) );
	}

	//난수를 얻기 위한 함수
	public static int RandomInt( Vector2 range )
	{
		return Random( (int)range.x, (int)range.y );
	}

	//난수를 얻기 위한 함수
	public static float Random( Vector2 range )
	{
		return Random( range.x, range.y );
	}

	//확률을 처리하기 위한 함수
	public static bool Random( int value )
	{
		if( Random(0,99)<value )
		{
			return true;
		}

		return false;
	}

	public static float BothRandom( float min, float max )
	{
		float value = Random( Mathf.Min(min, max), Mathf.Max(min, max) );

		if( Random(50) )
		{
			value *= -1f;
		}

		return value;
	}
}