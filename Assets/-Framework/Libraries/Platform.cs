using UnityEngine;

public class Platform
{
	//������ ���� Ȯ���ϱ� ���� �Լ�
	public static bool IsEditor()
	{
		return Application.isEditor;
	}

	//�÷��̾� ���� Ȯ���ϱ� ���� �Լ�
	public static bool IsRuntime()
	{
		return !IsEditor();
	}

	//������ �÷��̾� ���� Ȯ���ϱ� ���� �Լ�
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

	//�ȵ���̵� �÷��̾� ���� Ȯ���ϱ� ���� �Լ�
	public static bool IsAndroid()
	{
		if( Application.platform==RuntimePlatform.Android )
		{
			return true;
		}

		return false;
	}

    //����ũž ȯ������ Ȯ���ϱ� ���� �Լ�
	public static bool IsDesktop()
	{
		if( IsEditor() || IsWindows() )
		{
			return true;
		}

		return false;
	}

    //����Ʈ�� ȯ������ Ȯ���ϱ� ���� �Լ�
	public static bool IsSmart()
	{
		if( !IsEditor() && !IsWindows() )
		{
			return true;
		}

		return false;
	}
}