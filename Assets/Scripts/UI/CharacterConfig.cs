using New;
using UnityEngine;
using UnityEngine.UI;



namespace UI
{
    [CreateAssetMenu(fileName = "CharacterConfig", menuName = "ScriptableObjects/CharacterConfig")]
    public class CharacterConfig : ScriptableObject
    {
        [SerializeField] public string characterName;
        [SerializeField] public string description;
        [SerializeField] public int cost;
        [SerializeField] public GameObject modelPrefab;
        [SerializeField] public GameObject lobbyPrefab;
        [SerializeField] public int MaxHp;
        [SerializeField] public int radiusAttack;
        [SerializeField] public float speed;
        public bool isBought;
    }
}