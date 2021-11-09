using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Topics
{
    class Dialogue : TopicItem
    {
        public string[] sentences { get; set; }

        public void AddDialogue(string Dialogue)
        {
            string[] result = Dialogue.Split('.');
            this.sentences = result;
        }

        public override void startItem()
        {
            // Activate the dialogue box and put the sentences in it.
        }
    }
}