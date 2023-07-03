namespace RJCP.Core.Collections.Generic
{
    using System;
    using System.Collections.Generic;
    using NUnit.Framework;

    [TestFixture]
    public class NamedItemCollectionTest
    {
        private class NamedItem : INamedItem
        {
            public NamedItem(string name) { Name = name; }

            public string Name { get; private set; }
        }

        private class NamedItems : NamedItemCollection<NamedItem>
        {
            public NamedItems() : base("NamedItem") { }
        }

        [Test]
        public void Instantiate()
        {
            NamedItems items = new NamedItems();
            Assert.That(items, Is.Empty);
            Assert.That(items.IsReadOnly, Is.False);
            Assert.That(items.Name, Is.EqualTo("NamedItem"));
        }

        [Test]
        public void AddItem()
        {
            NamedItems items = new NamedItems { new NamedItem("one") };
            Assert.That(items, Has.Count.EqualTo(1));
        }

        [Test]
        public void AddDuplicateItem()
        {
            NamedItem item = new NamedItem("one");
            NamedItems items = new NamedItems() { item };
            Assert.That(() => { items.Add(item); }, Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void AddDuplicateItemName()
        {
            NamedItems items = new NamedItems { new NamedItem("one") };
            Assert.That(() => { items.Add(new NamedItem("one")); }, Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void AddNull()
        {
            NamedItems items = new NamedItems();
            Assert.That(() => { items.Add(null); }, Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void AddNullName()
        {
            NamedItems items = new NamedItems();
            Assert.That(() => { items.Add(new NamedItem(null)); }, Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void RemoveItem()
        {
            NamedItem item = new NamedItem("one");
            NamedItems items = new NamedItems() { item };
            Assert.That(items, Has.Count.EqualTo(1));

            items.Remove(item);
            Assert.That(items, Is.Empty);
        }

        [Test]
        public void RemoveNamedItem()
        {
            NamedItems items = new NamedItems { new NamedItem("one") };
            Assert.That(items, Has.Count.EqualTo(1));

            items.Remove("one");
            Assert.That(items, Is.Empty);
        }

        [Test]
        public void RemoveNullItem()
        {
            NamedItems items = new NamedItems { new NamedItem("one") };
            Assert.That(() => { items.Remove((NamedItem)null); }, Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void RemoveNullItemName()
        {
            NamedItems items = new NamedItems { new NamedItem("one") };
            Assert.That(() => { items.Remove((string)null); }, Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void RemoveItemNullName()
        {
            NamedItems items = new NamedItems { new NamedItem("one") };
            Assert.That(items.Remove(new NamedItem(null)), Is.False);
        }

        [Test]
        public void RemoveNonExistentName()
        {
            NamedItems items = new NamedItems { new NamedItem("one") };
            Assert.That(items.Remove("two"), Is.False);
            Assert.That(items, Has.Count.EqualTo(1));
        }

        [Test]
        public void RemoveNonExistentItem()
        {
            NamedItems items = new NamedItems { new NamedItem("one") };
            Assert.That(items.Remove(new NamedItem("two")), Is.False);
            Assert.That(items, Has.Count.EqualTo(1));
        }

        [Test]
        public void RemoveDifferentObject()
        {
            NamedItems items = new NamedItems { new NamedItem("one") };
            // Even though we have an object named "one", we create a different object not in the
            // collection with the same name. That should fail.
            Assert.That(items.Remove(new NamedItem("one")), Is.False);
            Assert.That(items, Has.Count.EqualTo(1));
        }

        [Test]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Assertion", "NUnit2014:Use SomeItemsConstraint.",
            Justification = "Test case must test Contains direct")]
        public void ContainsItem()
        {
            NamedItem item = new NamedItem("one");
            NamedItems items = new NamedItems { item };
            Assert.That(items, Has.Count.EqualTo(1));
            Assert.That(items.Contains(item), Is.True);
        }

        [Test]
        public void ContainsNamedItem()
        {
            NamedItems items = new NamedItems { new NamedItem("one") };
            Assert.That(items.Contains("one"), Is.True);
        }

        [Test]
        public void ContainsNonexistentName()
        {
            NamedItems items = new NamedItems { new NamedItem("one") };
            Assert.That(items.Contains("two"), Is.False);
        }

        [Test]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Assertion", "NUnit2014:Use SomeItemsConstraint.",
            Justification = "Test case must test Contains direct")]
        public void ContainsNonexistentItem()
        {
            NamedItems items = new NamedItems { new NamedItem("one") };
            Assert.That(items.Contains(new NamedItem("two")), Is.False);
        }

        [Test]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Assertion", "NUnit2014:Use SomeItemsConstraint.",
            Justification = "Test case must test Contains direct")]
        public void ContainsNonexistentItemSameNameDifferentObject()
        {
            NamedItems items = new NamedItems { new NamedItem("one") };
            Assert.That(items.Contains(new NamedItem("one")), Is.False);
        }

        [Test]
        public void ContainsNullName()
        {
            NamedItems items = new NamedItems { new NamedItem("one") };
            Assert.That(items.Contains((string)null), Is.False);
        }

        [Test]
        public void ContainsNullItem()
        {
            NamedItems items = new NamedItems { new NamedItem("one") };
            Assert.That(() => { items.Contains((NamedItem)null); }, Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Assertion", "NUnit2014:Use SomeItemsConstraint.",
            Justification = "Test case must test Contains direct")]
        public void ContainsItemNullName()
        {
            NamedItems items = new NamedItems { new NamedItem("one") };
            Assert.That(items.Contains(new NamedItem(null)), Is.False);
        }

        [Test]
        public void IndexerMultipleItems()
        {
            NamedItem item1 = new NamedItem("one");
            NamedItem item2 = new NamedItem("two");
            NamedItem item3 = new NamedItem("three");
            NamedItems items = new NamedItems() { item1, item2, item3 };
            Assert.That(items["one"], Is.EqualTo(item1));
            Assert.That(items["two"], Is.EqualTo(item2));
            Assert.That(items["three"], Is.EqualTo(item3));
        }

        [Test]
        public void IndexerNullItem()
        {
            NamedItems items = new NamedItems { new NamedItem("one") };
            Assert.That(() => { _ = items[null]; }, Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void IndexerNonExistent()
        {
            NamedItems items = new NamedItems { new NamedItem("one") };
            Assert.That(() => { _ = items["two"]; }, Throws.TypeOf<KeyNotFoundException>());
        }

        [Test]
        public void Clear()
        {
            NamedItem item1 = new NamedItem("one");
            NamedItem item2 = new NamedItem("two");
            NamedItem item3 = new NamedItem("three");
            NamedItems items = new NamedItems() { item1, item2, item3 };
            Assert.That(items, Has.Count.EqualTo(3));
            items.Clear();
            Assert.That(items, Is.Empty);
            Assert.That(items.Contains("one"), Is.False);
            Assert.That(items.Contains("two"), Is.False);
            Assert.That(items.Contains("three"), Is.False);
        }

        [Test]
        public void Enumerate()
        {
            NamedItem item1 = new NamedItem("one");
            NamedItem item2 = new NamedItem("two");
            NamedItem item3 = new NamedItem("three");
            NamedItems items = new NamedItems() { item1, item2, item3 };
            int count = 0;
            foreach (NamedItem item in items) {
                count++;
            }

            Assert.That(count, Is.EqualTo(3));
        }

        [Test]
        public void ReadOnlySet()
        {
            NamedItem item = new NamedItem("one");
            NamedItems items = new NamedItems() { item };
            items.IsReadOnly = true;
            Assert.That(items.IsReadOnly, Is.True);
        }

        [Test]
        public void ReadOnlyMakeWritableFail()
        {
            NamedItem item = new NamedItem("one");
            NamedItems items = new NamedItems() { item };
            items.IsReadOnly = true;
            Assert.That(() => { items.IsReadOnly = false; }, Throws.TypeOf<InvalidOperationException>());
        }

        [Test]
        public void ReadOnlyAddItem()
        {
            NamedItem item = new NamedItem("one");
            NamedItems items = new NamedItems() { item };
            items.IsReadOnly = true;
            Assert.That(() => { items.Add(new NamedItem("two")); }, Throws.TypeOf<InvalidOperationException>());
        }

        [Test]
        public void ReadOnlyClear()
        {
            NamedItem item = new NamedItem("one");
            NamedItems items = new NamedItems() { item };
            items.IsReadOnly = true;
            Assert.That(() => { items.Clear(); }, Throws.TypeOf<InvalidOperationException>());
        }

        [Test]
        public void ReadOnlyRemoveItem()
        {
            NamedItem item = new NamedItem("one");
            NamedItems items = new NamedItems() { item };
            items.IsReadOnly = true;
            Assert.That(() => { items.Remove(item); }, Throws.TypeOf<InvalidOperationException>());
        }

        [Test]
        public void ReadOnlyRemoveName()
        {
            NamedItem item = new NamedItem("one");
            NamedItems items = new NamedItems() { item };
            items.IsReadOnly = true;
            Assert.That(() => { items.Remove("one"); }, Throws.TypeOf<InvalidOperationException>());
        }
    }
}
