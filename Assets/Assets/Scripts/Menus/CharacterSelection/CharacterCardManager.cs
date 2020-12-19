using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCardManager : MonoBehaviour
{
    [SerializeField] List<CharacterCardGenerator> characterCards;

    [SerializeField] RenderTexture[] videoFeed;
}
