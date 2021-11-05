using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Topics
{
    /// <summary>
    /// This class is the main container for an entire topic,
    /// which consists of dialogue, and questions. Dialogue and
    /// questions are subclasses of TopicItem
    /// </summary>
    class Topic
    {
        private List<TopicItem> items = new List<TopicItem>();
    }
}
