using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ApplicationBehaviourBase : TransformBehaviour
{
	protected virtual void Start()
	{
	}

	//���ø����̼��� ���۵Ǿ����� Ȯ���ϱ� ���� �Լ�
	public static bool IsStartup()
	{
		if( This!=null )
		{
			return true;
		}

		return false;
	}

	//�ڷ�ƾ�� �����ϱ� ���� �Լ�
	public void Coroutine( YieldInstruction yieldInstruction, Action<object, object> func, object wParam=null, object lParam=null )
	{
		if( yieldInstruction==null ) return;
//		if( func==null ) return;		//(NULL)���� �����
//		if( wParam==null ) return;		//(NULL)���� �����
//		if( lParam==null ) return;		//(NULL)���� �����

		StartCoroutine( Coroutine_( yieldInstruction, func, wParam, lParam ) );
	}

	//�ڷ�ƾ�� �����ϱ� ���� �Լ�
	public IEnumerator Coroutine_( YieldInstruction yieldInstruction, Action<object, object> func, object wParam=null, object lParam=null )
	{
		if( yieldInstruction==null ) yield break;
//		if( func==null ) return;		//(NULL)���� �����
//		if( wParam==null ) return;		//(NULL)���� �����
//		if( lParam==null ) return;		//(NULL)���� �����

		yield return yieldInstruction;

		if( func!=null )
		{
			func( wParam, lParam );
		}
	}

    public static ApplicationBehaviour This = null;

	/*
	[NonSerialized] public CGame Game = null;
	[NonSerialized] public CMessageQueue MessageQueue = null;
	[NonSerialized] public CPhase Phase = null;
	[NonSerialized] public CTime Time = null;
	*/

	protected virtual void Awake()
    {
        This = this as ApplicationBehaviour;

		/*
		Game = GetComponentInChildren(typeof(CGame)) as CGame;
		MessageQueue = GetComponentInChildren(typeof(CMessageQueue)) as CMessageQueue;
		Phase = GetComponentInChildren(typeof(CPhase)) as CPhase;
		Time = GetComponentInChildren(typeof(CTime)) as CTime;
		*/
	}
}