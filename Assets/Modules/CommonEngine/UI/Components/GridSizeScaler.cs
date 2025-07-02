using UnityEngine;
using UnityEngine.UI;

namespace CommonEngine.UI.Components
{
	[RequireComponent(typeof(GridLayoutGroup))]
	public class GridSizeScaler : MonoBehaviour
	{
		public int MaxColumns = 4;
		public float MinWidth = 100;

		[SerializeField]
		private GridLayoutGroup _selfGridLayoutGroup;
		[SerializeField]
		private RectTransform _selfRectTransform;


		public void UpdateCellSize(int totalItems)
		{
			if (MaxColumns <= 0 || totalItems <= 0)
			{
				return;
			}

			_selfGridLayoutGroup.GetComponent<RectTransform>().sizeDelta = Vector2.zero; 
			int actualColumns = Mathf.Min(totalItems, MaxColumns);
			float rectWidth = _selfRectTransform.rect.width;
			float cellWidth = Mathf.Max(rectWidth / actualColumns, MinWidth);

			while (cellWidth * actualColumns > rectWidth)
			{
				actualColumns--;
			}

			cellWidth = Mathf.Max(rectWidth / actualColumns, MinWidth);
			float cellHeight = Mathf.Min(cellWidth, _selfRectTransform.rect.height);

			_selfGridLayoutGroup.cellSize = new Vector2(cellWidth, cellHeight);
			_selfGridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
			_selfGridLayoutGroup.constraintCount = actualColumns;

		}



	}
}
