using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosHandler : Singleton<PosHandler>
{
    bool isAtMix, isAtShop, isAtMelt;
    public Transform mixPos, shopPos, meltPos;
    public GameObject leftArrow, rightArrow, cam, character;
    public float duration = 1;
    Vector3 charCamDif;
    bool isMoving;

    public GameObject shop;

    private void Start()
    {
        charCamDif = character.transform.position - cam.transform.position;
        isAtMix = true;
    }

    public void GoRight()
    {
        if (isAtMix && !isMoving)
        {
            isMoving = true;
            StartCoroutine(MoveScreen(meltPos.position, true));
            isAtMelt = true;
            isAtMix = false;
            rightArrow.SetActive(false);
        }

        if (isAtShop && !isMoving)
        {
            isMoving = true;
            StartCoroutine(MoveScreen(mixPos.position, false));
            isAtMix = true;
            isAtShop = false;
            leftArrow.SetActive(true);
        }
    }
    
    public void GoLeft()
    {
        if (isAtMix && !isMoving)
        {
            isMoving = true;
            StartCoroutine(MoveScreen(shopPos.position, false));
            shop.SetActive(true);
            isAtShop = true;
            isAtMix = false;
            leftArrow.SetActive(false);
        }

        if (isAtMelt && !isMoving)
        {
            isMoving = true;
            StartCoroutine(MoveScreen(mixPos.position, true));
            isAtMix = true;
            isAtMelt = false;
            rightArrow.SetActive(true);
        }
    }

    IEnumerator MoveScreen(Vector3 targetPosition, bool moveChar)
    {
        float timeElapsed = 0;
        Vector3 startPosition = cam.transform.position;
        Vector3 charStartPosition = character.transform.position;
        while (timeElapsed < duration)
        {
            cam.transform.position = Vector3.Lerp(startPosition, targetPosition, timeElapsed / duration);
            if (moveChar)
            {
                character.transform.position = Vector3.Lerp(charStartPosition, targetPosition + charCamDif, timeElapsed / duration);
            }
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        cam.transform.position = targetPosition;
        if (isAtMix)
        {
            shop.SetActive(false);
        }
        isMoving = false;
    }
}
