using UnityEngine;
using UnityEngine.UI;

namespace CommonEngine.UI.Components
{
	[RequireComponent(typeof(GridLayoutGroup))]
	internal class GridSizeScaler : MonoBehaviour
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
				return;

			int actualColumns = Mathf.Min(totalItems, MaxColumns);
			float rectWidth = _selfRectTransform.rect.width;
			_selfRectTransform.sizeDelta = Vector2.zero;

			// Reduce number of columns to prevet overflow
			while (actualColumns > 1)
			{
				float testCellWidth = rectWidth / actualColumns;
				if (testCellWidth >= MinWidth)
				{
					break;
				}

				actualColumns--;
			}

			float cellWidth = Mathf.Max(rectWidth / actualColumns, MinWidth);
			float cellHeight = Mathf.Min(cellWidth, _selfRectTransform.rect.height);

			_selfGridLayoutGroup.cellSize = new Vector2(cellWidth, cellHeight);
			_selfGridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
			_selfGridLayoutGroup.constraintCount = actualColumns;
		}





	}
}
