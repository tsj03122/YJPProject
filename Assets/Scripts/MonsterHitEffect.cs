using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHitEffect : MonoBehaviour
{
    public GameObject monsterHitEffect;

    public void EffectOn()
    {
        monsterHitEffect.transform.position = this.transform.position;
        monsterHitEffect.SetActive(true);
    }

    public void EffectOff()
    {
        monsterHitEffect.SetActive(false);
    }


}
