using NUnit.Framework;
using Parse;
using ParseUnitySampleCommon;
using ParseUnitySampleCommon.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ParseUnitySampleBackend.Tests
{
    [TestFixture]
    public class SaveUserTests
    {
        [SetUp]
        public async void Init()
        {
            TestingHelpers.InitParse();
            ParseUser.LogOut();
        }

        [Test]
        [ExpectedException(ExpectedMessage = "Must supply a player name when creating a new user")]
        public async void RequiresPlayerName()
        {
            var user = new GameUser()
            {
                Username = TestingHelpers.GetRandomUserEmail(),
                Password = "a"
            };
            await user.SignUpAsync();
        }

        [Test]
        [ExpectedException(ExpectedMessage = "Username and email address must be equal")]
        public async void UsernameAndEmailMustBeEqual()
        {
            var user = new GameUser()
            {
                Username = TestingHelpers.GetRandomUserEmail(),
                Email = TestingHelpers.GetRandomUserEmail(),
                Password = "a",
                PlayerName = "Test User"
            };
            await user.SignUpAsync();
        }

        [Test]
        public async void SuccessfullyLogsAUserIn()
        {
            var email = TestingHelpers.GetRandomUserEmail();
            var user = new GameUser()
            {
                Username = email,
                Email = email,
                Password = "a",
                PlayerName = "Test User"
            };
            await user.SignUpAsync();

            Assert.IsNotNull(ParseUser.CurrentUser);
        }
    }
}