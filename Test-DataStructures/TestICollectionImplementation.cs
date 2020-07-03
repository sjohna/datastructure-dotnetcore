using NUnit.Framework;

using Datastructures;

namespace TestDataStructures
{
    /**
        Tests that all unbounded ICollection implementations should pass.

        Note: when adding items to a collection during a test, always add distinct items. This allows this test fixture
        to support sets and other similar collections.
    */
    [TestFixture(typeof(Vector<int>))]

    // run tests against .NET standard library collections to make sure behavior is consistent with new collections
    [TestFixture(typeof(System.Collections.Generic.List<int>))]
    [TestFixture(typeof(System.Collections.Generic.LinkedList<int>))]
    [TestFixture(typeof(System.Collections.Generic.HashSet<int>))]
    [TestFixture(typeof(System.Collections.Generic.SortedSet<int>))]

    // test traversible list implementations
    [TestFixture(typeof(TraversibleList<Vector<int>,int>))]
    [TestFixture(typeof(TraversibleList<System.Collections.Generic.List<int>,int>))]
    public class TestICollectionImplementation<Collection> where Collection : System.Collections.Generic.ICollection<int>, new()
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

        [Test]
        public void ContainsOnEmptyCollection()
        {
            Assert.IsFalse(m_collection.Contains(1));
        }

        [Test]
        public void ContainsOnSingleItemCollection()
        {
            m_collection.Add(1);
            Assert.IsTrue(m_collection.Contains(1));
            Assert.IsFalse(m_collection.Contains(2));
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
            Assert.IsFalse(m_collection.Contains(itemToRemove));
        }

        [Test]
        public void Clear()
        {
            m_collection.Add(1);
            m_collection.Clear();
            Assert.AreEqual(0, m_collection.Count);
            Assert.IsFalse(m_collection.Contains(1));
        }

        [Test]
        public void CopyToBeginningOfArrayExecutes()
        {
            m_collection.Add(1);
            m_collection.Add(2);
            m_collection.Add(3);
            var array = new int[3];
            m_collection.CopyTo(array,0);
        }

        [Test]
        public void CopyToMiddleOfArrayDoesNotAffectOtherArrayElements()
        {
            m_collection.Add(10);
            m_collection.Add(20);
            m_collection.Add(30);
            var array = new int[10] {1,2,3,4,5,6,7,8,9,10};
            m_collection.CopyTo(array,3);

            // it's not necessarily the case that elements 3-5 of array are 10,20,30 in order, so we don't assert that
            Assert.AreEqual(1, array[0]);
            Assert.AreEqual(2, array[1]);
            Assert.AreEqual(3, array[2]);
            Assert.AreEqual(7, array[6]);
            Assert.AreEqual(8, array[7]);
            Assert.AreEqual(9, array[8]);
            Assert.AreEqual(10, array[9]);
        }

        [Test]
        public void GetEnumeratorExecutes()
        {
            Assert.DoesNotThrow(() => m_collection.GetEnumerator());
        }

        [Test]
        public void ExplicitlyImplementedGetEnumeratorExecutes()
        {
            Assert.DoesNotThrow(() => (m_collection as System.Collections.IEnumerable).GetEnumerator());
        }

        [Test]
        public void EnumeratorHasCorrectNumberOfElements()
        {
            m_collection.Add(1);
            m_collection.Add(2);
            m_collection.Add(3);
            m_collection.Add(4);
            m_collection.Add(5);

            int numElements = 0;

            foreach (var item in m_collection)
            {
                ++numElements;
            }

            Assert.AreEqual(5, numElements);
        }

        [Test]
        public void AddTenThousandItems()
        {
            for (int i = 0; i < 10000; ++i)
            {
                m_collection.Add(i);
            }

            Assert.AreEqual(10000, m_collection.Count);
        }

        [Test]
        public void ICollectionScenario()
        {
            void DoScenario() 
            {
                m_collection.Add(1);
                m_collection.Add(2);
                m_collection.Add(3);
                CheckCountAndEnumeratorLength(3);

                m_collection.Remove(3);
                m_collection.Remove(4);
                CheckCountAndEnumeratorLength(2);

                m_collection.Add(3);
                CheckCountAndEnumeratorLength(3);

                m_collection.Add(4);
                CheckCountAndEnumeratorLength(4);

                m_collection.Remove(1);
                CheckCountAndEnumeratorLength(3);

                for (int i = 1; i <= 100; ++i)
                {
                    m_collection.Add(10*i);
                }

                CheckCountAndEnumeratorLength(103);

                m_collection.Add(0);

                for (int i = 1; i < 100; ++i)
                {
                    m_collection.Add(i*1234);
                    m_collection.Remove((i-1)*1234);
                    CheckCountAndEnumeratorLength(104);
                    Assert.IsTrue(m_collection.Contains(i*1234));
                    Assert.IsFalse(m_collection.Contains((i-1)*1234));
                }
            }

            for (int i = 0; i < 5; ++i)
            {
                DoScenario();
                m_collection.Clear();
            }
        }

        private void CheckCountAndEnumeratorLength(int expectedCount)
        {
            Assert.AreEqual(m_collection.Count, expectedCount);
            int numElements = 0;

            foreach (var element in m_collection)
            {
                ++numElements;
            }

            Assert.AreEqual(m_collection.Count, numElements);
        }
    }
} 