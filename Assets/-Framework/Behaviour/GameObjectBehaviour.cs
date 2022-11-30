using UnityEngine;

public abstract class GameObjectBehaviour : MonoBehaviour
{
	protected GameObject m_gameObject = null;

	protected virtual void Awake()
	{
		if( m_gameObject==null )
		{
			m_gameObject = gameObject;
		}
	}

	//게임 오브젝트를 얻기 위한 함수
	public GameObject _GameObject()
	{
		if( m_gameObject==null )
		{
			Awake();
		}

		return m_gameObject;
	}
}