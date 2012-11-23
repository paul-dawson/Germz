using UnityEngine;

class Collectible : MonoBehaviour  
{

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Player.AddScore(1);
            Destroy(this.gameObject);
        }
    }

}
