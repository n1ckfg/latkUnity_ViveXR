using UnityEngine;
using Wave.Essence;
using Wave.Essence.ScenePerception.Sample;
using Wave.Native;

namespace Wave.Essence.ScenePerception.Sample {

    public class NewScenePerceptionManager : MonoBehaviour {

        private ScenePerceptionMeshFacade _scenePerceptionMeshFacade;
        [SerializeField] private SpatialAnchorHelper _spacialAnchorHelper;
        [SerializeField] private GameObject anchorPrefab, anchorDisplayPrefab;
        [SerializeField] private ScenePerceptionHelper _scenePerceptionHelper;
        [SerializeField] private Material GeneratedMeshMaterialTranslucent, GeneratedMeshMaterialWireframe;

        private void OnEnable() {
            if (_scenePerceptionHelper == null) {
                _scenePerceptionHelper = new ScenePerceptionHelper();
            }

            if (_scenePerceptionHelper != null) {
                _scenePerceptionHelper.OnEnable();
            }

            //_spacialAnchorHelper = new SpatialAnchorHelper(_scenePerceptionHelper.scenePerceptionManager, anchorPrefab);
            //if (_scenePerceptionHelper.isSceneComponentRunning) {
               //_spacialAnchorHelper.SetAnchorsShouldBeUpdated();
            //}
            _scenePerceptionMeshFacade = new ScenePerceptionMeshFacade(_scenePerceptionHelper, anchorDisplayPrefab, GeneratedMeshMaterialTranslucent, GeneratedMeshMaterialWireframe);
        }

        private void OnDisable() {
            if (_scenePerceptionHelper != null) {
                _scenePerceptionHelper.OnDisable();
            }
        }
        private void OnApplicationPause(bool pause) {
            if (!pause) {
                //_spacialAnchorHelper.SetAnchorsShouldBeUpdated(); //Anchors will have moved since the program was previously running - re-update during On Resume in case of a tracking map change
            }
        }

        private void Start() {
            // TEST
            ChangeSceneMeshTypeToCollider();
        }

        private void Update() {
			if (_scenePerceptionHelper.isSceneComponentRunning) {
				//Handle Scene Perception
				if (!_scenePerceptionHelper.isScenePerceptionStarted) {
					_scenePerceptionHelper.StartScenePerception();
				} else {
					_scenePerceptionHelper.ScenePerceptionGetState(); //Update state of scene perception every frame
					_scenePerceptionMeshFacade.UpdateScenePerceptionMesh();
				}

				//Handle Spatial Anchor
				//_spacialAnchorHelper.UpdateAnchorDictionary();
			}
		}

		public void ChangeSceneMeshTypeToVisual() {
			_scenePerceptionMeshFacade.ChangeSceneMeshType(WVR_SceneMeshType.WVR_SceneMeshType_VisualMesh);
		}

		public void ChangeSceneMeshTypeToCollider() {
			_scenePerceptionMeshFacade.ChangeSceneMeshType(WVR_SceneMeshType.WVR_SceneMeshType_ColliderMesh);
		}

	}

}
