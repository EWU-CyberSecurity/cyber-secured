using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Topics
{
    /// <summary>
    /// This class represents a single question in a topic
    /// </summary>
    class Question : TopicItem 
    {
        public string questionText { get; set; }
        public string questionType { get; set; }
        public string questionID { get; set; }
        private Answer answer { get; set; }

        public override void startItem()
        {
            // Here we create the question box and put the text in it.
            // And then call answer.displayAnswer()
        }
    }
}
