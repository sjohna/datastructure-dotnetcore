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
    }
}