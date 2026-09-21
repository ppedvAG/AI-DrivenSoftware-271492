using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace YourProject.Tests;

[TestClass]
public class NicknameFilterTests
{
    private string _jsonFilePath = null!;
    private NicknameFilter _filter = null!;

    [TestInitialize]
    public void Setup()
    {
        _jsonFilePath = Path.Combine(
            Path.GetTempPath(),
            $"blocked-terms-{Guid.NewGuid():N}.json");

        File.WriteAllText(
            _jsonFilePath,
            """
        [
            "arschloch",
            "ficken",
            "hurensohn",
            "schlampe",
            "idiot",
            "sex",
            "porn"
        ]
        """);

        _filter = new NicknameFilter(_jsonFilePath);
    }

    [TestCleanup]
    public void Cleanup()
    {
        if (File.Exists(_jsonFilePath))
        {
            File.Delete(_jsonFilePath);
        }
    }

    [TestMethod]
    public void IsNicknameAllowed_ShouldReturnFalse_ForBlockedWord()
    {
        Assert.IsFalse(_filter.IsNicknameAllowed("Arschloch"));
    }

    [TestMethod]
    public void IsNicknameAllowed_ShouldReturnFalse_ForBlockedWordWithDifferentCasing()
    {
        Assert.IsFalse(_filter.IsNicknameAllowed("ARsChLoCh"));
    }

    [TestMethod]
    public void IsNicknameAllowed_ShouldReturnFalse_ForBlockedWordInsidePhrase()
    {
        Assert.IsFalse(_filter.IsNicknameAllowed("Super Arschloch"));
    }

    [TestMethod]
    public void IsNicknameAllowed_ShouldReturnTrue_ForAllowedNickname()
    {
        Assert.IsTrue(_filter.IsNicknameAllowed("Sonnenreiter"));
    }

    [TestMethod]
    public void IsNicknameAllowed_ShouldReturnFalse_ForEmptyNickname()
    {
        Assert.IsFalse(_filter.IsNicknameAllowed(""));
    }

    [TestMethod]
    public void IsNicknameAllowed_ShouldReturnFalse_ForWhitespaceNickname()
    {
        Assert.IsFalse(_filter.IsNicknameAllowed("   "));
    }

    [TestMethod]
    public void IsNicknameAllowed_ShouldReturnFalse_ForSexualTerm()
    {
        Assert.IsFalse(_filter.IsNicknameAllowed("Sexy Porn"));
    }

    [TestMethod]
    public void IsNicknameAllowed_ShouldReturnFalse_ForBlockedWordWithSpecialCharactersAroundIt()
    {
        Assert.IsFalse(_filter.IsNicknameAllowed("Cool-Arschloch!"));
    }

    [TestMethod]
    public void IsNicknameAllowed_ShouldReturnTrue_WhenBlockedWordIsOnlyPartOfAnotherWord()
    {
        Assert.IsTrue(_filter.IsNicknameAllowed("Arschitekt"));
    }

    [TestMethod]
    public void IsNicknameAllowed_ShouldReturnFalse_ForMultipleBlockedWords()
    {
        Assert.IsFalse(_filter.IsNicknameAllowed("cool idiot"));
    }

}
