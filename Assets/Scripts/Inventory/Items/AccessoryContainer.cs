using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessoryContainer
{
    // TODO.
    // 1. 악세서리 4개
    // 2. 악세서리 데이터 입출력 가능
    // 3. 인벤토리에서 악세서리 받기 가능
    public Dictionary<AccessoryType, AccessoryData> CurrentParts {get; private set;}

    public void GetAccessoryData(AccessoryData accessory)
    {
        // TODO.
        // 1. 악세서리의 타입을 확인
        // 2. 이미 가지고 있다면 -> 그냥 ret
        // 3. 없으면 -> 집어넣기
        // * ret값을 bool로 잡아야 할 수도 있다.
    }
}
