using BrassSparrow.Scripts.Doll;
using Doozy.Engine.UI;
using Maru.Scripts.MUI;
using UnityEngine;
using UnityEngine.UI.ProceduralImage;

namespace BrassSparrow.Scripts.UI {
    public class PartSelector : DoozyBehavior {
        public float meshDistance;
        public Color unselectedColor;
        public Color selectedColor;
        
        public DollPart DollPart;
        public GameObject masterCanvas;
        public float meshScale;
        
        private GameObject mesh;
        private ProceduralImage box;
        private UIMesh uiMesh;

        protected override void Awake() {
            base.Awake();
            var evtKey = DoozyEvents.DoozyEventKey(PartSelectedEvent.Prefix, this);
            
            var doozyButton = GetComponent<UIButton>();
            doozyButton.OnClick.OnTrigger.GameEvents.Add(evtKey);
            box = GetComponent<ProceduralImage>();
        }

        public void SetDollPart(DollPart part) {
            DollPart = part;
            if (mesh != null) {
                Destroy(mesh);
            }
            mesh = Instantiate(part.Go, transform);
        }

        protected override void Start() {
            base.Start();
            
            // Configure mesh
            mesh.transform.parent = transform;
            mesh.layer = gameObject.layer;
            mesh.transform.localRotation = new Quaternion(0, 180, 0, 0);
            
            uiMesh = mesh.AddComponent<UIMesh>();
            uiMesh.canvas = masterCanvas;
            uiMesh.unscaledDistance = meshDistance;
            mesh.transform.localScale = new Vector3(meshScale, meshScale, meshScale);
        }

        public void SetSelected(bool selected) {
            if (selected) {
                box.color = selectedColor;
            } else {
                box.color = unselectedColor;
            }
        }
    }
}