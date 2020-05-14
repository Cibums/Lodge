using UnityEngine;

public class MachineBehaviour : MonoBehaviour
{
    public GameObject destroyedPrefab;
    public AudioClip destroySound;
    public int dropItemID;
    public int dropAmount;
    public bool indestructable = false;
    public bool rotatable = false;
    [HideInInspector] public bool isQuitting = false;
    [HideInInspector] public bool ignoreDestruction = false;

    public void Rotate()
    {
        transform.Rotate(new Vector3(0, 90, 0));
    }

    public virtual void OnDestroy()
    {
        if (isQuitting || ignoreDestruction)
        {
            return;
        }

        if (destroyedPrefab != null)
        {
            GameObject go = Instantiate(destroyedPrefab, transform.position, Quaternion.identity);

            foreach (Transform child in go.transform)
            {
                if (child.gameObject.GetComponent<Rigidbody>())
                {
                    child.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f), Random.Range(-2f, 2f)), ForceMode.Impulse);
                }
            }
        }

        for (int i = 0; i < dropAmount; i++)
        {
            GameController.gameController.inventory.Add(dropItemID);
        }

        AudioController.audioController.PlaySound(destroySound);
        Instantiate(GameController.gameController.destroyParticles, transform.position, Quaternion.identity);
    }

    private void OnApplicationQuit()
    {
        isQuitting = true;
    }
}