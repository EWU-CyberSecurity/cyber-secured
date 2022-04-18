using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Topics
{
    public class Dialogue : TopicItem
    {
        private string[] sentences;

        public Dialogue() { }

        public Dialogue(string googleSheetString)
        {
            this.sentences = googleSheetString.Split(new string[] { "<>" }, StringSplitOptions.None);
        }

        public override void startItem()
        {
            // Activate the dialogue box and put the sentences in it.
            DialogueTrigger trigger = GameObject.Find("dlg_custom").GetComponent<DialogueTrigger>();
            trigger.dialogue.sentences = sentences;
            trigger.TriggerDialogue();
        }

        public string[] getSentences()
        {
            return sentences;
        }

        public void setSentences(string[] s)
        {
            this.sentences = s;
        }
    }
}