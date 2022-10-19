using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class TriggerZone : MonoBehaviour
{
    [SerializeField] private Collider _colliderZone;

    private CharacterController _parent;
    private CharacterController _currentCharacter;
    private List<CharacterController> _charactersInZone = new List<CharacterController>();

    public List<CharacterController> CharactersInZone => _charactersInZone;

    private void Awake()
    {
        _parent = GetComponentInParent<CharacterController>();
        _colliderZone.isTrigger = true;
    }

    public CharacterController GetNearCharacter()
    {
        CharacterController characterController = _currentCharacter;

        var dist = Vector3.Distance(_currentCharacter.transform.position, _parent.transform.position);

        foreach (var character in _charactersInZone)
        {
            var newDist = Vector3.Distance(character.transform.position, _parent.transform.position);
            if (dist > newDist)
            {
                characterController = character;
                dist = newDist;
            }
        }

        return characterController;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<CharacterController>(out var character))
        {
            if (character != _parent)
            {
                _charactersInZone.Add(character);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent<CharacterController>(out var character))
        {
            if (character != _parent)
            {
                _currentCharacter = character;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<CharacterController>(out var character))
        {
            if (character != _parent)
            {
                _charactersInZone.Remove(character);
            }
        }
    }
}
