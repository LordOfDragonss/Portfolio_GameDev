using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VolumetricAudio;

public class RotatingPipe : MonoBehaviour
{
    [SerializeField] SoundManager rotatingSound;
    [SerializeField] VA_AudioSource rotatingsource;
    [SerializeField] bool constantRotation = false;
    [SerializeField] float rotationSpeed = 10;
    [SerializeField] float waitTime = 3;
    [SerializeField] float duration = 3;
    [SerializeField] AnimationCurve SmoothnessCurve;

    [SerializeField] Vector3[] rotationCheckpoints;
    int iteration = 0;
    int angle = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (!constantRotation)
        {
            transform.rotation = Quaternion.Euler(rotationCheckpoints[0]);
            StartCoroutine(RotateUsingCheckpoints(duration));

        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (rotatingsource != null)
        {
            if (rotatingSound.gameObject.activeInHierarchy)
            {

                if (IsRotating() && !rotatingSound.GetMusicIsPlaying())
                {
                    rotatingSound.SetAndPlayMusic(0);
                }
                if (!IsRotating())
                {
                    rotatingSound.StopMusic();
                }
            }
        }
    }

    IEnumerator RotateUsingCheckpoints(float duration)
    {

        while (true)
        {
            //Rotate
            yield return RotateObject(gameObject, rotationCheckpoints[0], duration);
            //Wait?
            yield return new WaitForSeconds(waitTime);
            //Rotates
            yield return RotateObject(gameObject, rotationCheckpoints[1], duration);
            //Wait?
            yield return new WaitForSeconds(waitTime);
        }

    }

    bool rotating = false;
    IEnumerator RotateObject(GameObject gameObjectToMove, Vector3 eulerAngles, float duration)
    {
        if (rotating)
        {
            yield break;
        }
        rotating = true;

        Quaternion newRot = Quaternion.Euler(eulerAngles);
        Quaternion currentRot = gameObjectToMove.transform.rotation;

        float counter = 0;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            float percent = Mathf.Clamp01(counter / duration);
            float curvePercent = SmoothnessCurve.Evaluate(percent);
            counter += Time.deltaTime;

            if (rotatingsource != null)
            {
                rotatingsource.BaseVolume = 1 - curvePercent;
            }

            gameObjectToMove.transform.rotation = Quaternion.LerpUnclamped(currentRot, newRot, curvePercent);
            yield return null;
        }
        rotating = false;
    }

    public bool IsRotating()
    {
        return rotating;
    }
}
