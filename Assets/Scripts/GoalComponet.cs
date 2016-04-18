using Assets.Scripts.manager;
using Assets.Scripts.message.custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyMessenger;
using UnityEngine;

namespace Assets.Scripts
{
    public class GoalComponet : MonoBehaviour
    {
        public void OnCollisionEnter(Collision collision)
        {
            Initialiser.Instance.GetService<IMessageBus>().Publish(new GoalReachedMessage(this));
        }
    }
}
