using UnityEngine;

public class ParticleInfluenza : MonoBehaviour
{
    [SerializeField]
    private Disease prefab;

    void OnParticleCollision(GameObject other)
    {
        Cell cell = other.GetComponent<Cell>();

        if (cell != null)
            cell.AddDisease(prefab);
    }
}