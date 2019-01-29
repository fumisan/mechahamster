﻿// Copyright 2017 Google Inc. All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using UnityEngine;
using System.Collections;
namespace Hamster.MapObjects
{

    // General base-class for objects on the map.
    public class StartPosition : MapObject
    {

        static Vector3 kPlayerStartOffset = new Vector3(0, 2, 0);

        // Populated by the inspector:
        // Prefab to use when spawning a new player avatar at level start.
        public GameObject playerPrefab;

        public void UpdatePosOri(Transform xform)
        {
            xform.position = transform.position + kPlayerStartOffset;
            xform.rotation = Quaternion.identity;
        }
        public GameObject SpawnPlayer()
        {
            CommonData.mainGame.player = CommonData.mainGame.SpawnPlayer(); ;
            if (CommonData.mainGame.player != null)
            {
                UpdatePosOri(CommonData.mainGame.player.transform);
            }
            return CommonData.mainGame.player;
        }
        public void FixedUpdate()
        {
            if (CommonData.mainGame.isGameRunning())
            {
                if (CommonData.mainGame.player == null)
                {
                    GameObject player = null;
                    //if (CommonData.networkManager != null)
                    //{
                    //    player = MainGame.NetworkSpawnPlayer(CommonData.customNetwork.networkManager.toServerConnection);
                    //}
                    //else
                    {
                        player = SpawnPlayer();
                    }
                }
            }
            else
            {
                if (CommonData.mainGame.player != null)
                {
                    Reset();
                }
            }
        }

        public override void Reset()
        {
            if (CommonData.mainGame.player != null)
            {
                CommonData.mainGame.DestroyPlayer();
            }
        }
    }
}
