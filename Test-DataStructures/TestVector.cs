using NUnit.Framework;

using Datastructures;
using System.Linq;
using System;

namespace TestDataStructures
{
    public class TestVector
    {
        private Vector<int> m_vector; 

        [SetUp]
        public void Setup()
        {
            m_vector = new Vector<int>();
        }

        [Test]
        public void Create()
        {
            Assert.AreEqual(0, m_vector.Count);
        }

        [Test]
        public void AddOneItem()
        {
            m_vector.Add(7);
            Assert.AreEqual(1, m_vector.Count);
            Assert.AreEqual(7, m_vector[0]);
        }

        [Test]
        public void IndexerGetThrowsExceptionOnEmptyVector()
        {
            Assert.Throws<IndexOutOfRangeException>(() => { var x = m_vector[0]; });
            Assert.Throws<IndexOutOfRangeException>(() => { var x = m_vector[-1]; });
            Assert.Throws<IndexOutOfRangeException>(() => { var x = m_vector[1]; });
        }

        [Test]
        public void SetAfterAdding()
        {
            m_vector.Add(7);
            m_vector[0] = 9;
            Assert.AreEqual(1, m_vector.Count);
            Assert.AreEqual(9, m_vector[0]);
        }

        [Test]
        public void IndexerSetThrowsExceptionOnEmptyVector()
        {
            Assert.Throws<IndexOutOfRangeException>(() => { m_vector[0] = 7; });
            Assert.Throws<IndexOutOfRangeException>(() => { m_vector[-1] = 7; });
            Assert.Throws<IndexOutOfRangeException>(() => { m_vector[1] = 7; });            
        }

    }
}