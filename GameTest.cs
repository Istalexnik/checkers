using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Checkers
{
    [TestFixture]
    public static class GameTest
    {
        private static IWebDriver driver;

        [SetUp]
        public static void SetUp()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://www.gamesforthebrain.com/game/checkers/");
        }

        [Test, Order(1)]
        public static void TestTheSiteIsUp()
        {
            IWebElement gameBoard = driver.FindElement(By.Id("board"));

            Assert.NotNull(gameBoard, "The site is down");
        }

        [Test, Order(2)]
        public static void TestFiveMoves()
        {
            MakeMove("space22", "space13");
            MakeMove("space13", "space04");
            MakeMove("space62", "space53");
            MakeMove("space51", "space73");
            MakeMove("space31", "space22");
            driver.FindElement(By.LinkText("Restart...")).Click();

            Assert.True(driver.FindElement(By.Id("message")).Text == "Select an orange piece to move.");
        }

        [Test, Order(3)]
        public static void TestWinGame()
        {
            MakeMove("space22", "space13");
            MakeMove("space13", "space04");
            MakeMove("space62", "space53");
            MakeMove("space51", "space73");
            MakeMove("space31", "space22");
            MakeMove("space22", "space13");
            MakeMove("space42", "space24", "space06");
            MakeMove("space11", "space22");
            MakeMove("space40", "space51");
            MakeMove("space20", "space42");
            MakeMove("space22", "space33");
            MakeMove("space33", "space24");
            MakeMove("space24", "space15");
            MakeMove("space51", "space62");
            MakeMove("space62", "space53");
            MakeMove("space71", "space53");
            MakeMove("space13", "space24");
            MakeMove("space15", "space37");
            MakeMove("space06", "space17");
            MakeMove("space24", "space46");
            MakeMove("space53", "space35");
            MakeMove("space42", "space64");
            MakeMove("space46", "space57");
            MakeMove("space00", "space11");
            MakeMove("space11", "space22");
            MakeMove("space22", "space33");
            MakeMove("space33", "space44");
            MakeMove("space44", "space66");
            MakeMove("space73", "space55");
            Thread.Sleep(2000);

            Assert.True(driver.FindElement(By.Id("message")).Text == "You have won!");
        }

        [TearDown]
        public static void TearDown()
        {
            driver.Quit();
        }

        private static void MakeMove(params string[] moves)
        {
            Thread.Sleep(2000);
            string message = driver.FindElement(By.Id("message")).Text;
            if (message == "Make a move." || message == "Select an orange piece to move.")
            {
                driver.FindElement(By.Name(moves[0])).Click();
                driver.FindElement(By.Name(moves[1])).Click();
                if (moves.Length > 2)
                {
                    for (int i = 2; i < moves.Length; i++)
                    {
                        Thread.Sleep(2000);
                        driver.FindElement(By.Name(moves[i])).Click();
                    }
                }
            }
        }
    }
}