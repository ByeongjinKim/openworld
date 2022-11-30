using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ApplicationBehaviourBase : TransformBehaviour
{
	protected virtual void Start()
	{
	}

	//어플리케이션이 시작되었는지 확인하기 위한 함수
	public static bool IsStartup()
	{
		if( This!=null )
		{
			return true;
		}

		return false;
	}

	//코루틴을 실행하기 위한 함수
	public void Coroutine( YieldInstruction yieldInstruction, Action<object, object> func, object wParam=null, object lParam=null )
	{
		if( yieldInstruction==null ) return;
//		if( func==null ) return;		//(NULL)값을 허용함
//		if( wParam==null ) return;		//(NULL)값을 허용함
//		if( lParam==null ) return;		//(NULL)값을 허용함

		StartCoroutine( Coroutine_( yieldInstruction, func, wParam, lParam ) );
	}

	//코루틴을 실행하기 위한 함수
	public IEnumerator Coroutine_( YieldInstruction yieldInstruction, Action<object, object> func, object wParam=null, object lParam=null )
	{
		if( yieldInstruction==null ) yield break;
//		if( func==null ) return;		//(NULL)값을 허용함
//		if( wParam==null ) return;		//(NULL)값을 허용함
//		if( lParam==null ) return;		//(NULL)값을 허용함

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