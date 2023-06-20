using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveDetector : MonoBehaviour
{
    public bool isDanger;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "ShakeNeedle")
        {
            if (!isDanger)
            {
                MixMaster.Instance.SetIsInGoodZone();
            }
            else
            {
                MixMaster.Instance.SetIsInBadZone();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "ShakeNeedle")
        {
            if (!isDanger)
            {
                MixMaster.Instance.SetOutOfGoodZone();
            }
            else
            {
                MixMaster.Instance.SetOutOfBadZone();
            }
        }
    }
}
