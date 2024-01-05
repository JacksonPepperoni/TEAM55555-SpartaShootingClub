using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    [SerializeField] private ItemData itemData;

    public ItemData ItemData => itemData;
}
