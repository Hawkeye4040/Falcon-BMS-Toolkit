using System;
using System.Collections.Generic;

namespace Falcon.Radio.Engine
{
    public interface ITriggerEvent
    {
        #region Properties

        string Name { get; }

        string Type { get; }

        string Comment { get; }

        string Value { get; }

        #endregion

        #region Methods

        void Run();

        #endregion
    }

    public abstract class Trigger : ITriggerEvent
    {
        #region Properties

        public List<ITriggerEvent> TriggerEvents { get; protected set; }

        public string Name { get; set; }

        public string Value { get; set; }

        public string Comment { get; set; }

        public string Type { get; set; }

        #endregion

        #region Constructors

        protected Trigger(string name)
        {
            Name = name;
            Type = GetType().Name;
        }

        #endregion

        #region Methods

        public void Add(ITriggerEvent triggerEvent)
        {
            TriggerEvents.Add(triggerEvent);
        }

        public bool Remove(ITriggerEvent triggerEvent)
        {
            return TriggerEvents.Remove(triggerEvent);
        }

        public abstract void Run();

        #endregion
    }

    public sealed class Phrase : Trigger
    {
        #region Constructors

        public Phrase(string name, string value)
            : base(name)
        {
            Value = value;
        }

        public Phrase(string name, string value, string comment)
            : base(name)
        {
            Value = value;
            Comment = comment;
        }

        #endregion

        #region Methods

        public override void Run()
        {
            foreach (ITriggerEvent triggerEvent in TriggerEvents) triggerEvent.Run();
        }

        #endregion
    }

    public class Logic : Trigger
    {
        #region Constructors

        public Logic(string name, string value)
            : base(name)
        {
            Value = value;
        }

        #endregion

        #region Methods

        public override void Run()
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    public sealed class TriggerCategory
    {
        #region Properties

        public string Name { get; set; }

        public List<Trigger> Triggers { get; set; }

        #endregion
    }

    public sealed class PhraseGroup
    {
        #region Properties

        public string Name { get; set; }

        public string Type { get; }

        public string Comment { get; }

        public List<Phrase> Phrases { get; }

        #endregion

        #region Constructors

        public PhraseGroup()
        {
            Phrases = new List<Phrase>();
            Comment = string.Empty;
        }

        public PhraseGroup(IEnumerable<Phrase> phrases)
        {
            Phrases = new List<Phrase>();
            Phrases.AddRange(phrases);
            Comment = string.Empty;
        }

        #endregion
    }
}