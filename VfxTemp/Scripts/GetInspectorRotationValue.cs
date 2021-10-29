using UnityEngine;

namespace BlackJack.ProjectF.Client
{
	/// <summary>
	/// 获取Unity面板中的物体的旋转值
	/// </summary>
	public class GetInspectorRotationValue : MonoBehaviour
	{
		public GameObject m_ObjectGo;
		public bool m_UsePosition;
		public bool m_UseRotateX;
		public bool m_UseRotateY;
		public bool m_UseRotateZ;
		private void Update()
		{
			//位移模式
			Vector3 GoTransform = m_ObjectGo.transform.position;
			Vector3 thisTransform = this.transform.position;
			Vector3 finalTransform = m_UsePosition ? GoTransform : thisTransform;
			this.GetComponent<Transform>().position = finalTransform;
			
			//旋转模式
			//自我旋转
			float thisRotatex = this.transform.localEulerAngles.x;
			float thisRotatey = this.transform.localEulerAngles.y;
			float thisRotatez = this.transform.localEulerAngles.z;		
			//目标旋转
			float GoRotatex = m_ObjectGo.transform.localEulerAngles.x;
			float GoRotatey = m_ObjectGo.transform.localEulerAngles.y;
			float GoRotatez = m_ObjectGo.transform.localEulerAngles.z;
			
			//开关取值
			float finalRotatex = m_UseRotateX ? GoRotatex : thisRotatex;
			float finalRotatey = m_UseRotateY ? GoRotatey : thisRotatey;
			float finalRotatez = m_UseRotateZ ? GoRotatez : thisRotatez;
			
			this.GetComponent<Transform>().rotation = Quaternion.Euler(finalRotatex, finalRotatey, finalRotatez);
		
		}
	}
}

