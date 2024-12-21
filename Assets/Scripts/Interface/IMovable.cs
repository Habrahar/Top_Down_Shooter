using UnityEngine;
public interface IMovable
{
    Transform player {get; set;}
    void Follow(Vector3 position);

}
