using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyNamespace
{
    public class ItemGrid : MonoBehaviour
    {
        const int tileSizeWidth = 64;
        const int tileSizeHeight = 64;

        InventoryItem[,] inventoryItemSlot;

        RectTransform rectTransform;

        Vector2 positionOnTheGrid = new Vector2();
        Vector2Int tileGridPosition = new Vector2Int();

        public int gridSizeWidth = 12;
        public int gridSizeHeight = 5;

        public GameObject inventoryItemPrefab;

        private void Start()
        {
            rectTransform = GetComponent<RectTransform>();
            Init(12, 5);

            InventoryItem inventoryItem = Instantiate(inventoryItemPrefab).GetComponent<InventoryItem>();
            PlaceItem(inventoryItem, 3, 2);
        }

        public void Init(int width, int height)
        {
            inventoryItemSlot = new InventoryItem[width, height];
            Vector2 size = new Vector2(width * tileSizeWidth, height * tileSizeHeight);
            rectTransform.sizeDelta = size;
        }

        public Vector2Int GetTileGridPosition(Vector2 mousePosition)
        {
            positionOnTheGrid.x = mousePosition.x - rectTransform.position.x;
            positionOnTheGrid.y = rectTransform.position.y - mousePosition.y;

            tileGridPosition.x = (int)(positionOnTheGrid.x / tileSizeWidth);
            tileGridPosition.y = (int)(positionOnTheGrid.y / tileSizeHeight);

            return tileGridPosition;
        }

        public void PlaceItem(InventoryItem inventoryItem, int posX, int posY)
        {
            RectTransform rectTransform = inventoryItem.GetComponent<RectTransform>();
            rectTransform.SetParent(this.rectTransform);
            inventoryItemSlot[posX, posY] = inventoryItem;

            Vector2 position = new Vector2();
            position.x = posX * tileSizeWidth + tileSizeWidth / 2;
            position.y = -(posY * tileSizeHeight + tileSizeHeight / 2);

            rectTransform.localPosition = position;
        }
    }
}
