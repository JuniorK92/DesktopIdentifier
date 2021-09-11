using DI;
using NUnit.Framework;

namespace DiTestProject
{
    public class Tests
    {
        MainWindowViewModel _mainWindowViewModel { get; set; }
        [SetUp]
        public void Setup()
        {
            _mainWindowViewModel = new MainWindowViewModel();
        }

        [Test]
        public void Test1()
        {
            Assert.AreNotEqual(_mainWindowViewModel, null);

            Assert.AreEqual(_mainWindowViewModel.Width, 150);

            Assert.AreEqual(_mainWindowViewModel.Height, 100);
        }
    }
}