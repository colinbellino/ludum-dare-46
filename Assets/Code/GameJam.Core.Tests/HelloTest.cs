using NUnit.Framework;

namespace GameJam.Core.Tests
{
	public class HelloTest
	{
		[Test]
		public void Hello_Say_ReturnsAString()
		{
			var hello = new Hello();
			var text = hello.Say("Colin");

			Assert.AreEqual(text, "Hello World, Colin.");
		}
	}
}
