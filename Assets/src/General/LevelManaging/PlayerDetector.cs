using UnityEngine;

public class PlayerDetector : MonoBehaviour {
    public Linkable[] linkablesToActivate;

    public bool clinging;

    private void Start() {
        Destroy(GetComponent<SpriteRenderer>());
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            if (!clinging) {
                clinging = true;
                foreach (Linkable linkable in linkablesToActivate) {
                    linkable.Activate();
                }
            }
        }
    }
    
    
}
