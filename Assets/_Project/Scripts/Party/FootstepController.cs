using System;
using System.Collections;
using System.Collections.Generic;
using DarkTonic.MasterAudio;
using Descending.Core;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Descending.Party
{
    public class FootstepController : MonoBehaviour
    {
        [SerializeField] private FirstPersonController _fpc = null;
        [SerializeField, SoundGroupAttribute] private List<string> _stoneStepSounds = null;
        [SerializeField, SoundGroupAttribute] private List<string> _grassStepSounds = null;
        [SerializeField] private float _stepSpeed = 0.5f;
        [SerializeField] private float _sprintSpeed = 0.3f;
        [SerializeField] private float _stepVolume = 1f;

        private bool _handleFootsteps = true;
        private float _footstepTimer = 0f;

        private void Update()
        {
            if (_handleFootsteps) HandleFootsteps();
        }

        private void HandleFootsteps()
        {
            if (_fpc.Grounded == false) return;
            if (_fpc.IsMoving == false) return;

            _footstepTimer -= Time.deltaTime;

            if (_footstepTimer <= 0)
            {
                if (Physics.Raycast(Camera.main.transform.position, Vector3.down, out RaycastHit hit, 4f))
                {
                    GroundTag groundTag = hit.collider.GetComponent<GroundTag>();

                    if (groundTag != null)
                    {
                        if (groundTag.Tag == GroundTags.Stone)
                        {
                            string sound = _stoneStepSounds[(int) Random.Range(0, _stoneStepSounds.Count)];
                            MasterAudio.PlaySound3DAtVector3(sound, transform.position, _stepVolume, 1f);
                        }
                        
                        _footstepTimer = GetStepSpeed();
                        return;
                    }

                    Terrain terrain = hit.collider.GetComponent<Terrain>();

                    if (terrain != null)
                    {
                        string layerName = GetLayerName(hit.point, terrain);
                        
                        if (layerName == "Grass Light" || layerName == "Grass Dark")
                        {
                            string sound = _grassStepSounds[(int) Random.Range(0, _grassStepSounds.Count)];
                            MasterAudio.PlaySound3DAtVector3(sound, transform.position, _stepVolume, 1f);
                        }
                        else if (layerName == "Stone Light" || layerName == "Stone Dark")
                        {
                            string sound = _stoneStepSounds[(int) Random.Range(0, _stoneStepSounds.Count)];
                            MasterAudio.PlaySound3DAtVector3(sound, transform.position, _stepVolume, 1f);
                        }

                        _footstepTimer = GetStepSpeed();
                        //return;
                    }
                }
            }
        }

        private float GetStepSpeed()
        {
            float stepSpeed = _stepSpeed;
            if (_fpc.IsSprinting) stepSpeed = _sprintSpeed;

            return stepSpeed;
        }
        
        private float[] GetTextureMix(Vector3 position, Terrain terrain)
        {
            int mapX = Mathf.RoundToInt((position.x - terrain.transform.position.x) / terrain.terrainData.size.x * terrain.terrainData.alphamapWidth);
            int mapZ = Mathf.RoundToInt((position.z - terrain.transform.position.z) / terrain.terrainData.size.z * terrain.terrainData.alphamapHeight);
            float[,,] splatMapData = terrain.terrainData.GetAlphamaps(mapX, mapZ, 1, 1);
            float[] cellMix = new float[splatMapData.GetUpperBound(2) + 1];

            for (int i = 0; i < cellMix.Length; i++)
            {
                cellMix[i] = splatMapData[0, 0, i];
            }

            return cellMix;
        }

        private string GetLayerName(Vector3 position, Terrain terrain)
        {
            float[] cellMix = GetTextureMix(position, terrain);
            float strongest = 0f;
            int maxIndex = 0;
            for (int i = 0; i < cellMix.Length; i++)
            {
                if (cellMix[i] > strongest)
                {
                    maxIndex = i;
                    strongest = cellMix[i];
                }
            }

            return terrain.terrainData.terrainLayers[maxIndex].name;
        }
    }
}