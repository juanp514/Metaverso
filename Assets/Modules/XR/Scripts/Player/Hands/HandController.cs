using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

namespace Xennial.XR.Player
{
    public class HandController : MonoBehaviour
    {
        private const string POINT_ANIMATION_KEY = "Point";
        private const string GRAB_ANIMATION_KEY = "Grab";

        [SerializeField]
        private Animator _animator;

        [SerializeField]
        private InputActionReference _selectAction;

        [SerializeField]
        private InputActionReference _triggerAction;

        private XRPokeInteractor _pokeInteractor;
        private XRDirectInteractor _directInteractor;

        private void Start()
        {
            _pokeInteractor = transform.parent.GetComponentInChildren<XRPokeInteractor>();
            _directInteractor = transform.parent.GetComponentInChildren<XRDirectInteractor>();

            _selectAction.action.performed += Grab;
            _selectAction.action.canceled += Drop;

            _triggerAction.action.performed += EnablePoke;
            _triggerAction.action.canceled += DisablePoke;
        }

        private void Grab(InputAction.CallbackContext context)
        {
            _animator.SetFloat(GRAB_ANIMATION_KEY, context.action.ReadValue<float>());
        }

        private void Drop(InputAction.CallbackContext context)
        {
            _animator.SetFloat(GRAB_ANIMATION_KEY, 0);
        }

        private void EnablePoke(InputAction.CallbackContext obj)
        {
            _animator.SetBool(POINT_ANIMATION_KEY, true);
            _pokeInteractor.enabled = true;
            _directInteractor.enabled = false;
        }

        private void DisablePoke(InputAction.CallbackContext obj)
        {
            _animator.SetBool(POINT_ANIMATION_KEY, false);
            _pokeInteractor.enabled = false;
            _directInteractor.enabled = true;
        }
    }
}