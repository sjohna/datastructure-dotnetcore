using NUnit.Framework;

using Datastructures;
using System.Collections.Generic;
using System.Linq;
using System;

namespace TestDataStructures
{
    /**
        Tests that all ICollection implementations should pass.

        Note: when adding items to a collection during a test, always add distinct items. This allows this test fixture
        to support sets and other similar collections.
    */
    [TestFixture(typeof(Vector<int>))]
    public class TestICollectionImplementation<Collection> where Collection : ICollection<int>, new()
    {
        private Collection m_collection;

        [SetUp]
        public void Setup()
        {
            m_collection = new Collection();
        }

        [Test]
        public void Create()
        {
            Assert.AreEqual(0, m_collection.Count);
            Assert.IsFalse(m_collection.IsReadOnly);
        }

        [Test]
        public void AddSingleItem()
        {
            m_collection.Add(2);
            Assert.AreEqual(1, m_collection.Count);
        }

        [Test]
        public void RemoveFromEmptyCollection()
        {
            Assert.IsFalse(m_collection.Remove(2));
            Assert.AreEqual(0, m_collection.Count);
        }

        [Test]
        public void AddAndRemoveSingleItem()
        {
            m_collection.Add(2);
            Assert.IsTrue(m_collection.Remove(2));
            Assert.AreEqual(0, m_collection.Count);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]
        [TestCase(9)]
        [TestCase(10)]
        public void RemoveFromCollectionWithSeveralItems(int itemToRemove)
        {
            for (int i = 1; i <= 10; ++i)
            {
                m_collection.Add(i);
            }

            Assert.IsTrue(m_collection.Remove(itemToRemove));
            Assert.AreEqual(9, m_collection.Count);
        }
    }
} 