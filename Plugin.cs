using BepInEx;
using GorillaLocomotion;
using Oculus.Platform.Models;
using Photon.Pun;
using System;
using System.ComponentModel;
using System.Net;
using System.Text;
using UnityEngine;
using UnityEngine.XR;
using Utilla;

namespace DinosauceCars
{
    [Description("HauntedModMenu")]
    [ModdedGamemode]
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {

        bool inRoom;

        void Update()
        {
            if(inRoom || !PhotonNetwork.InRoom)
            {
                Rigidbody rb = Player.Instance.gameObject.GetComponent<Rigidbody>(); 
                Vector2 left;
                GameObject gameObject = GameObject.Find("gorillachestface");
                InputDevices.GetDeviceAtXRNode(XRNode.LeftHand).TryGetFeatureValue(UnityEngine.XR.CommonUsages.primary2DAxis, out left);
                Vector3 vector = (gameObject.transform.forward * left.y * 5) + (gameObject.transform.right * left.x * 5);
                bool moving = (vector.x > 0.1f || vector.x < -0.1f) || (vector.y > 0.1f || vector.y < -0.1f);

                if (moving)
                {
                    vector.y = rb.velocity.y;
                    rb.velocity = vector * Player.Instance.scale;
                }
            }
        }

        [ModdedGamemodeJoin]
        public void OnJoin(string gamemode)
        {
            inRoom = true;
        }

        [ModdedGamemodeLeave]
        public void OnLeave(string gamemode)
        {
            inRoom = false;
        }
    }
}
