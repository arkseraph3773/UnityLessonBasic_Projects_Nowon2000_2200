using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    private Color originColor;
    public Color buildAvailableColor;
    public Color buildNotAvailableColor;

    Renderer rend;
    BoxCollider col;

    private void Awake()
    {
        rend = GetComponent<Renderer>();
        col = GetComponent<BoxCollider>();
        originColor = rend.material.color;
    }
    private void OnMouseEnter()
    {
        rend.material.color = buildAvailableColor;
        if (TowerViewPresenter.instance.isSelceted)
        {
            Transform previewTransform = TowerViewPresenter.instance.GetTowerPreviewObjectTransform();
            previewTransform.gameObject.SetActive(true);
            previewTransform.position = transform.position + new Vector3(0, col.size.y / 2, 0);
        }
    }
    private void OnMouseExit()
    {
        rend.material.color = originColor;
        /*if (TowerViewPresenter.instance.isSelceted)
        {
            Transform previewTransform = TowerViewPresenter.instance.GetTowerPreviewObjectTransform();
            previewTransform.gameObject.SetActive(false);
            previewTransform.position = transform.position;
        }*/
    }
    private void OnMouseDown()
    {
        if (TowerViewPresenter.instance.isSelceted)
        {
            Transform previewTransform = TowerViewPresenter.instance.GetTowerPreviewObjectTransform();
            ObjectPool.SpawnFromPool(previewTransform.GetComponent<TowerPreview>().towerName, previewTransform.position);
        }
    }
}
