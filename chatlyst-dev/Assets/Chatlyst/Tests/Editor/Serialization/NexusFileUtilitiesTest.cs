using System;
using Chatlyst.Editor.Serialization;
using NUnit.Framework;

namespace Tests.Editor.Serialization
{
    /// <summary>
    ///     Unit Test for <see cref="Chatlyst.Editor.Serialization.NexusFileUtilities" />
    /// </summary>
    [TestOf(typeof(NexusFileUtilities))] [Author("Morsiusiurandum", "Morsiusiurandum@outlook.com")]
    public class NexusFileUtilitiesTest
    {
        private const string PrePath = "Assets/Chatlyst/";

        [TestCase(PrePath + "Editor/Resources/Template/Plot.nvp", ExpectedResult = true)]
        [TestCase(PrePath + "Editor/Resources/Template/Pite.nvp", ExpectedResult = false)]
        [TestCase(PrePath + "Editor/Resources/Template/Plot.nzp", ExpectedResult = false)]
        public bool PathValidCheckTest(string path)
        {
            //  Assert.Throws<ArgumentException>(MethodThatThrows);

            try
            {
                return NexusFileUtilities.PathValidCheck(path);
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
