using UnityEngine;

public abstract class TransformBehaviour : GameObjectBehaviour
{
	protected Transform m_transform = null;

	protected override void Awake()
	{
		if( m_transform==null )
		{
			base.Awake();
			m_transform = transform;
		}
	}

	//트랜스폼을 얻기 위한 함수
	public Transform Transform()
	{
		return m_transform;
	}
}