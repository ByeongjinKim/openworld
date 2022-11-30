using UnityEngine;

public abstract class RectTransformBehaviour : GameObjectBehaviour
{
	protected RectTransform m_transform = null;

	protected override void Awake()
	{
		if( m_transform==null )
		{
			base.Awake();
			m_transform = transform as RectTransform;
		}
	}

	//트랜스폼을 얻기 위한 함수
	public RectTransform Transform()
	{
		return m_transform;
	}
}