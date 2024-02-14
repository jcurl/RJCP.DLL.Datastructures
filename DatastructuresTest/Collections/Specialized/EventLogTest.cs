namespace RJCP.Core.Collections.Specialized
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using NUnit.Framework;

    [TestFixture]
    public class EventLogTest
    {
        private enum EventSeverity
        {
            Critical,
            Error,
            Warning,
            Information,
            Verbose,
        }

        private class EventData : IComparable<EventData>
        {
            public static EventEntry<EventData> Get(EventSeverity severity, string description)
            {
                EventData data = new(severity, description);
                return new EventEntry<EventData>(data);
            }

            private EventData(EventSeverity severity, string description)
            {
                Severity = severity;
                Description = description;
            }

            public EventSeverity Severity { get; }

            public string Description { get; }

            public int CompareTo(EventData other)
            {
                if (other is null) return 1;
                if (Severity < other.Severity) return -1;
                if (Severity > other.Severity) return 1;
                return 0;
            }
        }

        private class NotifyAction
        {
            public NotifyAction(NotifyCollectionChangedEventArgs args)
            {
                Action = args.Action;
                OldStartingIndex = args.OldStartingIndex;
                OldItems = args.OldItems;
                NewStartingIndex = args.NewStartingIndex;
                NewItems = args.NewItems;
            }

            public NotifyCollectionChangedAction Action { get; }

            public int OldStartingIndex { get; }

            public IList OldItems { get; }

            public int NewStartingIndex { get; }

            public IList NewItems { get; }
        }

        public enum TestMode
        {
            Add,
            InsertAtEnd
        }

        [Test]
        public void Default()
        {
            EventLog<EventData> log = new();
            Assert.That(log, Is.Empty);
            Assert.That(log.IsReadOnly, Is.False);
        }

        [TestCase(TestMode.Add)]
        [TestCase(TestMode.InsertAtEnd)]
        public void AddEvent(TestMode mode)
        {
            List<NotifyAction> notify = new();
            EventLog<EventData> log = new();
            log.CollectionChanged += (s, e) => {
                notify.Add(new NotifyAction(e));
            };
            DateTime start = DateTime.Now.ToLocalTime();
            switch (mode) {
            case TestMode.Add:
                log.Add(EventData.Get(EventSeverity.Warning, "Warning"));
                break;
            case TestMode.InsertAtEnd:
                log.Insert(0, EventData.Get(EventSeverity.Warning, "Warning"));
                break;
            default:
                Assert.Fail("Invalid test case");
                break;
            }

            Assert.That(log, Has.Count.EqualTo(1));
            Assert.That(log[0].Identifier.Description, Is.EqualTo("Warning"));
            Assert.That(log[0].Identifier.Severity, Is.EqualTo(EventSeverity.Warning));
            Assert.That(log[0].TimeStamp, Is.GreaterThanOrEqualTo(start));

            Assert.That(notify, Has.Count.EqualTo(1));
            Assert.That(notify[0].Action, Is.EqualTo(NotifyCollectionChangedAction.Add));
            Assert.That(notify[0].OldStartingIndex, Is.EqualTo(-1));
            Assert.That(notify[0].NewStartingIndex, Is.EqualTo(0));

            Assert.That(log.Max.Identifier.Severity, Is.EqualTo(EventSeverity.Warning));
        }

        [TestCase(TestMode.Add)]
        [TestCase(TestMode.InsertAtEnd)]
        public void AddEvents(TestMode mode)
        {
            List<NotifyAction> notify = new();
            EventLog<EventData> log = new();
            log.CollectionChanged += (s, e) => {
                notify.Add(new NotifyAction(e));
            };
            DateTime start = DateTime.Now.ToLocalTime();
            switch (mode) {
            case TestMode.Add:
                log.Add(EventData.Get(EventSeverity.Warning, "Warning"));
                log.Add(EventData.Get(EventSeverity.Information, "Info"));
                break;
            case TestMode.InsertAtEnd:
                log.Insert(0, EventData.Get(EventSeverity.Warning, "Warning"));
                log.Insert(1, EventData.Get(EventSeverity.Information, "Info"));
                break;
            default:
                Assert.Fail("Invalid test case");
                break;
            }

            Assert.That(log, Has.Count.EqualTo(2));
            Assert.That(log[0].Identifier.Description, Is.EqualTo("Warning"));
            Assert.That(log[0].Identifier.Severity, Is.EqualTo(EventSeverity.Warning));
            Assert.That(log[0].TimeStamp, Is.GreaterThanOrEqualTo(start));

            Assert.That(log[1].Identifier.Description, Is.EqualTo("Info"));
            Assert.That(log[1].Identifier.Severity, Is.EqualTo(EventSeverity.Information));
            Assert.That(log[1].TimeStamp, Is.GreaterThanOrEqualTo(log[0].TimeStamp));

            Assert.That(notify, Has.Count.EqualTo(2));
            Assert.That(notify[0].Action, Is.EqualTo(NotifyCollectionChangedAction.Add));
            Assert.That(notify[0].OldStartingIndex, Is.EqualTo(-1));
            Assert.That(notify[0].NewStartingIndex, Is.EqualTo(0));
            Assert.That(notify[0].OldItems, Is.Null);
            Assert.That(notify[0].NewItems, Has.Count.EqualTo(1));
            Assert.That(notify[1].Action, Is.EqualTo(NotifyCollectionChangedAction.Add));
            Assert.That(notify[1].OldStartingIndex, Is.EqualTo(-1));
            Assert.That(notify[1].NewStartingIndex, Is.EqualTo(1));
            Assert.That(notify[1].OldItems, Is.Null);
            Assert.That(notify[1].NewItems, Has.Count.EqualTo(1));

            Assert.That(log.Max.Identifier.Severity, Is.EqualTo(EventSeverity.Warning));
        }

        [TestCase(TestMode.Add)]
        [TestCase(TestMode.InsertAtEnd)]
        public void AddEventsSameSeverity(TestMode mode)
        {
            List<NotifyAction> notify = new();
            EventLog<EventData> log = new();
            log.CollectionChanged += (s, e) => {
                notify.Add(new NotifyAction(e));
            };
            DateTime start = DateTime.Now.ToLocalTime();
            switch (mode) {
            case TestMode.Add:
                log.Add(EventData.Get(EventSeverity.Warning, "Warning 1"));
                log.Add(EventData.Get(EventSeverity.Warning, "Warning 2"));
                break;
            case TestMode.InsertAtEnd:
                log.Insert(0, EventData.Get(EventSeverity.Warning, "Warning 1"));
                log.Insert(1, EventData.Get(EventSeverity.Warning, "Warning 2"));
                break;
            default:
                Assert.Fail("Invalid test case");
                break;
            }

            Assert.That(log, Has.Count.EqualTo(2));
            Assert.That(log[0].Identifier.Description, Is.EqualTo("Warning 1"));
            Assert.That(log[0].Identifier.Severity, Is.EqualTo(EventSeverity.Warning));
            Assert.That(log[0].TimeStamp, Is.GreaterThanOrEqualTo(start));

            Assert.That(log[1].Identifier.Description, Is.EqualTo("Warning 2"));
            Assert.That(log[1].Identifier.Severity, Is.EqualTo(EventSeverity.Warning));
            Assert.That(log[1].TimeStamp, Is.GreaterThanOrEqualTo(log[0].TimeStamp));

            Assert.That(notify, Has.Count.EqualTo(2));
            Assert.That(notify[0].Action, Is.EqualTo(NotifyCollectionChangedAction.Add));
            Assert.That(notify[0].OldStartingIndex, Is.EqualTo(-1));
            Assert.That(notify[0].NewStartingIndex, Is.EqualTo(0));
            Assert.That(notify[0].OldItems, Is.Null);
            Assert.That(notify[0].NewItems, Has.Count.EqualTo(1));
            Assert.That(notify[1].Action, Is.EqualTo(NotifyCollectionChangedAction.Add));
            Assert.That(notify[1].OldStartingIndex, Is.EqualTo(-1));
            Assert.That(notify[1].NewStartingIndex, Is.EqualTo(1));
            Assert.That(notify[1].OldItems, Is.Null);
            Assert.That(notify[1].NewItems, Has.Count.EqualTo(1));

            Assert.That(log.Max.Identifier.Severity, Is.EqualTo(EventSeverity.Warning));
            Assert.That(log.Max.Identifier.Description, Is.EqualTo("Warning 1"));
        }

        [TestCase(TestMode.Add)]
        [TestCase(TestMode.InsertAtEnd)]
        public void AddSameEvents(TestMode mode)
        {
            List<NotifyAction> notify = new();
            EventLog<EventData> log = new();
            log.CollectionChanged += (s, e) => {
                notify.Add(new NotifyAction(e));
            };
            DateTime start = DateTime.Now.ToLocalTime();
            IEvent<EventData> entry = EventData.Get(EventSeverity.Warning, "Warning");

            switch (mode) {
            case TestMode.Add:
                log.Add(entry);
                log.Add(entry);
                break;
            case TestMode.InsertAtEnd:
                log.Insert(0, entry);
                log.Insert(1, entry);
                break;
            default:
                Assert.Fail("Invalid test case");
                break;
            }

            Assert.That(log, Has.Count.EqualTo(2));
            Assert.That(log[0].Identifier.Description, Is.EqualTo("Warning"));
            Assert.That(log[0].Identifier.Severity, Is.EqualTo(EventSeverity.Warning));
            Assert.That(log[0].TimeStamp, Is.GreaterThanOrEqualTo(start));

            Assert.That(log[1].Identifier.Description, Is.EqualTo("Warning"));
            Assert.That(log[1].Identifier.Severity, Is.EqualTo(EventSeverity.Warning));
            Assert.That(log[1].TimeStamp, Is.GreaterThanOrEqualTo(log[0].TimeStamp));

            Assert.That(notify, Has.Count.EqualTo(2));
            Assert.That(notify[0].Action, Is.EqualTo(NotifyCollectionChangedAction.Add));
            Assert.That(notify[0].OldStartingIndex, Is.EqualTo(-1));
            Assert.That(notify[0].NewStartingIndex, Is.EqualTo(0));
            Assert.That(notify[0].OldItems, Is.Null);
            Assert.That(notify[0].NewItems, Has.Count.EqualTo(1));
            Assert.That(notify[1].Action, Is.EqualTo(NotifyCollectionChangedAction.Add));
            Assert.That(notify[1].OldStartingIndex, Is.EqualTo(-1));
            Assert.That(notify[1].NewStartingIndex, Is.EqualTo(1));
            Assert.That(notify[1].OldItems, Is.Null);
            Assert.That(notify[1].NewItems, Has.Count.EqualTo(1));

            Assert.That(log.Max.Identifier.Severity, Is.EqualTo(EventSeverity.Warning));
        }

        [Test]
        public void InsertAtBeginning()
        {
            List<NotifyAction> notify = new();
            EventLog<EventData> log = new();
            log.CollectionChanged += (s, e) => {
                notify.Add(new NotifyAction(e));
            };
            DateTime start = DateTime.Now.ToLocalTime();
            log.Add(EventData.Get(EventSeverity.Warning, "Warning"));

            Assert.That(() => {
                log.Insert(0, EventData.Get(EventSeverity.Information, "Info"));
            }, Throws.TypeOf<NotSupportedException>());

            Assert.That(log, Has.Count.EqualTo(1));
            Assert.That(log[0].Identifier.Description, Is.EqualTo("Warning"));
            Assert.That(log[0].Identifier.Severity, Is.EqualTo(EventSeverity.Warning));
            Assert.That(log[0].TimeStamp, Is.GreaterThanOrEqualTo(start));

            Assert.That(notify, Has.Count.EqualTo(1));
            Assert.That(notify[0].Action, Is.EqualTo(NotifyCollectionChangedAction.Add));
            Assert.That(notify[0].OldStartingIndex, Is.EqualTo(-1));
            Assert.That(notify[0].NewStartingIndex, Is.EqualTo(0));
        }

        [TestCase(TestMode.Add)]
        [TestCase(TestMode.InsertAtEnd)]
        public void AddEventsSimple(TestMode mode)
        {
            List<NotifyAction> notify = new();
            EventLog<EventSeverity> log = new();
            log.CollectionChanged += (s, e) => {
                notify.Add(new NotifyAction(e));
            };
            DateTime start = DateTime.Now.ToLocalTime();
            switch (mode) {
            case TestMode.Add:
                log.Add(new EventEntry<EventSeverity>(EventSeverity.Warning));
                log.Add(new EventEntry<EventSeverity>(EventSeverity.Information));
                break;
            case TestMode.InsertAtEnd:
                log.Insert(0, new EventEntry<EventSeverity>(EventSeverity.Warning));
                log.Insert(1, new EventEntry<EventSeverity>(EventSeverity.Information));
                break;
            default:
                Assert.Fail("Invalid test case");
                break;
            }

            Assert.That(log, Has.Count.EqualTo(2));
            Assert.That(log[0].Identifier, Is.EqualTo(EventSeverity.Warning));
            Assert.That(log[0].TimeStamp, Is.GreaterThanOrEqualTo(start));

            Assert.That(log[1].Identifier, Is.EqualTo(EventSeverity.Information));
            Assert.That(log[1].TimeStamp, Is.GreaterThanOrEqualTo(log[0].TimeStamp));

            Assert.That(notify, Has.Count.EqualTo(2));
            Assert.That(notify[0].Action, Is.EqualTo(NotifyCollectionChangedAction.Add));
            Assert.That(notify[0].OldStartingIndex, Is.EqualTo(-1));
            Assert.That(notify[0].NewStartingIndex, Is.EqualTo(0));
            Assert.That(notify[0].OldItems, Is.Null);
            Assert.That(notify[0].NewItems, Has.Count.EqualTo(1));
            Assert.That(notify[1].Action, Is.EqualTo(NotifyCollectionChangedAction.Add));
            Assert.That(notify[1].OldStartingIndex, Is.EqualTo(-1));
            Assert.That(notify[1].NewStartingIndex, Is.EqualTo(1));
            Assert.That(notify[1].OldItems, Is.Null);
            Assert.That(notify[1].NewItems, Has.Count.EqualTo(1));

            // Type T doesn't implement IComparable<T>.
            Assert.That(log.Max, Is.Null);
        }

        [Test]
        public void IndexInRange()
        {
            EventLog<EventSeverity> log = new() {
                new EventEntry<EventSeverity>(EventSeverity.Warning),
                new EventEntry<EventSeverity>(EventSeverity.Information)
            };

            Assert.That(log[0].Identifier, Is.EqualTo(EventSeverity.Warning));
            Assert.That(log[1].Identifier, Is.EqualTo(EventSeverity.Information));
        }

        [Test]
        public void IndexOutOfRange()
        {
            EventLog<EventSeverity> log = new() {
                new EventEntry<EventSeverity>(EventSeverity.Warning),
                new EventEntry<EventSeverity>(EventSeverity.Information)
            };

            Assert.That(() => {
                _ = log[2].Identifier;
            }, Throws.TypeOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void IndexSet()
        {
            EventLog<EventSeverity> log = new() {
                new EventEntry<EventSeverity>(EventSeverity.Warning),
                new EventEntry<EventSeverity>(EventSeverity.Information)
            };

            Assert.That(() => {
                log[0] = new EventEntry<EventSeverity>(EventSeverity.Error);
            }, Throws.TypeOf<NotSupportedException>());
        }

        [Test]
        public void IndexOf()
        {
            EventLog<EventData> log = new();
            EventEntry<EventData> e1 = EventData.Get(EventSeverity.Warning, "Warning 1");
            EventEntry<EventData> e2 = EventData.Get(EventSeverity.Warning, "Warning 2");
            EventEntry<EventData> e3 = EventData.Get(EventSeverity.Warning, "Warning 2");
            EventEntry<EventData> e4 = EventData.Get(EventSeverity.Information, "Info");

            log.Add(e1);
            log.Add(e2);

            Assert.That(log.IndexOf(e1), Is.EqualTo(0));
            Assert.That(log.IndexOf(e2), Is.EqualTo(1));
            Assert.That(log.IndexOf(e3), Is.EqualTo(-1));
            Assert.That(log.IndexOf(e4), Is.EqualTo(-1));
        }

        [Test]
        public void Contains()
        {
            EventLog<EventData> log = new();
            EventEntry<EventData> e1 = EventData.Get(EventSeverity.Warning, "Warning 1");
            EventEntry<EventData> e2 = EventData.Get(EventSeverity.Warning, "Warning 2");
            EventEntry<EventData> e3 = EventData.Get(EventSeverity.Warning, "Warning 2");
            EventEntry<EventData> e4 = EventData.Get(EventSeverity.Information, "Info");

            log.Add(e1);
            log.Add(e2);

            Assert.That(log, Does.Contain(e1));
            Assert.That(log, Does.Contain(e2));
            Assert.That(log, Does.Not.Contain(e3)); // It doesn't implement IEquatable
            Assert.That(log, Does.Not.Contain(e4));
        }

        [Test]
        public void RemoveAt()
        {
            List<NotifyAction> notify = new();
            EventLog<EventData> log = new();
            log.CollectionChanged += (s, e) => {
                notify.Add(new NotifyAction(e));
            };
            log.Add(EventData.Get(EventSeverity.Warning, "Warning 1"));
            log.Add(EventData.Get(EventSeverity.Warning, "Warning 2"));

            Assert.That(notify, Has.Count.EqualTo(2));
            Assert.That(log, Has.Count.EqualTo(2));

            Assert.That(() => {
                log.RemoveAt(0);
            }, Throws.TypeOf<NotSupportedException>());
            Assert.That(notify, Has.Count.EqualTo(2));
            Assert.That(log, Has.Count.EqualTo(2));

            Assert.That(() => {
                log.RemoveAt(1);
            }, Throws.TypeOf<NotSupportedException>());
            Assert.That(notify, Has.Count.EqualTo(2));
            Assert.That(log, Has.Count.EqualTo(2));
        }

        [Test]
        public void Remove()
        {
            List<NotifyAction> notify = new();
            EventLog<EventData> log = new();
            log.CollectionChanged += (s, e) => {
                notify.Add(new NotifyAction(e));
            };
            EventEntry<EventData> e1 = EventData.Get(EventSeverity.Warning, "Warning 1");
            EventEntry<EventData> e2 = EventData.Get(EventSeverity.Warning, "Warning 2");
            log.Add(e1);
            log.Add(e2);

            Assert.That(notify, Has.Count.EqualTo(2));
            Assert.That(log, Has.Count.EqualTo(2));

            Assert.That(() => {
                log.Remove(e2);
            }, Throws.TypeOf<NotSupportedException>());
            Assert.That(notify, Has.Count.EqualTo(2));
            Assert.That(log, Has.Count.EqualTo(2));

            Assert.That(() => {
                log.Remove(e1);
            }, Throws.TypeOf<NotSupportedException>());
            Assert.That(notify, Has.Count.EqualTo(2));
            Assert.That(log, Has.Count.EqualTo(2));
        }

        [Test]
        public void Clear()
        {
            List<NotifyAction> notify = new();
            EventLog<EventData> log = new();
            log.CollectionChanged += (s, e) => {
                notify.Add(new NotifyAction(e));
            };
            log.Add(EventData.Get(EventSeverity.Warning, "Warning 1"));
            log.Add(EventData.Get(EventSeverity.Warning, "Warning 2"));

            Assert.That(notify, Has.Count.EqualTo(2));
            Assert.That(log, Has.Count.EqualTo(2));
            Assert.That(log.Max.Identifier.Description, Is.EqualTo("Warning 1"));

            log.Clear();
            Assert.That(notify, Has.Count.EqualTo(3));
            Assert.That(notify[2].Action, Is.EqualTo(NotifyCollectionChangedAction.Reset));
            Assert.That(notify[2].OldStartingIndex, Is.EqualTo(-1));
            Assert.That(notify[2].OldItems, Is.Null);
            Assert.That(notify[2].NewStartingIndex, Is.EqualTo(-1));
            Assert.That(notify[2].NewItems, Is.Null);
            Assert.That(log, Is.Empty);
        }

        [Test]
        public void CopyTo()
        {
            EventLog<EventSeverity> log = new() {
                new EventEntry<EventSeverity>(EventSeverity.Warning),
                new EventEntry<EventSeverity>(EventSeverity.Information)
            };

            EventEntry<EventSeverity>[] list = new EventEntry<EventSeverity>[2];
            log.CopyTo(list, 0);

            int i = 0;
            foreach (EventEntry<EventSeverity> entry in log.Cast<EventEntry<EventSeverity>>()) {
                Assert.That(list[i].Identifier, Is.EqualTo(entry.Identifier));
                Assert.That(list[i].TimeStamp, Is.EqualTo(entry.TimeStamp));
                i++;
            }
        }
    }
}
