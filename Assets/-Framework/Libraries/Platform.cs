using UnityEngine;

public class Platform
{
	//에디터 인지 확인하기 위한 함수
	public static bool IsEditor()
	{
		return Application.isEditor;
	}

	//플레이어 인지 확인하기 위한 함수
	public static bool IsRuntime()
	{
		return !IsEditor();
	}

	//윈도우 플레이어 인지 확인하기 위한 함수
	public static bool IsWindows()
	{
		switch( Application.platform )
		{
			case RuntimePlatform.WindowsPlayer:
			case RuntimePlatform.WindowsEditor:
				return true;
		}

		return false;
	}

	//안드로이드 플레이어 인지 확인하기 위한 함수
	public static bool IsAndroid()
	{
		if( Application.platform==RuntimePlatform.Android )
		{
			return true;
		}

		return false;
	}

    //데스크탑 환경인지 확인하기 위한 함수
	public static bool IsDesktop()
	{
		if( IsEditor() || IsWindows() )
		{
			return true;
		}

		return false;
	}

    //스마트폰 환경인지 확인하기 위한 함수
	public static bool IsSmart()
	{
		if( !IsEditor() && !IsWindows() )
		{
			return true;
		}

		return false;
	}
}