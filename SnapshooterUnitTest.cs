using Snapshooter.Xunit;
using System.Diagnostics;
using System.Text.Json;

namespace Snapshooter_Tests
{
    public class SnapshooterUnitTest
    {

        [Fact]
        public void Assert_AssertEqualObj_AssertSuccessful()
        {
            var MyStudent = new Student(1, "abc"); 
            Snapshot.Match(MyStudent);
        }

        [Fact] 
        public void Assert_AssertEqualJson_AssertSuccessfulInTime()
        {
            var (userJsonString, timeTaken) = GetUserWithTiming();
            Snapshot.Match(userJsonString);
            Assert.True(timeTaken < 1000);
        }


        
        private static (string, long) GetUserWithTiming()
        {
            var url = "https://jsonplaceholder.typicode.com/users/2";

            using HttpClient client = new();
            Stopwatch stopwatch = Stopwatch.StartNew();
            HttpResponseMessage response = client.GetAsync(url).Result;
            response.EnsureSuccessStatusCode();
            string responseBody = response.Content.ReadAsStringAsync().Result;
            JsonDocument jsonDoc = JsonDocument.Parse(responseBody);
            stopwatch.Stop();
            long elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
            return (responseBody, elapsedMilliseconds);
        }
    }

    internal record Student(int StudentId, string StudentName);
}