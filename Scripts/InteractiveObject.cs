using UnityEngine;
using static UnityEngine.Random;


namespace ArtomStatsenko
{
    public abstract class InteractiveObject : MonoBehaviour, IExecute
    {
        protected Color _color;

        private bool _isInteractable;

        protected bool IsInteractable
        {
            get { return _isInteractable; }
            private set
            {
                _isInteractable = value;
                GetComponent<Renderer>().enabled = _isInteractable;
                GetComponent<Collider>().enabled = _isInteractable;
                GetComponentInChildren<Light>().enabled = _isInteractable;
            }
        }


        private void Start()
        {
            IsInteractable = true;
            _color = ColorHSV();

            if (TryGetComponent(out Renderer renderer))
            {
                renderer.material.color = _color;
            }
        }

        public abstract void Execute();        
        protected abstract void Interaction();

        private void OnTriggerEnter(Collider other)
        {
            if (!IsInteractable || !other.CompareTag("Player"))
            {
                return;
            }

            Interaction();
            IsInteractable = false;
        }
    }
}