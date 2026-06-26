using UnityEngine;
using FMODUnity;
using FMOD.Studio;
public class SimpleAudioController : MonoBehaviour
{
    [SerializeField] private EventReference soundEvent;
    [SerializeField] private EventReference collisionSoundEvent;
    [SerializeField] private EventReference triggerSoundEvent;

    private EventInstance instance;

    void Start()
    {
        if (!soundEvent.IsNull)
        {
            instance = RuntimeManager.CreateInstance(soundEvent);
            instance.start();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.relativeVelocity.magnitude > 2f)
        {
            if (!collisionSoundEvent.IsNull)
            {
                Vector3 contactPoint = collision.contacts[0].point;
                RuntimeManager.PlayOneShot(collisionSoundEvent, contactPoint);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!triggerSoundEvent.IsNull)
        {
            RuntimeManager.PlayOneShot(triggerSoundEvent, other.transform.position);
        }
    }

    void OnDestroy()
    {
        if (instance.isValid())
        {
            instance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            instance.release();
        }
    }


}
